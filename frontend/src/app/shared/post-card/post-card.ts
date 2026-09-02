import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Post } from '../../features/models/post';
import { MediaType } from '../../features/models/mediaType';
import { PostMedia } from '../../features/models/postMedia';
import { PostAll } from '../../features/models/post-all';
import { Router } from '@angular/router';
import { LikeService } from '../../core/services/like-service';
@Component({
  selector: 'app-post-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './post-card.html',
  styleUrl: './post-card.css',
})
export class PostCard {
  @Input() post!: PostAll;

  MediaType = MediaType;

  currentMediaIndex = 0;

  constructor(private router: Router, private likeService: LikeService){}

  toggleLike(): void {
    if(this.post.isLiked){
      this.post.likesCount--;
      this.likeService.removeLike(this.post.id).subscribe({
        next: res=>{
          console.log(res);
        },
        error: err=>{
          console.error(err);
        }
      })
    }
    else {
      this.post.likesCount++;
      const formData = new FormData();
      formData.append('postId', this.post.id);
      formData.append('userId','123');
      this.likeService.addLike(formData).subscribe({
        next: res=>{
          console.log(res);
        },
        error: err=>{
          console.error(err);
        }
      })
    }
    this.post.isLiked = ! this.post.isLiked;
  }
  showPost(): void{
    this.router.navigate(['post-details',this.post.id])
  }
  nextMedia(): void{
    if(!this.post.media?.length) return;
    this.currentMediaIndex = (this.currentMediaIndex + 1) % this.post.media.length
  }

  previousMedia(): void{
    if(!this.post.media?.length) return;
    this.currentMediaIndex = (this.currentMediaIndex - 1 + this.post.media.length) % this.post.media.length
  }
}
