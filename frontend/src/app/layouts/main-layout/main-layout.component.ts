import { Component, inject, OnInit, OnDestroy, PLATFORM_ID, Inject } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { HeaderComponent } from '../../Components/header/header.component';
import { SidebarComponent } from '../../Components/sidebar/sidebar.component';
import { LayoutService } from '../../Services/layout.service';
import { LoginComponent } from '../../Components/auth/login/login.component';
import { RegisterComponent } from '../../Components/auth/register/register.component';
import { ToastComponent } from '../../Components/toast/toast.component';
import { ChatbotComponent } from '../../Components/chatbot/chatbot.component';
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
    ChatbotComponent,
  ],
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.css'],
})
export class MainLayoutComponent implements OnInit, OnDestroy {
  private layoutService = inject(LayoutService);
  isSidebarOpen = this.layoutService.isSidebarOpen;
  isLoginOpen = this.layoutService.isLoginOpen;
  isRegisterOpen = this.layoutService.isRegisterOpen;

  constructor(@Inject(PLATFORM_ID) private platformId: Object) {}

  ngOnInit(): void {
    if (isPlatformBrowser(this.platformId)) {
      document.body.classList.add('authenticated-layout');
    }
  }

  ngOnDestroy(): void {
    if (isPlatformBrowser(this.platformId)) {
      document.body.classList.remove('authenticated-layout');
    }
  }
}
