import { HttpClient } from '@angular/common/http';
import { Injectable, Service } from '@angular/core';
import { ChatDto } from '../../features/models/chatDto';
import { Observable } from 'rxjs';
import { ChatResponse } from '../../features/models/chatResponseDto';

@Injectable({
    providedIn: 'root'
})
export class ChatService {
    private apiUrl = `https://localhost:7109/api/chat`

    constructor(private http: HttpClient){}

    getAllChats(): Observable<ChatResponse[]>{
        return this.http.get<ChatResponse[]>(this.apiUrl);
    }
    getChat(chatId: string): Observable<ChatResponse>{
        return this.http.get<ChatResponse>(`${this.apiUrl}/${chatId}`)
    }
    getChatWithUser(userId: string): Observable<ChatResponse>{
        return this.http.get<ChatResponse>(`${this.apiUrl}/${this.apiUrl}`)
    }
}
