import { Component } from '@angular/core';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import { UserSearchDto } from '../../models/userSearchDto';
import { signal } from '@angular/core';
import { UserService } from '../../../core/services/user-service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatProgressSpinner, MatProgressSpinnerModule } from '@angular/material/progress-spinner';
@Component({
  selector: 'app-search-page',
  imports: [MatIconModule, CommonModule, FormsModule, MatProgressSpinnerModule],
  templateUrl: './search-page.html',
  styleUrl: './search-page.css',
})
export class SearchPage {
  searchQuery = signal('')
  users = signal<UserSearchDto[]>([])
  loading = signal(false)

  constructor(private userService: UserService, private router: Router){}

  searchUsers() : void{
    const query = this.searchQuery().trim()
    console.log(query)
    if(!query){
      this.users.set([]);
      return;
    }

    this.loading.set(true)

    this.userService.searchUsers(query).subscribe({
      next: users=>{
        this.users.set(users);
        this.loading.set(false)
      },
      error: err=>{
        console.error(err);
        this.users.set([])
        this.loading.set(false)
      }
    })
  }

  clearSearch(): void{
    this.searchQuery.set('')
    this.users.set([]);
  }
  
  openProfile(userId: string){
    this.router.navigate(['/profile', userId])
  }
}
