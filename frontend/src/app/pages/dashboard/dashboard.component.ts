import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Chart, ChartConfiguration, registerables } from 'chart.js';

Chart.register(...registerables);

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7005/api/Reporting';

  // Current date info
  currentYear = new Date().getFullYear();
  currentMonth = new Date().getMonth() + 1;

  // Stats
  stats = {
    totalEmployees: 0,
    totalDepartments: 0,
    activeLeaveRequests: 0,
    activeJobPostings: 0,
  };

  // Charts
  departmentChart: Chart | null = null;
  designationChart: Chart | null = null;
  leaveStatusChart: Chart | null = null;
  attendanceTrendChart: Chart | null = null;

  // Data
  recentActivities: any[] = [];
  isLoading = true;
  error: string | null = null;

  ngOnInit() {
    this.loadDashboardData();
  }

  loadDashboardData() {
    this.isLoading = true;

    // Load total employees - raw number response
    this.http.get<number>(`${this.apiUrl}/TotalEmployees`).subscribe({
      next: (res) => {
        this.stats.totalEmployees = res;
      },
      error: (err) => console.error('Error loading total employees:', err),
    });

    // Load employees by department - object { "DeptName": count }
    this.http.get<{ [key: string]: number }>(`${this.apiUrl}/EmployeesByDepartment`).subscribe({
      next: (res) => {
        if (res) {
          const deptData = Object.entries(res).map(([name, count]) => ({
            departmentName: name,
            employeeCount: count,
          }));
          this.stats.totalDepartments = deptData.length;
          setTimeout(() => this.createDepartmentChart(deptData), 100);
        }
      },
      error: (err) => console.error('Error loading dept breakdown:', err),
    });

    // Load employees by designation - object { "Title": count }
    this.http.get<{ [key: string]: number }>(`${this.apiUrl}/EmployeesByDesignation`).subscribe({
      next: (res) => {
        if (res) {
          const designationData = Object.entries(res).map(([title, count]) => ({
            designationTitle: title,
            employeeCount: count,
          }));
          setTimeout(() => this.createDesignationChart(designationData), 100);
        }
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error loading designation breakdown:', err);
        this.isLoading = false;
      },
    });

    // Load total leave requests - raw number
    this.http.get<number>(`${this.apiUrl}/TotalLeaveRequests`).subscribe({
      next: (res) => {
        this.stats.activeLeaveRequests = res;
      },
      error: (err) => console.error('Error loading leave requests:', err),
    });

    // Load leave requests by status - object { "Status": count }
    this.http.get<{ [key: string]: number }>(`${this.apiUrl}/LeaveRequestsByStatus`).subscribe({
      next: (res) => {
        if (res) {
          const statusData = Object.entries(res).map(([status, count]) => ({
            status: status,
            count: count,
          }));
          setTimeout(() => this.createLeaveStatusChart(statusData), 100);
        }
      },
      error: (err) => console.error('Error loading leave status:', err),
    });

    // Load monthly leave trend
    this.http.get<any[]>(`${this.apiUrl}/MonthlyLeaveTrend/${this.currentYear}`).subscribe({
      next: (res) => {
        if (res && Array.isArray(res)) {
          setTimeout(() => this.createAttendanceTrendChart(res), 100);
        }
      },
      error: (err) => console.error('Error loading leave trend:', err),
    });

    // Load active job postings - raw number
    this.http.get<number>(`${this.apiUrl}/ActiveJobPostings`).subscribe({
      next: (res) => {
        this.stats.activeJobPostings = res;
      },
      error: (err) => console.error('Error loading job postings:', err),
    });

    // Mock recent activities
    this.recentActivities = [
      {
        id: 1,
        type: 'employee',
        description: 'New employee joined the organization',
        timestamp: new Date(Date.now() - 2 * 60 * 60 * 1000),
        icon: 'person-add-outline',
        color: 'success',
      },
      {
        id: 2,
        type: 'leave',
        description: 'Leave request submitted',
        timestamp: new Date(Date.now() - 4 * 60 * 60 * 1000),
        icon: 'calendar-outline',
        color: 'warning',
      },
      {
        id: 3,
        type: 'payroll',
        description: 'Monthly payroll processing completed',
        timestamp: new Date(Date.now() - 6 * 60 * 60 * 1000),
        icon: 'credit-card-outline',
        color: 'info',
      },
      {
        id: 4,
        type: 'training',
        description: 'Training course enrollment completed',
        timestamp: new Date(Date.now() - 8 * 60 * 60 * 1000),
        icon: 'book-outline',
        color: 'primary',
      },
      {
        id: 5,
        type: 'recruitment',
        description: 'New job applications received',
        timestamp: new Date(Date.now() - 10 * 60 * 60 * 1000),
        icon: 'briefcase-outline',
        color: 'secondary',
      },
    ];
  }

  createDepartmentChart(data: any[]) {
    const ctx = document.getElementById('departmentChart') as HTMLCanvasElement;
    if (!ctx) return;

    const labels = data.map((d) => d.departmentName);
    const counts = data.map((d) => d.employeeCount);

    const config: ChartConfiguration = {
      type: 'bar',
      data: {
        labels: labels,
        datasets: [
          {
            label: 'Employees',
            data: counts,
            backgroundColor: 'rgba(51, 102, 255, 0.8)',
            borderColor: 'rgba(51, 102, 255, 1)',
            borderWidth: 2,
            borderRadius: 8,
          },
        ],
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
          legend: {
            display: false,
          },
          title: {
            display: true,
            text: 'Employees by Department',
            font: { size: 16, weight: 'bold' },
            color: '#ffffff',
          },
        },
        scales: {
          y: {
            beginAtZero: true,
            ticks: {
              stepSize: 1,
              color: '#8f9bb3'
            },
            grid: {
              color: 'rgba(143, 155, 179, 0.1)',
            },
          },
          x: {
            ticks: {
              color: '#8f9bb3'
            },
            grid: {
              display: false,
            },
          },
        },
      },
    };

    if (this.departmentChart) {
      this.departmentChart.destroy();
    }
    this.departmentChart = new Chart(ctx, config);
  }

  createDesignationChart(data: any[]) {
    const ctx = document.getElementById('designationChart') as HTMLCanvasElement;
    if (!ctx) return;

    // Filter out designations with 0 employees for cleaner chart
    const filteredData = data.filter(d => d.employeeCount > 0);
    const labels = filteredData.map((d) => d.designationTitle);
    const counts = filteredData.map((d) => d.employeeCount);

    const colors = [
      '#3366ff', '#00d68f', '#0095ff', '#ffaa00', '#ff3d71',
      '#a855f7', '#10b981', '#f59e0b', '#ef4444', '#8b5cf6',
      '#ec4899', '#14b8a6', '#f97316', '#6366f1', '#84cc16'
    ];

    const config: ChartConfiguration = {
      type: 'doughnut',
      data: {
        labels: labels,
        datasets: [
          {
            data: counts,
            backgroundColor: colors.slice(0, labels.length),
            hoverOffset: 10,
            borderWidth: 0,
          },
        ],
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
          legend: {
            position: 'bottom',
            labels: {
              padding: 15,
              font: { size: 11 },
              color: '#ffffff'
            },
          },
          title: {
            display: true,
            text: 'Designation Distribution',
            font: { size: 16, weight: 'bold' },
            color: '#ffffff',
          },
        },
      },
    };

    if (this.designationChart) {
      this.designationChart.destroy();
    }
    this.designationChart = new Chart(ctx, config);
  }

  createLeaveStatusChart(data: any[]) {
    const ctx = document.getElementById('leaveStatusChart') as HTMLCanvasElement;
    if (!ctx) return;

    const labels = data.map((d) => d.status);
    const counts = data.map((d) => d.count);

    const statusColors: any = {
      'Pending': '#ffaa00',
      'Approved': '#00d68f',
      'Rejected': '#ff3d71',
      'Cancelled': '#8f9bb3',
    };

    const backgroundColors = labels.map(label => statusColors[label] || '#3366ff');

    const config: ChartConfiguration = {
      type: 'pie',
      data: {
        labels: labels,
        datasets: [
          {
            data: counts,
            backgroundColor: backgroundColors,
            hoverOffset: 8,
            borderWidth: 0,
          },
        ],
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
          legend: {
            position: 'bottom',
            labels: {
              padding: 15,
              font: { size: 12 },
              color: '#ffffff'
            },
          },
          title: {
            display: true,
            text: 'Leave Requests Status',
            font: { size: 16, weight: 'bold' },
            color: '#ffffff',
          },
        },
      },
    };

    if (this.leaveStatusChart) {
      this.leaveStatusChart.destroy();
    }
    this.leaveStatusChart = new Chart(ctx, config);
  }

  createAttendanceTrendChart(data: any[]) {
    const ctx = document.getElementById('attendanceTrendChart') as HTMLCanvasElement;
    if (!ctx) return;

    const monthNames = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    const labels = data.map((d) => monthNames[d.month - 1] || d.month);
    const leaveCounts = data.map((d) => d.leaveCount || d.count || 0);

    const config: ChartConfiguration = {
      type: 'line',
      data: {
        labels: labels,
        datasets: [
          {
            label: 'Leave Requests',
            data: leaveCounts,
            borderColor: '#ff3d71',
            backgroundColor: 'rgba(255, 61, 113, 0.1)',
            tension: 0.4,
            fill: true,
            borderWidth: 3,
          },
        ],
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
          legend: {
            position: 'top',
            labels: {
              padding: 15,
              font: { size: 12 },
              color: '#ffffff'
            },
          },
          title: {
            display: true,
            text: 'Monthly Leave Trend',
            font: { size: 16, weight: 'bold' },
            color: '#ffffff',
          },
        },
        scales: {
          y: {
            beginAtZero: true,
            ticks: {
              stepSize: 5,
              color: '#8f9bb3'
            },
            grid: {
              color: 'rgba(143, 155, 179, 0.1)',
            },
          },
          x: {
            ticks: {
              color: '#8f9bb3'
            },
            grid: {
              display: false,
            },
          },
        },
      },
    };

    if (this.attendanceTrendChart) {
      this.attendanceTrendChart.destroy();
    }
    this.attendanceTrendChart = new Chart(ctx, config);
  }

  getTimeAgo(timestamp: Date): string {
    const now = new Date();
    const diff = now.getTime() - new Date(timestamp).getTime();
    const hours = Math.floor(diff / (1000 * 60 * 60));

    if (hours < 1) return 'Just now';
    if (hours === 1) return '1 hour ago';
    if (hours < 24) return `${hours} hours ago`;

    const days = Math.floor(hours / 24);
    if (days === 1) return '1 day ago';
    return `${days} days ago`;
  }
}
