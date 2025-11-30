import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastService } from '../../Services/toast.service';

@Component({
  selector: 'app-toast',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './toast.component.html',
  styleUrls: ['./toast.component.css'],
})
export class ToastComponent {
  toastService = inject(ToastService);

  getIcon(type: string): string {
    switch (type) {
      case 'success':
        return 'checkmark-circle-2-outline';
      case 'error':
        return 'close-circle-outline';
      case 'warning':
        return 'alert-triangle-outline';
      case 'info':
      default:
        return 'info-outline';
    }
  }
}
