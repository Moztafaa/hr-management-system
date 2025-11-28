import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';

declare var eva: any;

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet],
  template: '<router-outlet></router-outlet>',
  styleUrl: './app.css',
})
export class App implements OnInit {
  ngOnInit() {
    setTimeout(() => {
      eva.replace();
    }, 100);
  }
}
