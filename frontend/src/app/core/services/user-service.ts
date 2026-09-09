import { Service } from '@angular/core';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserDto } from '../../features/models/userDto';
import { Observable } from 'rxjs';
import { UserSearchDto } from '../../features/models/userSearchDto';
@Injectable({
    providedIn: 'root'
})
export class UserService {
    private apiUrl = 'https://localhost:7109/api/user'

    constructor(private http: HttpClient){}

    getUser(userId: string){
        return this.http.get<UserDto>(`${this.apiUrl}/${userId}`)
    }
    updateProfileImage(formData: FormData): Observable<UserDto>{
        return this.http.put<UserDto>(`${this.apiUrl}/update/profile-image/`, formData);
    }
    searchUsers(query: string): Observable<UserSearchDto[]>{
        return this.http.get<UserSearchDto[]>(`${this.apiUrl}/search?query=${query}`)
    }
}
