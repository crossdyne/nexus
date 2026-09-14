import { Injectable } from "@angular/core";
import { HttpService } from "../../../core/http/http.service";
import { Result } from "@crossdyne/toolkit";
import { HttpParams } from "@angular/common/http";
import { SearchUserResponse } from "../models/search-user.response";

@Injectable({ 
    providedIn: 'root' 
})
export class SearchService extends HttpService {
    constructor() {
        super('')
    }

    async search(input: string) : Promise<Result<SearchUserResponse[]>> {
        return await this.getAsync<SearchUserResponse[]>('/users/search', new HttpParams({ fromObject: { input: input } }));
    }
}