import { Injectable, Service } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Observable, tap} from 'rxjs'
@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private apiUrl = 'https://localhost:7109/api/Auth'
    constructor (private http: HttpClient){}

    login(username: string, password:string){
        return this.http.post<any>(`${this.apiUrl}/login`,
        {
            username: username,
            password: password
        }
        ).pipe(tap(response =>{localStorage.setItem('token', response.token)}))
    }
}
