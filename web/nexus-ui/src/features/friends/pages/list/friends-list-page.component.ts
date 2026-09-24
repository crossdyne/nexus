import { Component, inject, OnInit, signal } from '@angular/core';
import { FriendResponse } from '../../models/friend.response';
import { FriendsService } from '../../services/friends.service';
import { Result, Unit } from '@crossdyne/toolkit';
import { MapErrorsHelper } from '../../../../core/helpers/map-errors.helper';
import { Dialog } from '@angular/cdk/dialog';
import { ConfirmFormComponent } from '../../../../shared/ui/confirm-form/confirm-form';
import { ConfirmFormData } from '../../../../shared/ui/confirm-form/model/confirm-form.data';
import { ConfirmFormResult } from '../../../../shared/ui/confirm-form/model/confirm-form.result';

@Component({
    selector: 'app-friends-list-page.component',
    templateUrl: './friends-list-page.component.html',
    styleUrls: ['./friends-list-page.component.scss'],
    standalone: true,
})
export class FriendsListPageComponent implements OnInit {
    private friendsService = inject(FriendsService);
    private dialog = inject(Dialog);

    friends = signal<FriendResponse[]>([]);

    ngOnInit(): void {
        this.loadFriends();
    }

    async deleteFriend(friend: FriendResponse) {
        const dialogRef = this.dialog.open<ConfirmFormResult, ConfirmFormData, ConfirmFormComponent>(
            ConfirmFormComponent,{
                disableClose: true,
                hasBackdrop: true,
                data: {
                    title: `Вы точно хотите удалить ${friend.userName}? `,
                    body: 'После удаление вам прийдется заново отправлять запрос на дружбу.'
                }
            }
        );

        dialogRef.closed.subscribe(async result => {
            if (!result)
                return;

            if (result.status === 'ok') {
                const result: Result<Unit> = await this.friendsService.removeFriend(friend.userId);

                result.match(
                    unit => this.friends.update(friends => friends.filter(f => f.userId !== friend.userId)),
                    errors => console.error(MapErrorsHelper.mapErrors(errors))
                );
            }
        });
    }

    async loadFriends() {
        const result: Result<FriendResponse[]> = await this.friendsService.friends(); 

        result.match(
            friends => this.friends.set(friends),
            errors => console.error(MapErrorsHelper.mapErrors(errors))
        );
    }
}