import { Component, inject, input } from "@angular/core";
import { CommonModule } from "@angular/common";
import { Router } from "@angular/router";

@Component({
    selector: 'profile-info',
    standalone: true,
    templateUrl: './profile-info.component.html',
    styleUrls: ['./profile-info.component.scss'],
    imports: [CommonModule]
})
export class ProfileInfoComponent {
    private router = inject(Router);

    login = input.required<string>();
    email = input.required<string>();
    friendshipCode = input.required<string>();
    dataRegistration = input.required<Date>();

    private readonly copyMap = new Map<string, () => string>([
        ['login', () => this.login()],
        ['email', () => this.email()],
        ['friendshipCode', () => this.friendshipCode()]
    ]);

    async copy(elementToCopy: 'login' | 'email' | 'friendshipCode' | null){
         if (!elementToCopy)
            return;   

         const getter = this.copyMap.get(elementToCopy);

        if (getter)
            await navigator.clipboard.writeText(getter());
    }

    async navigateChangeEmail() {
        this.router.navigate(['/change/email']);
    }
}