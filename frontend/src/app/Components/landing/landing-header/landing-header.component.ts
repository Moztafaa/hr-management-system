import { Component, EventEmitter, Output, signal } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-landing-header',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './landing-header.component.html',
  styleUrl: './landing-header.component.css',
})
export class LandingHeaderComponent {
  @Output() loginClick = new EventEmitter<void>();
  @Output() careersClick = new EventEmitter<void>();

  isMobileMenuOpen = signal(false);

  toggleMobileMenu(): void {
    this.isMobileMenuOpen.update((v) => !v);
  }

  closeMobileMenu(): void {
    this.isMobileMenuOpen.set(false);
  }

  onLoginClick(): void {
    this.closeMobileMenu();
    this.loginClick.emit();
  }

  onCareersClick(): void {
    this.closeMobileMenu();
    this.careersClick.emit();
  }

  scrollToSection(sectionId: string): void {
    this.closeMobileMenu();
    const element = document.getElementById(sectionId);
    if (element) {
      element.scrollIntoView({ behavior: 'smooth' });
    }
  }
}
