import { Component, inject } from '@angular/core';
import { HeaderComponent } from '../../Components/header/header.component';
import { SidebarComponent } from '../../Components/sidebar/sidebar.component';
import { LayoutService } from '../../Services/layout.service';
import { LoginComponent } from '../../Components/auth/login/login.component';
import { RegisterComponent } from '../../Components/auth/register/register.component';
import { ToastComponent } from '../../Components/toast/toast.component';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';

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
    ToastComponent,
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
