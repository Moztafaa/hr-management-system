import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from '../../components/header/header.component';
import { SidebarComponent } from '../../components/sidebar/sidebar.component';
import { LayoutService } from '../../services/layout.service';
import { LoginComponent } from '../../components/auth/login/login.component';
import { RegisterComponent } from '../../components/auth/register/register.component';

@Component({
  selector: 'app-main-layout',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    HeaderComponent,
    SidebarComponent,
    LoginComponent,
    RegisterComponent,
  ],
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.css'],
})
export class MainLayoutComponent {
  private layoutService = inject(LayoutService);
  isSidebarOpen = this.layoutService.isSidebarOpen;
  isLoginOpen = this.layoutService.isLoginOpen;
  isRegisterOpen = this.layoutService.isRegisterOpen;
}
