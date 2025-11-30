import { Component, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { Attendance } from '../../../Services/attendance/attendance';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Employee } from '../../../Services/employee/employee';
@Component({
  selector: 'app-check-out',
  imports: [FormsModule, CommonModule],
  templateUrl: './check-out.html',
  styleUrl: './check-out.css',
})
export class CheckOut implements AfterViewInit {

  employeeId!: number;
  capturedImage: string | null = null; // لتخزين الصورة قبل الرفع
  @ViewChild('video') video!: ElementRef<HTMLVideoElement>;
  @ViewChild('canvas') canvas!: ElementRef<HTMLCanvasElement>;

  constructor(private attendanceService: Attendance, private employeeService: Employee) { }
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

  captureImageAndUpload() {
    const video = this.video.nativeElement;
    const canvas = this.canvas.nativeElement;

    canvas.width = 112;
    canvas.height = 112;

    const ctx = canvas.getContext('2d')!;
    ctx.drawImage(video, 0, 0, canvas.width, canvas.height);

    this.capturedImage = canvas.toDataURL("image/jpeg"); // نعرض الصورة كـ preview
    console.log("Captured Image:", this.capturedImage);
    this.employeeId = this.employeeService.getEmployeeId();
    console.log("Employee ID:", this.employeeId);
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

      this.attendanceService.checkOut(this.employeeId, file)
        .subscribe({
          next: res => {
            if (res.id == -1) {
              console.log(res.errorMassage);
              alert("❌ " + res.errorMassage);
            }
            else {
              alert('✔️ checkOut Successfully');
            }
            this.capturedImage = null;
          },
          error: err => {
            console.error(err);
            alert("❌ Failed to checkOut");
          }
        });
    }, 'image/jpeg', 0.9);
  }
}