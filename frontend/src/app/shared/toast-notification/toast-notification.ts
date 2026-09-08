import { CommonModule } from '@angular/common';
import { Component, OnDestroy } from '@angular/core';
import { MessageNotificationDto } from '../../features/models/messageNotificationDto';
import { signal } from '@angular/core';
import { Subscription } from 'rxjs';
import { ToastService } from '../../core/services/toast-service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-toast-notification',
  imports: [CommonModule],
  standalone: true,
  templateUrl: './toast-notification.html',
  styleUrl: './toast-notification.css',
})
export class ToastNotification implements OnDestroy {
  notification = signal<MessageNotificationDto | null>(null);

  private subscription: Subscription;
  private timeout?: ReturnType<typeof setTimeout>

  private notificationSound = new Audio('/sounds/notification1.mp3')

  constructor(private toastService: ToastService, private router: Router){
    this.subscription = this.toastService.messageToast$.subscribe(message=>{
      this.notification.set(message)
      if(this.timeout){
        clearTimeout(this.timeout)
      }

      this.notificationSound.currentTime = 0;
            this.notificationSound.play().catch(err => {
                console.log("Greska prilikom reprodukovanja zvuka");
            })

      this.timeout = setTimeout(()=>{
        this.notification.set(null)
      }, 4000)
    });
  }

  openChat(){
    const notification = this.notification();

    if(!notification){
      return;
    }

    this.notification.set(null);

    this.router.navigate(['inbox', notification.chatId])
  }
  
  close(event: MouseEvent){
    event.stopPropagation()
    this.notification.set(null)
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
    if(this.timeout) clearTimeout(this.timeout)
  }
}
