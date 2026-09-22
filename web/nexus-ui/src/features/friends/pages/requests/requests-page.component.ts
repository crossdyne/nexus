import { Component, inject, signal } from "@angular/core";
import { IncomingFriendResponse } from "../../models/incoming-friend.response";
import { OutgoingFriendResponse } from "../../models/outgoing-friend.response";
import { FriendRequestsService } from "../../services/friend-requests.service";
import { Result } from "@crossdyne/toolkit";
import { MapErrorsHelper } from "../../../../core/helpers/map-errors.helper";

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

    declineRequest(request: IncomingFriendResponse) {
        throw new Error('Method not implemented.');
    }

    acceptRequest(request: IncomingFriendResponse) {
        throw new Error('Method not implemented.');
    }

    cancelRequest(request: OutgoingFriendResponse) {
        throw new Error('Method not implemented.');
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