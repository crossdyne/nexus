import { Routes } from "@angular/router";
import { MainLayoutComponent } from "../../core/layout/main/main-layout.component";

export const FRIENDS_ROUTES: Routes = [
    {
        path: '',
        component: MainLayoutComponent,
        children: [
            { 
                path: '', 
                loadComponent: () => import('./pages/requests/requests-page.component').then(c => c.RequestsPageComponent)
            }
        ]
    }
];