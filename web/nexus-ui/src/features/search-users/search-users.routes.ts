import { Routes } from "@angular/router";
import { MainLayoutComponent } from "../../core/layout/main/main-layout.component";

export const SEARCH_USERS_ROUTES: Routes = [
    {
        path: '',
        component: MainLayoutComponent,
        children: [
            { 
                path: '', 
                loadComponent: () => import('./pages/search/search-page.component').then(c => c.SearchPageComponent)
            }
        ]
    }
]