import { Injectable, Service } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';
import * as signalR from '@microsoft/signalr'
import { HttpClient } from '@angular/common/http';
import { Notification } from '../../features/models/notification';
@Injectable({
    providedIn: 'root'
})
export class NotificationService {
    private apiUrl = 'https://localhost:7109/api/Notification'
    private hubUrl = 'https://localhost:7109/hubs/notifications'

    private notificationsSubject = new BehaviorSubject<Notification[]>([])

    private newNotificationSubject = new Subject<Notification>();

    private notificationSound = new Audio('/sounds/notification1.mp3')

    newNotification$ = this.newNotificationSubject.asObservable();

    notification$ = this.notificationsSubject.asObservable();

    private hubConnection!: signalR.HubConnection;

    constructor(private http: HttpClient){}

    getNotifications(){
        this.http.get<Notification[]>(`${this.apiUrl}`).subscribe(notifications => {
            this.notificationsSubject.next(notifications)
        })
    }

    startConnection(){
        
        this.hubConnection = new signalR.HubConnectionBuilder().withUrl(this.hubUrl,{
            accessTokenFactory: ()=>{
                const token = localStorage.getItem('token')
                console.log(`token_notif = ${token}`)
                return token || '';
            }
        })
        .withAutomaticReconnect()
        .build()

        this.hubConnection.on("ReceiveNotification", (notification: Notification) => {
            console.log('Nova notifikacija:', notification);

            const currentNotifications = this.notificationsSubject.value;

            this.notificationsSubject.next([
                notification,
                ...currentNotifications
            ]);

            this.newNotificationSubject.next(notification);

            this.notificationSound.currentTime = 0;
            this.notificationSound.play().catch(err => {
                console.log("Greska prilikom reprodukovanja zvuka");
            })
        });
        
        if(this.hubConnection.state == signalR.HubConnectionState.Connected || this.hubConnection.state == signalR.HubConnectionState.Connecting) return;

        this.hubConnection.start()
        .then(() => console.log("SignalR connected"))
        .catch(err => console.error("signalR error:", err))
    }

    getUnreadCount(){
        return this.notificationsSubject.value.filter(n => !n.isRead).length;
    }

    markAsRead(notificationId: string){
        return this.http.post(`${this.apiUrl}/read/${notificationId}`, {})
    };
}
