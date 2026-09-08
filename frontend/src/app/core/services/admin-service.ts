import { HttpClient } from '@angular/common/http';
import { Injectable, Service } from '@angular/core';
import { UserDto } from '../../features/models/userDto';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class AdminService {
    private apiUrl = `https://localhost:7109/api/admin`

    constructor(private http: HttpClient){}

    banUser(userId: string){
        return this.http.post(`${this.apiUrl}/ban/${userId}`,{})
    }

    unbanUser(userId: string){
        return this.http.post(`${this.apiUrl}/unban/${userId}`, {})
    }

    deletePost(postId: string){
        return this.http.delete(`${this.apiUrl}/${postId}`)
    }

    getBannedUsers() : Observable<UserDto[]>{
        return this.http.get<UserDto[]>(`${this.apiUrl}`)
    }
}
