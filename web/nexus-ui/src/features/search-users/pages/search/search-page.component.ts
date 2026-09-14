import { Component, inject, model, signal } from "@angular/core";
import { SearchService } from "../../services/search.service";
import { SearchUserResponse } from "../../models/search-user.response";
import { FormsModule } from "@angular/forms";
import { Result } from "@crossdyne/toolkit";
import { MapErrorsHelper } from "../../../../core/helpers/map-errors.helper";

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

    input = model<string>('');
    users = signal<SearchUserResponse[]>([]);

    async search() {
        const result: Result<SearchUserResponse[]> = await this.searchService.search(this.input());

        result.match(
            users => this.users.set(users),
            errors => console.error(MapErrorsHelper.mapErrors(errors))
        );
    }
}