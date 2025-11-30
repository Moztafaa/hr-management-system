import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LayoutService {
  isSidebarOpen = signal(true);
  isLoginOpen = signal(false);
  isRegisterOpen = signal(false);

  toggleSidebar() {
    this.isSidebarOpen.update((value) => !value);
  }

  openLogin() {
    this.isLoginOpen.set(true);
    this.isRegisterOpen.set(false);
  }

  closeLogin() {
    this.isLoginOpen.set(false);
  }

  openRegister() {
    this.isRegisterOpen.set(true);
    this.isLoginOpen.set(false);
  }

  closeRegister() {
    this.isRegisterOpen.set(false);
  }
}
