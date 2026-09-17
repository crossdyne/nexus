import { Component, inject, model, signal } from "@angular/core";
import { SearchService } from "../../services/search.service";
import { SearchUserResponse } from "../../models/search-user.response";
import { FormsModule } from "@angular/forms";
import { Result, Unit } from "@crossdyne/toolkit";
import { MapErrorsHelper } from "../../../../core/helpers/map-errors.helper";
import { FriendsRequestService } from "../../services/friends-request.service";
import { SendFriendRequest } from "../../models/send-friend-request.request";

@Component({
    selector: 'search-users-page',
    templateUrl: './search-page.component.html',
    styleUrls: ['./search-page.component.scss'],
    standalone: true,
    imports: [
        FormsModule
    ]
})
export class SearchPageComponent {
    private searchService = inject(SearchService);
    private friendRequestService = inject(FriendsRequestService);

    input = model<string>('');
    users = signal<SearchUserResponse[]>([]);

    async search() {
        const result: Result<SearchUserResponse[]> = await this.searchService.search(this.input());

        result.match(
            users => this.users.set(users),
            errors => console.error(MapErrorsHelper.mapErrors(errors))
        );
    }

    async sendRequest(inviteCode: string) {
        const request: SendFriendRequest = {
            inviteCode: inviteCode,
        }

        const result: Result<Unit> = await this.friendRequestService.sendAsync(request);

        result.match(
            () => this.users.update(users => users.map(u => u.inviteCode === inviteCode ? {...u, isSend: true} : u)),
            errors => console.error(MapErrorsHelper.mapErrors(errors)) 
        );
    }
}