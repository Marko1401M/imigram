import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { PostService } from '../../../core/services/post-service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-create-post',
  imports: [CommonModule, FormsModule],
  templateUrl: './create-post.html',
  styleUrl: './create-post.css',
})
export class CreatePost {
  
  content = '';
  location = '';
  selectedFiles: File[] = [];

  constructor(
      private postService: PostService, 
      private router: Router
  ){}

  onFileSelected(event: any){
      this.selectedFiles = Array.from(
          event.target.files
      );
  }

  createPost(){
    const formData = new FormData();

    formData.append(
        "content",
        this.content
    );

    formData.append(
        "location",
        this.location
    );

    this.selectedFiles.forEach(file => {
        formData.append(
            "media",
            file
        );
    });

    this.postService.createPost(formData)
        .subscribe({
            next: res => {
                console.log("Uspešno kreiran post");
                console.log(res)
                
                this.router.navigate([`/post-details/`, res.id])
            },
            error: err => {
                console.log(err);
            }
        });

  }
}
