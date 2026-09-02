import { Service } from '@angular/core';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserDto } from '../../features/models/userDto';
@Injectable({
    providedIn: 'root'
})
export class UserService {
    private apiUrl = 'https://localhost:7109/api/user'

    constructor(private http: HttpClient){}

    getUser(userId: string){
        return this.http.get<UserDto>(`${this.apiUrl}/${userId}`)
    }
}
