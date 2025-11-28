import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { LayoutService } from '../../services/layout.service';
import { AuthService } from '../../services/auth.service';

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

  logout() {
    this.authService.logout();
    this.router.navigate(['/pages/login']);
  }
}
