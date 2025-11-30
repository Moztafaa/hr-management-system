import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { LayoutService } from '../../Services/layout.service';
import { AuthService } from '../../Services/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent {
  private layoutService = inject(LayoutService);
  private authService = inject(AuthService);
  private router = inject(Router);

  toggleSidebar() {
    this.layoutService.toggleSidebar();
  }
  goTocheckOut() {
    this.router.navigate(['/pages/checkOut']);
  }
  goToCheckIn() {
    this.router.navigate(['/pages/checkIn']);
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/pages/login']);
  }
}
