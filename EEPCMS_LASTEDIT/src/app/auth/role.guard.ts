import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { UserService } from 'src/app/service/user.service';
import { map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {

  constructor(private userService: UserService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    const expectedRoles = route.data['expectedRoles'] || [];

    return this.userService.currentUser.pipe(
      map(user => {
        if (user) {
          const hasRole = user.roles.some(role => expectedRoles.includes(role));
          if (hasRole) {
            return true;
          } else {
            this.router.navigate(['/404']);
            return false;
          }
        } else {
          this.router.navigate(['/login']);
          return false;
        }
      }),
      catchError(error => {
        console.error('Error checking user roles:', error);
        this.router.navigate(['/login']);
        return of(false);
      })
    );
  }
}
