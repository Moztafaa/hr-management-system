import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutService } from '../../services/layout.service';

export interface MenuItem {
  title: string;
  icon?: string;
  link?: string;
  group?: boolean;
  children?: MenuItem[];
}

import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css'],
})
export class SidebarComponent {
  private layoutService = inject(LayoutService);
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
      title: 'Recruitment',
      icon: 'person-add-outline',
      children: [
        { title: 'Job Postings', link: '/pages/recruitment/jobs' },
        { title: 'Candidates', link: '/pages/recruitment/candidates' },
        { title: 'Job Applications', link: '/pages/recruitment/applications' },
      ],
    },
    { title: 'Notifications', icon: 'bell-outline', link: '/pages/notifications' },
    {
      title: 'Employee Self-Service',
      icon: 'person-outline',
      children: [
        { title: 'My Profile', link: '/pages/ess/profile' },
        { title: 'My Leave Requests', link: '/pages/ess/leave-requests' },
        { title: 'My Payslips', link: '/pages/ess/payslips' },
        { title: 'My Dashboard', link: '/pages/ess/dashboard' },
      ],
    },
    {
      title: 'Settings',
      icon: 'settings-2-outline',
      children: [{ title: 'Change Password', link: '/pages/account/change-password' }],
    },
  ];

  expandedItems = new Set<string>();

  toggleItem(item: any) {
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

  isExpanded(item: any): boolean {
    return this.expandedItems.has(item.title);
  }
}
