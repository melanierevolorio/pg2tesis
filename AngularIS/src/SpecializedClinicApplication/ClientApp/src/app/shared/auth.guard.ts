import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable, map, catchError, of } from 'rxjs';
import { User } from '../../models/user.model';
import { AuthService, UserClaim } from '../../services/authentication.service';
import { UserClaimKeys } from '../../services/claim-keys';
import { LocalService } from '../../services/local.service';

@Injectable({
  providedIn: 'root'
})

export class AuthGuard implements CanActivate {

  constructor(private router: Router, private authService: AuthService, private local: LocalService) {
  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.authService.getUserData()
      .pipe(
        map(response => {
          this.local.saveJsonData("currentUser", response);
          return true;
        }),
        catchError(err => {
          this.router.navigateByUrl('/login');
          throw 'error in source. Details: ' + err;
        })
      );
  }
}
