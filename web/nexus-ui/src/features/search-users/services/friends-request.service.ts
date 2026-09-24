import { Injectable } from "@angular/core";
import { HttpService } from "../../../core/http/http.service";
import { Result, Unit } from "@crossdyne/toolkit";
import { SendFriendRequest } from "../models/send-friend-request.request";

@Injectable({
     providedIn: 'root'
})
export class FriendsRequestService extends HttpService {
    constructor() {
        super('');
    }

    async sendAsync(request: SendFriendRequest) : Promise<Result<Unit>> {
        return await this.postAsync('friends/request', request);
    }
}