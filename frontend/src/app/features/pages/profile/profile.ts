import { Component, OnInit, signal } from '@angular/core';
import { UserService } from '../../../core/services/user-service';
import { PostService } from '../../../core/services/post-service';
import { ActivatedRoute, Router } from '@angular/router';
import { PostAll } from '../../models/post-all';
import { UserDto } from '../../models/userDto';
import { PostCard } from '../../../shared/post-card/post-card';
import { CommonModule } from '@angular/common';
import { FollowService } from '../../../core/services/follow-service';
import { form } from '@angular/forms/signals';
import { ToastService } from '../../../core/services/toast-service';
import { MatIconModule } from '@angular/material/icon';
@Component({
  selector: 'app-profile',
  imports: [CommonModule, PostCard, MatIconModule],
  templateUrl: './profile.html',
  styleUrl: './profile.css',
})
export class Profile {
  user = signal<UserDto | null>(null)

  posts = signal<PostAll[]>([])

  userId!: string

  followers = signal([])

  followings = signal<[]>([])

  followStatus = signal('');

  constructor(private route: ActivatedRoute, 
    private userService: UserService, 
    private postService: PostService, 
    private followService: FollowService,
    private router: Router,
    private toastService: ToastService
  ){}

  ngOnInit(): void{
    this.userId = this.route.snapshot.paramMap.get('id')!;
    
    this.loadUser();
    
    this.loadPosts();

    this.loadFollowingStatus();

    this.loadFollowers();
    
    this.loadFollowings();
  }
  isOwner(){
    const loggedUser = localStorage.getItem('userId')
    return loggedUser == this.userId;
  }
  onPostDeleted(postId: string) {
    this.posts.update(posts =>
        posts.filter(post => post.id !== postId)
    );
    this.toastService.success("Objava uspešno obrisana")
}
  showMessageButton() : boolean{
    const userId = localStorage.getItem('userId');
    if(this.userId && userId && this.userId != userId) return true;
    return false;
  }
  openChat(receiverId: string){
    this.router.navigate(['/inbox', receiverId])
  }
  showFollowers(){
    const uId = localStorage.getItem('userId');
    if(uId && uId == this.userId) this.router.navigate(['/followers'])
  }
  showFollowing(){
    const uId = localStorage.getItem('userId');
    if(uId && uId == this.userId) this.router.navigate(['/followers'])
  }
  loadFollowingStatus(){
    
    this.followService.getFollowStatus(this.userId).subscribe({
      next: res=>{
        console.log("Profil stranica!!!!")
        console.log(res);
        this.followStatus.set(res.result);
        console.log(this.followStatus())
      },
      error: err=>{
        console.error(err);
      }
    })
    
  }
  unfollow(userId: string){

  }
  rejectFollowRequest(){
    
  }
  acceptFollowRequest(){

  }
  sendFollowRequest(userId: string){
    const formData = new FormData();
    formData.append('RecieverId', userId);
    this.followService.sendFollowRequest(formData).subscribe({
      next: res=>{
        console.log("Zapracen")
        this.toastService.success("Uspešno poslat zahtev za praćenje!")
        this.loadFollowingStatus();
        console.log(res)
      },
      error: err=>{
        console.log("Error");
      }

    })
    
  }
  loadUser() : void{
    this.userService.getUser(this.userId).subscribe({
      next: res=>{
        
        this.user.set(res)
      },
      error: err=>{
        console.error(err);
      }
    })
  }
  loadFollowings(): void{
    this.followService.getFollowings(this.userId).subscribe({
      next: res=>{
        
        this.followings.set(res);
      },
      error: err=>{
        console.error(err);
      }
    })
  }
  loadFollowers(): void{
    this.followService.getFollowers(this.userId).subscribe({
      next: res=>{
        
        this.followers.set(res);
      },
      error: err=>{
        console.error(err);
      }
    })
  }
  loadPosts(): void {
    this.postService.getPostsForUser(this.userId).subscribe({
      next: res=>{
        
        this.posts.set(res);
      },
      error: err=>{
        console.log(err);
      }
    })
  }
  onProfileImageSelected(event: Event): void {
    const input = event.target as HTMLInputElement;

    if (!input.files || input.files.length === 0) {
        return;
    }

    const file = input.files[0];

    if (!file.type.startsWith('image/')) {
        alert('Please select an image.');
        return;
    }

    if (file.size > 5 * 1024 * 1024) {
        alert('Image must be smaller than 5 MB.');
        return;
    }

    const formData = new FormData();
    formData.append('Image', file);

    this.userService.updateProfileImage(formData).subscribe({
        next: (updatedUser) => {
            this.user.set(updatedUser);
        },
        error: (err) => {
            console.error('Error updating profile image:', err);
            alert('Failed to update profile image.');
        }
    });
}
}
