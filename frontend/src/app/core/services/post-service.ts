import { HttpClient } from '@angular/common/http';
import { Injectable, Service } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class PostService {
    private apiUrl = "https://localhost:7109/api/post";

    constructor(private http: HttpClient){}

    createPost(formData: FormData){
        return this.http.post(
            this.apiUrl,
            formData
        );
    }
}