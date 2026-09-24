import { Component, inject, OnInit, signal } from '@angular/core';
import { FriendResponse } from '../../models/friend.response';
import { FriendsService } from '../../services/friends.service';
import { Result } from '@crossdyne/toolkit';
import { MapErrorsHelper } from '../../../../core/helpers/map-errors.helper';

@Component({
    selector: 'app-friends-list-page.component',
    templateUrl: './friends-list-page.component.html',
    styleUrls: ['./friends-list-page.component.scss'],
    standalone: true,
})
export class FriendsListPageComponent implements OnInit {
    private friendsService = inject(FriendsService);
    
    friends = signal<FriendResponse[]>([]);

    ngOnInit(): void {
        this.loadFriends();
    }

    async loadFriends() {
        const result: Result<FriendResponse[]> = await this.friendsService.friends(); 

        result.match(
            friends => this.friends.set(friends),
            errors => console.error(MapErrorsHelper.mapErrors(errors)));
    }
}