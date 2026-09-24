import { Injectable } from "@angular/core";
import { FriendResponse } from "../models/friend.response";
import { Result } from "@crossdyne/toolkit";
import { HttpService } from "../../../core/http/http.service";

@Injectable({
    providedIn: 'root'
})
export class FriendsService extends HttpService {

    constructor() {
        super('');
    }

    friends(): Promise<Result<FriendResponse[]>> {
        return this.getAsync<FriendResponse[]>('friends');
    }
}