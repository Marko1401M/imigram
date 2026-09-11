import { Component } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { NotificationService } from '../../core/services/notification-service';
import { NumberInput } from '@angular/cdk/coercion';
import { CommonModule } from '@angular/common';
import { Notification } from '../../features/models/notification';
import { NgIf } from '@angular/common';
import { signal } from '@angular/core';
import { computed } from '@angular/core';
@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterLink, RouterLinkActive, MatIconModule, CommonModule, NgIf],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})
export class Navbar {
  notifications = signal<Notification[]>([]);
  showNotifications = signal(false);
  userId = signal<string>('');
  constructor(private notificationService: NotificationService, private router: Router){}

  ngOnInit(){
    this.notificationService.notification$
    .subscribe(notifications => {
      this.notifications.set(notifications)
      console.log('Notifications: ')
      console.log(notifications);
    })
    const userId = localStorage.getItem('userId');
    if(userId != null){ 
      this.userId.set(userId);
      this.notificationService.getNotifications()
    
    }
    
  }

  toggleNotifications(){
    this.showNotifications.update(value => !value);
  }

  unreadCount = computed(()=>
    this.notifications().filter(n=>!n.isRead).length
  );

  getUnreadCount(): number{
    return this.notifications()?.filter(n => !n.isRead).length ?? 0;
  }

  openNotification(notification: Notification) {

    this.notifications.update(notifications =>
        notifications.map(n =>
            n.id === notification.id
                ? { ...n, isRead: true }
                : n
        )
    );

    this.notificationService
        .markAsRead(notification.id)
        .subscribe();
    if(notification.type =="FollowReq") this.router.navigate(['/followers']);
    else if(notification.type == "Follow") this.router.navigate(['/profile',notification?.senderId]);
    else if(notification.postId) this.router.navigate(['/post-details',notification.postId])
  }
  isAdmin(){
    const role = localStorage.getItem('role');
    
    return role === 'admin';
  }
  logout(){
    localStorage.removeItem('token');
    localStorage.removeItem('userId');
    //localStorage.removeItem('username');
    this.router.navigate(['/login'])
  }
}
