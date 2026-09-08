import { HttpClient } from '@angular/common/http';
import { Injectable, Service } from '@angular/core';
import { Observable } from 'rxjs';
import { PostAll } from '../../features/models/post-all';
@Injectable({
    providedIn: 'root'
})
export class PostService {
    private apiUrl = "https://localhost:7109/api/post";

    constructor(private http: HttpClient){}

    createPost(formData: FormData){
        return this.http.post<PostAll>(
            this.apiUrl,
            formData
        );
    }
    getAllPosts(): Observable<PostAll[]>{
        return this.http.get<PostAll[]>(
            this.apiUrl
        );
    }
    getPost(id: string): Observable<PostAll>{
        return this.http.get<PostAll>(`${this.apiUrl}/${id}`);
    }
    getPostsForUser(userId: string): Observable<PostAll[]>{
        return this.http.get<PostAll[]>(`${this.apiUrl}/all/${userId}`)
    }
    deletePost(postId: string){
        return this.http.delete(`${this.apiUrl}/${postId}`)
    }
    getFeed(): Observable<PostAll[]>{
        return this.http.get<PostAll[]>(`${this.apiUrl}/feed`);
    }
}