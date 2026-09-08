import { Injectable, Service } from '@angular/core';
import { Subject, Subscribable, Subscription } from 'rxjs';
import { MessageNotificationDto } from '../../features/models/messageNotificationDto';
import { signal } from '@angular/core';
import { ToastDto } from '../../features/models/toastDto';
@Injectable({
    providedIn:'root'
})
export class ToastService {
    private messageToastSubject = new Subject<MessageNotificationDto>()

    messageToast$ = this.messageToastSubject.asObservable();
    
    private toastSubject = new Subject<ToastDto>()

    toast$ = this.toastSubject.asObservable();

    showMessageToast(message: MessageNotificationDto){
        const userId = localStorage.getItem('userId');
        if(userId != message.senderId) this.messageToastSubject.next(message);
    }

    success(message: string){
        this.toastSubject.next({
            message: message,
            type:'success'
        });
    }

    error(message: string){
        this.toastSubject.next({
            message:message,
            type:'error'
        });
    }

    info(message: string){
        this.toastSubject.next({
            message:message,
            type: 'info'
        });
    }
}
