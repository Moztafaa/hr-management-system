import { Component, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { Employee } from '../../../Services/employee/employee';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-update-employee-image',
  imports: [FormsModule, CommonModule],
  templateUrl: './update-employee-image.html',
  styleUrl: './update-employee-image.css',
})
export class UpdateEmployeeImage implements AfterViewInit {

  employeeId!: number;
  capturedImage: string | null = null; // لتخزين الصورة قبل الرفع
  @ViewChild('video') video!: ElementRef<HTMLVideoElement>;
  @ViewChild('canvas') canvas!: ElementRef<HTMLCanvasElement>;

  constructor(private employeeService: Employee) { }
  ngAfterViewInit() {
    this.startCamera(); // تبدأ الكاميرا تلقائي
  }
  // Start the camera
  startCamera() {
    navigator.mediaDevices.getUserMedia({ video: true })
      .then(stream => {
        this.video.nativeElement.srcObject = stream;
        this.video.nativeElement.play();
      })
      .catch(err => console.error("Camera error:", err));
  }

  captureImageAddUpload() {
    const video = this.video.nativeElement;
    const canvas = this.canvas.nativeElement;

    canvas.width = 112;
    canvas.height = 112;

    const ctx = canvas.getContext('2d')!;
    ctx.drawImage(video, 0, 0, canvas.width, canvas.height);

    this.capturedImage = canvas.toDataURL("image/jpeg"); // نعرض الصورة كـ preview
    console.log("Captured Image:", this.capturedImage);
    if (!this.employeeId) {
      alert("Please enter Employee ID");
      return;
    }

    if (!this.capturedImage) {
      alert("Please capture an image first!");
      return;
    }
    canvas.toBlob(blob => {
      if (!blob) return alert('Capture failed');
      const file = new File([blob], 'capture.jpg', { type: 'image/jpeg' });

      this.employeeService.updateEmployeeImage(this.employeeId, file)
        .subscribe({
          next: res => {
            if (res.employee == false) {
              alert("❌ " + res.errorMassage);
              alert("❌ Failed to update employee image");
            }
            else {
              alert('✔️ Employee Image Updated Successfully');
            }

            this.capturedImage = null; // إعادة تعيين بعد الرفع
          },
          error: err => {
            console.error(err);
            alert("❌ Failed to update employee image");
          }
        });
    }, 'image/jpeg', 0.9);
  }
}