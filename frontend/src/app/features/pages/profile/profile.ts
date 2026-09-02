import { Component, OnInit, signal } from '@angular/core';
import { UserService } from '../../../core/services/user-service';
import { PostService } from '../../../core/services/post-service';
import { ActivatedRoute } from '@angular/router';
import { PostAll } from '../../models/post-all';
import { UserDto } from '../../models/userDto';
import { PostCard } from '../../../shared/post-card/post-card';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-profile',
  imports: [CommonModule, PostCard],
  templateUrl: './profile.html',
  styleUrl: './profile.css',
})
export class Profile {
  user = signal<UserDto | null>(null)

  posts = signal<PostAll[]>([])

  userId!: string

  constructor(private route: ActivatedRoute, private userService: UserService, private postService: PostService){}

  ngOnInit(): void{
    this.userId = this.route.snapshot.paramMap.get('id')!;

    this.loadUser();
    
    this.loadPosts();
  }

  loadUser() : void{
    this.userService.getUser(this.userId).subscribe({
      next: res=>{
        console.log(res);
        this.user.set(res)
      },
      error: err=>{
        console.error(err);
      }
    })
  }

  loadPosts(): void {
    this.postService.getPostsForUser(this.userId).subscribe({
      next: res=>{
        console.log(res)
        this.posts.set(res);
      },
      error: err=>{
        console.log(err);
      }
    })
  }
}
