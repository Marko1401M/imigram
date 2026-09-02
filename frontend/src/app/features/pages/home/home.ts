import { Component } from '@angular/core';
import { Post } from '../../models/post';
import { PostService } from '../../../core/services/post-service';
import { PostAll } from '../../models/post-all';
import { CommonModule } from '@angular/common';
import { PostCard } from '../../../shared/post-card/post-card';

import { signal } from '@angular/core';
@Component({
  selector: 'app-home',
  imports: [CommonModule, PostCard],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {             // ! ! ! ! ! ! ! 
  posts = signal<PostAll[]>([]) // Signal mora da se koristi zbog change detectiona, kada ga nema onda angular registruje promene tek nakon ctrl s iz nekog razloga
  constructor(private postService:PostService){}

  ngOnInit(){
    this.loadPosts()
  }

  loadPosts(){
    this.postService.getAllPosts().subscribe({
      next:(response) =>{
        console.log(response)
        
        this.posts.set(response)
        //this.cdr.detectChanges();
        console.log(this.posts)
      },
      error:(err)=>{
        console.error(err)
      }
    });
  }
}
