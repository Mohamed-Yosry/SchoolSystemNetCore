import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable, of, switchMap } from "rxjs";
import { AuthService } from "../auth.service";

@Injectable({
    providedIn: 'root'
})
export class Authguard implements CanActivate {
    constructor(private _authService: AuthService, private _router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
        return this._authService.check()
            .pipe(
                switchMap((response: boolean) => {
                    if (response)
                        return of(true);
                    // navigate to login page
                    this._router.navigate(['/sign-in']);
                    return of(false);
                })
            );
    }
}
