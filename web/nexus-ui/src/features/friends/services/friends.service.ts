import { Injectable } from "@angular/core";
import { FriendResponse } from "../models/friend.response";
import { Result, Unit } from "@crossdyne/toolkit";
import { HttpService } from "../../../core/http/http.service";

@Injectable({
    providedIn: 'root'
})
export class FriendsService extends HttpService {

    constructor() {
        super('');
    }

    async removeFriend(friendId: string): Promise<Result<Unit>> {
        return this.deleteAsync(`friends/${friendId}`);
    }

    async friends(): Promise<Result<FriendResponse[]>> {
        return this.getAsync<FriendResponse[]>('friends');
    }
}