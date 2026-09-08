import { Injectable, Service } from '@angular/core';
import { HubConnection, HubConnectionBuilder, HubConnectionState } from '@microsoft/signalr';
import { Observable, Subject } from 'rxjs';
import { MessageDto } from '../../features/models/messageDto';
import { HttpClient } from '@angular/common/http';
import { ToastService } from './toast-service';
import { MessageNotificationDto } from '../../features/models/messageNotificationDto';
import { not } from 'rxjs/internal/util/not';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
    private apiUrl = `https://localhost:7109/api/message`

    private hubConnection!: HubConnection;

    private messageReceivedSubject = new Subject<MessageDto>();

    messageReceived$ = this.messageReceivedSubject.asObservable();

    constructor(private http: HttpClient, private toastService: ToastService){
        
    }

    async startConnection(){
        this.hubConnection = new HubConnectionBuilder().withUrl('https://localhost:7109/hubs/chat', {
            accessTokenFactory: () => {
                const token = localStorage.getItem("token");
                console.log("CHAT HUB:")
                console.log(token)
                return token ?? ''
            }
        })
        .withAutomaticReconnect()
        .build()

        this.hubConnection.on(
            'ReceiveMessage',
            (notification: MessageNotificationDto) => {
                console.log("Stigla poruka!")
                const message: MessageDto = {
                    id: notification.id,
                    chatId: notification.chatId,
                    content: notification.content,
                    isRead: notification.isRead,
                    senderId: notification.senderId,
                    sentAt: notification.sentAt
                };
                this.messageReceivedSubject.next(message)
                this.toastService.showMessageToast(notification);
            }
        );
        try {
            if(this.hubConnection.state == HubConnectionState.Connected || this.hubConnection.state == HubConnectionState.Connecting) return;

            await this.hubConnection.start();
            console.log('CHAT HUB CONNECTED')
        } catch (error){
            console.error('SignalR error connection', error)
        }
    }

    async sendMessage(chatId: string, receiverId: string, content: string){
        if(!this.hubConnection || this.hubConnection.state !== 'Connected') await this.startConnection();
        await this.hubConnection.invoke(
            'SendMessage',
            chatId,
            receiverId,
            content
        );
    }

    getMessages(chatId: string) : Observable<MessageDto[]>{
        return this.http.get<MessageDto[]>(`${this.apiUrl}/${chatId}`)
    }

    markAsRead(messageId: string) : Observable<MessageDto>{
        return this.http.put<MessageDto>(`${this.apiUrl}/${messageId}/read`, {})
    }
}
