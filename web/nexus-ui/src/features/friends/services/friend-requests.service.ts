import { Injectable } from "@angular/core";
import { HttpService } from "../../../core/http/http.service";
import { Result, Unit } from "@crossdyne/toolkit";
import { IncomingFriendResponse } from "../models/incoming-friend.response";
import { OutgoingFriendResponse } from "../models/outgoing-friend.response";
import { CancelFriendRequest } from "../models/cancel-friend.request";
import { DeclineFriendRequest } from "../models/decline-friend.request";
import { AcceptFriendRequest } from "../models/accept-friend.request";

@Injectable({ 
    providedIn: 'root'
})
export class FriendRequestsService extends HttpService {
    constructor() {
        super('');
    }

    async accept(request: AcceptFriendRequest): Promise<Result<Unit>> {
        return this.postAsync('friends/accept', request);
    }

    async cancel(request: CancelFriendRequest): Promise<Result<Unit>> {
        return this.postAsync('friends/cancel', request);
    }

    async decline(request: DeclineFriendRequest): Promise<Result<Unit>> {
        return this.postAsync('friends/decline', request);
    }

    async incomingFriends(): Promise<Result<IncomingFriendResponse[]>> {
        return this.getAsync<IncomingFriendResponse[]>('friends/incoming');
    }

    async outgoingFriends(): Promise<Result<OutgoingFriendResponse[]>> {
        return this.getAsync<OutgoingFriendResponse[]>('friends/outgoing');
    }
}