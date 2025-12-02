import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { LandingHeaderComponent } from '../landing-header/landing-header.component';
import { LandingFooterComponent } from '../landing-footer/landing-footer.component';
import { CareerSectionComponent } from '../career-section/career-section.component';
import { LoginModalComponent } from '../login-modal/login-modal.component';
import { JobApplyModalComponent } from '../job-apply-modal/job-apply-modal.component';
import { JobPosting } from '../../../models/recruitment/job-posting';

@Component({
  selector: 'app-landing-page',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    LandingHeaderComponent,
    LandingFooterComponent,
    CareerSectionComponent,
    LoginModalComponent,
    JobApplyModalComponent,
  ],
  templateUrl: './landing-page.component.html',
  styleUrl: './landing-page.component.css',
})
export class LandingPageComponent {
  private router = inject(Router);

  showLoginModal = signal(false);
  showApplyModal = signal(false);
  selectedJob = signal<JobPosting | null>(null);

  features = [
    {
      icon: 'eva eva-people-outline',
      title: 'Employee Management',
      description:
        'Comprehensive employee records, profiles, and organizational hierarchy management.',
    },
    {
      icon: 'eva eva-calendar-outline',
      title: 'Leave Management',
      description: 'Streamlined leave requests, approvals, and balance tracking for all employees.',
    },
    {
      icon: 'eva eva-credit-card-outline',
      title: 'Payroll Processing',
      description:
        'Automated salary calculations with allowances, deductions, and payslip generation.',
    },
    {
      icon: 'eva eva-briefcase-outline',
      title: 'Recruitment',
      description: 'End-to-end hiring workflow from job postings to candidate onboarding.',
    },
    {
      icon: 'eva eva-trending-up-outline',
      title: 'Performance Reviews',
      description: 'Goal tracking, KPIs, and 360-degree feedback for employee growth.',
    },
    {
      icon: 'eva eva-book-open-outline',
      title: 'Training & Development',
      description: 'Course management and employee skill development tracking.',
    },
  ];

  openLoginModal(): void {
    this.showLoginModal.set(true);
  }

  closeLoginModal(): void {
    this.showLoginModal.set(false);
  }

  onLoginSuccess(): void {
    this.showLoginModal.set(false);
    this.router.navigate(['/pages/dashboard']);
  }

  scrollToCareers(): void {
    const element = document.getElementById('careers');
    if (element) {
      element.scrollIntoView({ behavior: 'smooth' });
    }
  }

  openApplyModal(job: JobPosting): void {
    this.selectedJob.set(job);
    this.showApplyModal.set(true);
  }

  closeApplyModal(): void {
    this.showApplyModal.set(false);
    this.selectedJob.set(null);
  }

  onApplicationSuccess(): void {
    this.closeApplyModal();
  }
}
