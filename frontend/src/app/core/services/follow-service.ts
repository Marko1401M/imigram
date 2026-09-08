import { HttpClient } from '@angular/common/http';
import { Service, Injectable } from '@angular/core';
import { Observable } from 'rxjs';


@Injectable({
    providedIn: 'root'
})
export class FollowService {
    private apiUrl = `https://localhost:7109/api/follow`

    constructor(private http: HttpClient){}

    sendFollowRequest(formData: FormData){
        return this.http.post(`${this.apiUrl}/send_request`, formData);
    }
    acceptFollowRequest(requestId: string){
        return this.http.post(`${this.apiUrl}/accept_request/${requestId}`,{})
    }
    declineFollowRequest(requestId: string){
        return this.http.post(`${this.apiUrl}/decline_request/${requestId}`,{});
    }
    getFollowRequests(): Observable<[]>{
        return this.http.get<[]>(`${this.apiUrl}/follow_request`);
    }
    getFollowers(userId: string): Observable<[]>{
        return this.http.get<[]>(`${this.apiUrl}/followers/${userId}`)
    }
    getFollowings(userId: string): Observable<[]>{
        return this.http.get<[]>(`${this.apiUrl}/followings/${userId}`);
    }
    getFollowStatus(userId: string): Observable<{result: string}>{
        return this.http.get<{result: string}>(`${this.apiUrl}/follow_status/${userId}`);
    }
}
