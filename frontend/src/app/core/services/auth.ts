import { Injectable, Service } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Observable, tap} from 'rxjs'
import { form } from '@angular/forms/signals';
@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private apiUrl = 'https://localhost:7109/api/Auth'
    constructor (private http: HttpClient){}

    isLoggedIn() : boolean{
        return !!localStorage.getItem("token");
    }

    logout(){
        localStorage.removeItem("token");
    }

    login(username: string, password:string){
  
        return this.http.post<any>(`${this.apiUrl}/login`,
        {
            username: username,
            password: password
        }
        ).pipe(tap(response =>{
            localStorage.setItem('token', response.token);
            localStorage.setItem('userId', response.id);
            localStorage.setItem('role', response.role)
        }))
    }

    register(formData: FormData){
        return this.http.post(`${this.apiUrl}/register`, formData)
    }
}
