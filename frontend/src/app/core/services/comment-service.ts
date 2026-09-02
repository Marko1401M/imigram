import { HttpClient } from '@angular/common/http';
import { Injectable, Service } from '@angular/core';
import { AddCommentDto } from '../../features/models/addCommentDto';
import { form } from '@angular/forms/signals';
import { Comment } from '../../features/models/comment';
@Injectable({
    providedIn: 'root'
})
export class CommentService {
    private apiUrl = 'https://localhost:7109/api/comment'
    constructor(private http: HttpClient){}

    addComment(formData: FormData){
        return this.http.post(this.apiUrl, formData);
    }
    getComments(postId: string){
        return this.http.get<Comment[]>(`${this.apiUrl}/all/${postId}`);
    }
}
