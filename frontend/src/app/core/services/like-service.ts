import { Injectable, Service } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Injectable({
    providedIn: 'root'
})
export class LikeService {
    private apiUrl = `https://localhost:7109/api/like`

    constructor(private http: HttpClient){}

    addLike(formData: FormData){
        return this.http.post(this.apiUrl, formData);
    }

    removeLike(postId: string){
        return this.http.delete(`${this.apiUrl}/${postId}`);
    }
}
