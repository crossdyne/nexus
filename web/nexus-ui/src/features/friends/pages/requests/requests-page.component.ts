import { Component, inject, signal } from "@angular/core";
import { IncomingFriendResponse } from "../../models/incoming-friend.response";
import { OutgoingFriendResponse } from "../../models/outgoing-friend.response";
import { FriendRequestsService } from "../../services/friend-requests.service";
import { Result, Unit } from "@crossdyne/toolkit";
import { MapErrorsHelper } from "../../../../core/helpers/map-errors.helper";
import { DeclineFriendRequest } from "../../models/decline-friend.request";
import { CancelFriendRequest } from "../../models/cancel-friend.request";
import { AcceptFriendRequest } from "../../models/accept-friend.request";

@Component({
    selector: 'requests-friend-page',
    templateUrl: './requests-page.component.html',
    styleUrls: ['./requests-page.component.scss'],
    standalone: true,
})
export class RequestsPageComponent {
    private readonly friendRequestsService = inject(FriendRequestsService);

    incomingRequests = signal<IncomingFriendResponse[]>([]);
    outgoingRequests = signal<OutgoingFriendResponse[]>([]);

    constructor() {
        this.getIncomingRequests();
        this.getOutgoingRequests();
    }

    async declineRequest(friend: IncomingFriendResponse) {
        const request: DeclineFriendRequest = {
            requesterUserId: friend.userId
        };
        
        const result: Result<Unit> = await this.friendRequestsService.decline(request);

        result.match(
            unit => this.incomingRequests.update(requests => requests.filter(req => req.userId !== friend.userId)),
            errors => console.error(MapErrorsHelper.mapErrors(errors))
        );
    }

    async acceptRequest(friend: IncomingFriendResponse) {        
        const request: AcceptFriendRequest = {
            requesterUserId: friend.userId
        };

        const result: Result<Unit> = await this.friendRequestsService.accept(request);

        result.match(
            unit => this.incomingRequests.update(requests => requests.filter(req => req.userId !== friend.userId)),
            errors => console.error(MapErrorsHelper.mapErrors(errors))
        );
    }

    async cancelRequest(friend: OutgoingFriendResponse) {
        const request: CancelFriendRequest = {
            recipientUserId: friend.userId
        };

        const result: Result<Unit> = await this.friendRequestsService.cancel(request);

        result.match(
            unit => this.outgoingRequests.update(requests => requests.filter(req => req.userId !== friend.userId)),
            errors => console.error(MapErrorsHelper.mapErrors(errors))
        );
    }

    async getIncomingRequests(): Promise<void> {
        const result: Result<IncomingFriendResponse[]> = await this.friendRequestsService.incomingFriends();

        result.match(
            incomings => this.incomingRequests.set(incomings),
            errors => console.error(MapErrorsHelper.mapErrors(errors))
        );
    }
    
    async getOutgoingRequests(): Promise<void> {
        const result: Result<OutgoingFriendResponse[]> = await this.friendRequestsService.outgoingFriends();

        result.match(
            outgoing => this.outgoingRequests.set(outgoing),
            errors => console.error(MapErrorsHelper.mapErrors(errors))
        );
    }
}