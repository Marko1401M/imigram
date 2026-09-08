import { Component } from '@angular/core';
import { signal } from '@angular/core';
import { FollowService } from '../../../core/services/follow-service';
import { CommonModule, NgIf } from '@angular/common';
import { UserDto } from '../../models/userDto';
import { FollowRequest } from '../../models/followRequestDto';
@Component({
  selector: 'app-followers-page',
  imports: [CommonModule, NgIf],
  templateUrl: './followers-page.html',
  styleUrl: './followers-page.css',
})
export class FollowersPage {
  followRequests = signal<FollowRequest[]>([]);
  following = signal<UserDto[]>([])
  followers = signal<UserDto[]>([])
  constructor(private followService: FollowService){}

  ngOnInit(){
    this.loadFollowRequests()
    const userId = localStorage.getItem('userId');
    if(userId) this.loadFollowing(userId);
    if(userId) this.loadFollowers(userId);
  }
  rejectRequest(requestId: string){
    this.followService.declineFollowRequest(requestId).subscribe({
      next: res=>{

        this.loadFollowRequests();
      },
      error: err=>{
        console.error(err);
      }
    })
  }
  acceptRequest(requestId: string){
    this.followService.acceptFollowRequest(requestId).subscribe({
      next: res=>{
        const userId = localStorage.getItem('userId')
        if(userId) this.loadFollowers(userId);
        this.loadFollowRequests();
      },
      error: err=>{
        console.error(err);
      }
    })
  }
  unfollow(userId: string){

  }
  loadFollowRequests(){
    this.followService.getFollowRequests().subscribe({
      next: res=>{
        this.followRequests.set(res);
        console.log(res);
      },
      error: err=>{
        console.error(err);
      }
    })
  }
  loadFollowing(userId: string){
    this.followService.getFollowings(userId).subscribe({
      next: res=>{
        this.following.set(res)
      },
      error: err=>{
        console.error(err);
      }
    })
  }
  loadFollowers(userId: string){
    console.log('test', userId);
    this.followService.getFollowers(userId).subscribe({
      next: res=>{
        
        this.followers.set(res);
      },
      error: err=>{
        console.error(err);
      }
    })
  }
}
