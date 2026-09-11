import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Post } from '../../features/models/post';
import { MediaType } from '../../features/models/mediaType';
import { PostMedia } from '../../features/models/postMedia';
import { PostAll } from '../../features/models/post-all';
import { Event, Router } from '@angular/router';
import { LikeService } from '../../core/services/like-service';
import { MatIconModule } from '@angular/material/icon';
import { signal } from '@angular/core';
import { PostService } from '../../core/services/post-service';
import { output } from '@angular/core';
import { ReportService } from '../../core/services/report-service';
import { form } from '@angular/forms/signals';
import { ToastService } from '../../core/services/toast-service';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-post-card',
  standalone: true,
  imports: [CommonModule, MatIconModule, FormsModule],
  templateUrl: './post-card.html',
  styleUrl: './post-card.css',
})
export class PostCard {
  @Input() post!: PostAll;

  MediaType = MediaType;

  currentMediaIndex = 0;

  menuOpen = signal(false);

  reportPanelOpen = signal(false);

  reportReason = '';
  reportDescription = '';

  @Output() postDeleted = new EventEmitter<string>()

  private userId = signal<string | null>(null)
  constructor(private router: Router, 
    private likeService: LikeService, 
    private postService: PostService, 
    private reportService: ReportService,
    private toastService: ToastService
  ){}

  ngOnInit(){
    this.userId.set(localStorage.getItem('userId'))
  }
  isOwner(): boolean{
    return this.userId() === this.post.userId;
  }
  openReportPanel(event: MouseEvent) {
    event.stopPropagation();

    this.menuOpen.set(false);
    this.reportPanelOpen.set(true);
}

closeReportPanel(event?: MouseEvent) {
    event?.stopPropagation();

    this.reportPanelOpen.set(false);
    this.reportReason = '';
    this.reportDescription = '';
}

submitReport(event: MouseEvent) {
    event.stopPropagation();

    if (!this.reportReason) {
        return;
    }

    const report = {
        postId: this.post.id,
        reason: this.reportReason,
        description: this.reportDescription
    };

    const formData = new FormData();
    
    formData.append('ReporterId', localStorage.getItem('userId') || "");
    formData.append('PostId', this.post.id);
    formData.append('ReportedUserId', this.post.userId);
    formData.append('Reason', report.reason);
    formData.append('Description', report.description);
    this.reportService.createReport(formData).subscribe({
      next: res=>{
        console.log("Uspesan report!")
        this.toastService.success("Prijava uspešno poslata!")
        this.closeReportPanel();
      },
      error: err=>{
        this.toastService.error("Došlo je do greške.")
        console.error(err);
      }
    })
}
  toggleMenu(event: MouseEvent){
    event.stopPropagation()
    this.menuOpen.update(value => !value)
  }
  openPost(event: MouseEvent){
    event.stopPropagation();
    this.router.navigate(['/post-details',this.post.id])
  }
  editPost(){
    this.menuOpen.set(false);
  }
  openProfile(event: MouseEvent){
    event.stopPropagation()
    this.router.navigate(['/profile',this.post.userId]);
  }
  deletePost(){
    
    this.postService.deletePost(this.post.id).subscribe({
      next: res=>{
        this.postDeleted.emit(this.post.id)
        //this.router.navigate(['/home'])
      },
      error: err=>{

      }
    })
    this.menuOpen.set(false)
  }

  reportPost(){
    const formData = new FormData();
    formData.append('ReporterId', 'qwe');
    formData.append('PostId', this.post.id);
    formData.append('ReportedUserId', this.post.userId);
    formData.append('Reason', 'test');
    formData.append('Description', 'Neki opis, samo testiram 123 123 123');
    this.reportService.createReport(formData).subscribe({
      next: res=>{
        console.log("Uspesan report!")
      },
      error: err=>{
        console.error(err);
      }
    })
    this.menuOpen.set(false)
  }

  copyLink(){
    this.menuOpen.set(false)

    navigator.clipboard.writeText(window.location.href)
    this.toastService.info("Link kopiran.")
  }



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
