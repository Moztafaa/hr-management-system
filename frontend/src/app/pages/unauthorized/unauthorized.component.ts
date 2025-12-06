import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../Services/auth.service';

@Component({
  selector: 'app-unauthorized',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <div class="unauthorized-container">
      <div class="unauthorized-card">
        <div class="icon-wrapper">
          <i class="eva eva-lock-outline"></i>
        </div>
        
        <h1 class="title">Access Denied</h1>
        <p class="subtitle">You don't have permission to access this page.</p>
        
        <div class="details">
          <p>This area is restricted to authorized personnel only.</p>
          <p *ngIf="userRole" class="role-info">
            Your current role: <span class="role-badge">{{ userRole }}</span>
          </p>
        </div>
        
        <div class="actions">
          <button class="btn-primary" (click)="goToHome()">
            <i class="eva eva-home-outline"></i>
            Go to Home
          </button>
          <button class="btn-secondary" (click)="goBack()">
            <i class="eva eva-arrow-back-outline"></i>
            Go Back
          </button>
        </div>
        
        <div class="help-text">
          <p>If you believe you should have access, please contact your administrator.</p>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .unauthorized-container {
      min-height: 100vh;
      display: flex;
      align-items: center;
      justify-content: center;
      background: linear-gradient(135deg, var(--body-bg) 0%, var(--color-basic-900) 100%);
      padding: 20px;
    }
    
    .unauthorized-card {
      background: var(--card-bg);
      border-radius: 16px;
      padding: 48px;
      text-align: center;
      max-width: 480px;
      width: 100%;
      box-shadow: 0 8px 32px rgba(0, 0, 0, 0.3);
      border: 1px solid var(--border-basic);
    }
    
    .icon-wrapper {
      width: 100px;
      height: 100px;
      border-radius: 50%;
      background: linear-gradient(135deg, var(--color-danger) 0%, var(--color-danger-active) 100%);
      display: flex;
      align-items: center;
      justify-content: center;
      margin: 0 auto 24px;
      box-shadow: 0 4px 20px rgba(255, 61, 113, 0.3);
    }
    
    .icon-wrapper i {
      font-size: 48px;
      color: white;
    }
    
    .title {
      font-size: 28px;
      font-weight: 700;
      color: var(--text-color);
      margin: 0 0 8px;
    }
    
    .subtitle {
      font-size: 16px;
      color: var(--text-hint);
      margin: 0 0 24px;
    }
    
    .details {
      background: var(--input-bg);
      border-radius: 12px;
      padding: 20px;
      margin-bottom: 24px;
      border: 1px solid var(--border-basic);
    }
    
    .details p {
      color: var(--text-hint);
      margin: 0 0 8px;
      font-size: 14px;
    }
    
    .details p:last-child {
      margin-bottom: 0;
    }
    
    .role-info {
      margin-top: 12px !important;
    }
    
    .role-badge {
      display: inline-block;
      background: var(--color-primary);
      color: white;
      padding: 4px 12px;
      border-radius: 20px;
      font-size: 12px;
      font-weight: 600;
      margin-left: 8px;
    }
    
    .actions {
      display: flex;
      gap: 12px;
      justify-content: center;
      flex-wrap: wrap;
      margin-bottom: 24px;
    }
    
    .btn-primary,
    .btn-secondary {
      display: inline-flex;
      align-items: center;
      gap: 8px;
      padding: 12px 24px;
      border-radius: 8px;
      font-size: 14px;
      font-weight: 600;
      cursor: pointer;
      transition: all var(--transition-fast);
      border: none;
    }
    
    .btn-primary {
      background: linear-gradient(135deg, var(--color-primary) 0%, var(--color-primary-active) 100%);
      color: white;
    }
    
    .btn-primary:hover {
      background: linear-gradient(135deg, var(--color-primary-hover) 0%, var(--color-primary) 100%);
      transform: translateY(-2px);
      box-shadow: 0 4px 12px rgba(51, 102, 255, 0.4);
    }
    
    .btn-secondary {
      background: transparent;
      color: var(--text-color);
      border: 1px solid var(--border-basic);
    }
    
    .btn-secondary:hover {
      background: var(--hover-bg);
      border-color: var(--color-primary);
    }
    
    .btn-primary i,
    .btn-secondary i {
      font-size: 18px;
    }
    
    .help-text {
      border-top: 1px solid var(--border-basic);
      padding-top: 20px;
    }
    
    .help-text p {
      color: var(--text-hint);
      font-size: 13px;
      margin: 0;
    }
    
    @media (max-width: 480px) {
      .unauthorized-card {
        padding: 32px 24px;
      }
      
      .icon-wrapper {
        width: 80px;
        height: 80px;
      }
      
      .icon-wrapper i {
        font-size: 36px;
      }
      
      .title {
        font-size: 24px;
      }
      
      .actions {
        flex-direction: column;
      }
      
      .btn-primary,
      .btn-secondary {
        width: 100%;
        justify-content: center;
      }
    }
  `]
})
export class UnauthorizedComponent {
  private router = inject(Router);
  private authService = inject(AuthService);

  userRole: string | null = null;

  constructor() {
    const roles = this.authService.getRoles();
    if (roles && roles.length > 0) {
      this.userRole = roles.join(', ');
    }
  }

  goToHome(): void {
    const roles = this.authService.getRoles();
    if (roles?.includes('HR') || roles?.includes('Manager')) {
      this.router.navigate(['/pages/dashboard']);
    } else if (roles?.includes('Employee')) {
      this.router.navigate(['/pages/ess/dashboard']);
    } else {
      this.router.navigate(['/']);
    }
  }

  goBack(): void {
    window.history.back();
  }
}
