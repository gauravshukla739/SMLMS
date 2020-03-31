import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { SharedService } from 'src/app/shared/services/shared.service.';



@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
    constructor(
        private router: Router,
        private sharedService: SharedService
    ) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        // const currentUser = this.authenticationService.currentUserValue;
        // if (currentUser) {
        //     // logged in so return true
        //     return true;
        // }
        // not logged in so redirect to login page with the return url
        if (this.sharedService.accessToken != null && this.sharedService.accessToken.length > 0)
            return true;
        else {
            this.router.navigate(['/secure']);
            return false;
        }

    }
}