import { Component, signal } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { Navbar } from './shared/navbar/navbar';
import { NgIf } from '@angular/common';
import { NotificationService } from './core/services/notification-service';
import { Notification } from './features/models/notification';
import { MatIcon, MatIconModule } from '@angular/material/icon';
@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Navbar, NgIf, MatIconModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('frontend');
  currentNotification = signal<Notification | null>(null);
  private timeout: any;
  constructor(private router: Router, private notificationService:NotificationService){
    const token = localStorage.getItem('token')
    if(token){
      notificationService.startConnection();
      notificationService.getNotifications();
    }
  }
  
  ngOnInit(){
    const token = localStorage.getItem('token');
    if(token){
      this.notificationService.notification$.subscribe(notifications=>{
        if(notifications.length === 0) return;

        const notification = notifications[0];

        this.currentNotification.set(notification);

        clearTimeout(this.timeout)

        this.timeout = setTimeout(()=>{
          this.currentNotification.set(null)
        }, 4000);
      });
    }
  }
  
  showNavbar(): boolean{
    return this.router.url !== '/login' && this.router.url !== '/register';
  }
}
