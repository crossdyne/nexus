import { Injectable } from "@angular/core";
import { HttpService } from "../../../core/http/http.service";
import { Result } from "@crossdyne/toolkit";
import { IncomingFriendResponse } from "../models/incoming-friend.response";
import { OutgoingFriendResponse } from "../models/outgoing-friend.response";

@Injectable({ 
    providedIn: 'root'
})
export class FriendRequestsService extends HttpService {
    constructor() {
        super('');
    }

    async incomingFriends(): Promise<Result<IncomingFriendResponse[]>> {
        return this.getAsync<IncomingFriendResponse[]>('friends/incoming');
    }

    async outgoingFriends(): Promise<Result<OutgoingFriendResponse[]>> {
        return this.getAsync<OutgoingFriendResponse[]>('friends/outgoing');
    }
}