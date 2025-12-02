import { Component, inject, HostListener } from '@angular/core';
import { LayoutService } from '../../Services/layout.service';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';

export interface MenuItem {
  title: string;
  icon?: string;
  link?: string;
  group?: boolean;
  children?: MenuItem[];
}

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css'],
})
export class SidebarComponent {
  private layoutService = inject(LayoutService);
  private router = inject(Router);
  isSidebarOpen = this.layoutService.isSidebarOpen;

  menuItems: MenuItem[] = [
    { title: 'Dashboard', icon: 'home-outline', link: '/pages/dashboard' },
    {
      title: 'Employee Management',
      icon: 'people-outline',
      children: [
        { title: 'Employee List', link: '/pages/employees' },
        { title: 'Add Employee', link: '/pages/employees/create' },
      ],
    },
    {
      title: 'Organization',
      icon: 'briefcase-outline',
      children: [
        { title: 'Departments', link: '/pages/departments' },
        { title: 'Designations', link: '/pages/designations' },
      ],
    },
    {
      title: 'Attendance',
      icon: 'clock-outline',
      children: [
        { title: 'Check In/Out', link: '/pages/attendance/check' },
        { title: 'Attendance Records', link: '/pages/attendance/records' },
      ],
    },
    {
      title: 'Leave Management',
      icon: 'calendar-outline',
      children: [
        { title: 'Leave Types', link: '/pages/leave/types' },
        { title: 'Leave Requests', link: '/pages/leave/requests' },
        { title: 'Leave Approvals', link: '/pages/leave/approvals' },
        { title: 'Leave Balances', link: '/pages/leave/balances' },
      ],
    },
    {
      title: 'Payroll',
      icon: 'credit-card-outline',
      children: [
        { title: 'Payslips', link: '/pages/payroll/payslips' },
        { title: 'Allowances', link: '/pages/payroll/allowances' },
        { title: 'Deductions', link: '/pages/payroll/deductions' },
        { title: 'Employee Allowances', link: '/pages/payroll/employee-allowances' },
        { title: 'Employee Deductions', link: '/pages/payroll/employee-deductions' },
      ],
    },
    {
      title: 'Performance',
      icon: 'trending-up-outline',
      children: [
        { title: 'Review Cycles', link: '/pages/performance/cycles' },
        { title: 'Reviews', link: '/pages/performance/reviews' },
        { title: 'Goals', link: '/pages/performance/goals' },
        { title: 'KPIs', link: '/pages/performance/kpis' },
        { title: 'Competencies', link: '/pages/performance/competencies' },
        { title: 'Feedback', link: '/pages/performance/feedback' },
      ],
    },
    {
      title: 'Training',
      icon: 'book-open-outline',
      children: [
        { title: 'Training Courses', link: '/pages/training/courses' },
        { title: 'Employee Enrollments', link: '/pages/training/enrollments' },
        { title: 'Training Requests', link: '/pages/training/requests' },
      ],
    },
    {
      title: 'Recruitment',
      icon: 'person-add-outline',
      children: [
        { title: 'Job Postings', link: '/pages/recruitment/jobs' },
        { title: 'Candidates', link: '/pages/recruitment/candidates' },
        { title: 'Job Applications', link: '/pages/recruitment/applications' },
      ],
    },
    {
      title: 'Reports & Analytics',
      icon: 'bar-chart-outline',
      children: [
        { title: 'Dashboard KPIs', link: '/pages/reports/dashboard' },
        { title: 'Employee Reports', link: '/pages/reports/employees' },
        { title: 'Payroll Reports', link: '/pages/reports/payroll' },
        { title: 'Leave Reports', link: '/pages/reports/leave' },
        { title: 'Recruitment Reports', link: '/pages/reports/recruitment' },
      ],
    },
    { title: 'Notifications', icon: 'bell-outline', link: '/pages/notifications' },
    {
      title: 'Employee Self-Service',
      icon: 'person-outline',
      children: [
        { title: 'My Profile', link: '/pages/ess/profile' },
        { title: 'My Leave Requests', link: '/pages/ess/leave-requests' },
        { title: 'My Training', link: '/pages/ess/training' },
        { title: 'My Payslips', link: '/pages/ess/payslips' },
        { title: 'My Dashboard', link: '/pages/ess/dashboard' },
        { title: 'Update My Image', link: '/pages/updateEmployeeImage' },
      ],
    },
    {
      title: 'Settings',
      icon: 'settings-2-outline',
      children: [{ title: 'Change Password', link: '/pages/account/change-password' }],
    },
  ];

  expandedItems = new Set<string>();
  private isMobile = false;

  constructor() {
    this.checkScreenSize();
  }

  @HostListener('window:resize')
  onResize() {
    this.checkScreenSize();
  }

  private checkScreenSize() {
    this.isMobile = window.innerWidth < 992;
  }

  onMenuItemClick(item: MenuItem, event: Event) {
    if (item.children) {
      event.preventDefault();
      this.toggleItem(item);
    } else {
      // Close sidebar on mobile when navigating
      this.closeSidebarOnMobile();
    }
  }

  toggleItem(item: MenuItem) {
    if (item.children) {
      // If sidebar is closed, open it first
      if (!this.isSidebarOpen()) {
        this.layoutService.isSidebarOpen.set(true);
      }

      if (this.expandedItems.has(item.title)) {
        this.expandedItems.delete(item.title);
      } else {
        this.expandedItems.add(item.title);
      }
    }
  }

  isExpanded(item: MenuItem): boolean {
    return this.expandedItems.has(item.title);
  }

  closeSidebarOnMobile() {
    if (this.isMobile && this.isSidebarOpen()) {
      this.layoutService.isSidebarOpen.set(false);
    }
  }

  isActiveRoute(link?: string): boolean {
    if (!link) return false;
    return this.router.url === link;
  }

  hasActiveChild(item: MenuItem): boolean {
    if (!item.children) return false;
    return item.children.some((child) => child.link && this.router.url.startsWith(child.link));
  }
}
