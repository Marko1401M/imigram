import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PostService } from '../../../core/services/post-service';
import { signal } from '@angular/core';
import { PostAll } from '../../models/post-all';
import { PostCard } from '../../../shared/post-card/post-card';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CommentService } from '../../../core/services/comment-service';
import { AddCommentDto } from '../../models/addCommentDto';
import { Comment } from '../../models/comment';
import { form } from '@angular/forms/signals';
@Component({
  selector: 'app-post-details',
  imports: [PostCard, CommonModule, FormsModule],
  standalone: true,
  templateUrl: './post-details.html',
  styleUrl: './post-details.css',
})
export class PostDetails implements OnInit {
  post = signal<PostAll | null>(null)
  comments = signal<Comment[]>([]);

  newComment = '';
  postId!: string;

  constructor(private route: ActivatedRoute, private postService: PostService, private commentService: CommentService, private router: Router){}

  ngOnInit(): void {
    this.postId = this.route.snapshot.paramMap.get('id')!;

    this.loadPost();
    this.loadComments();
  }
  onPostDeleted(postId: string){
    this.router.navigate(['/home'])
  }
  loadPost(): void{
    this.postService.getPost(this.postId).subscribe({
      next:(response)=>{
        this.post.set(response)
      },
      error:(err) => {
        console.error(err);
      }
    })
  }

  loadComments(): void{
    this.commentService.getComments(this.postId).subscribe({
      next:(response) =>{
        this.comments.set(response)
        console.log(response);
      },
      error: err=>{
        console.error(err);
      }
    })
    return;
  }

  addComment(): void {
  if (!this.newComment.trim()) {
    return;
  }

  console.log('Post ID:', this.postId);
  console.log('Comment:', this.newComment);
  const formData = new FormData()
  formData.append('userId', '1')
  formData.append('postId', this.postId);
  formData.append('content', this.newComment)
  this.commentService.addComment(formData).subscribe({
    next: (res) =>{
      this.loadComments();
      if(this.post() != null) {
        const updatedPost = this.post();
        if(updatedPost){
          updatedPost.commentsCount += 1;
          this.post.set(updatedPost);
        }
      }
    },
    error: err=>{
      console.error("Greska")
    }
  })
  this.newComment = '';
  }
}
