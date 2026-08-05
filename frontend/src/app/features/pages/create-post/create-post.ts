import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { PostService } from '../../../core/services/post-service';
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
      private postService: PostService
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
            },
            error: err => {
                console.log(err);
            }
        });

  }
}
