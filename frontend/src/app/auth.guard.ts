import { Injectable, inject } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router,
  UrlTree,
} from '@angular/router';
import { AuthService } from './Services/auth.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  private authService = inject(AuthService);
  private router = inject(Router);

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean | UrlTree {
    // 1) لو مش Logged in → روح للـ login
    if (!this.authService.isLoggedIn()) {
      return this.router.createUrlTree(['/pages/login']);
    }

    // 2) لو فيه أدوار مطلوبة على الـ route
    const requiredRoles = route.data?.['roles'] as string[] | undefined;

    if (!requiredRoles || requiredRoles.length === 0) {
      // مفيش roles محددة → أي حد Logged in يدخل
      return true;
    }

    const allowed = this.authService.hasAnyRole(requiredRoles);

    if (!allowed) {
      // Redirect to unauthorized page instead of alert
      return this.router.createUrlTree(['/pages/unauthorized']);
    }

    return true;
  }
}
