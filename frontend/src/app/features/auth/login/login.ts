import { Component } from '@angular/core';
import { AuthService } from '../../../core/services/auth';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService } from '../../../core/services/message-service';
import { NotificationService } from '../../../core/services/notification-service';
import { signal } from '@angular/core';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-login',
  imports: [FormsModule, CommonModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  username = '';
  password ='';
  errorMessage = signal('');
  constructor(private authService: AuthService, 
              private router: Router, 
              private messageService: MessageService, 
              private notificationService: NotificationService){}

  login(){
    this.authService.login(this.username, this.password).subscribe({
      next: async (response) =>{
        console.log("success", response);
        await this.messageService.startConnection()
        this.errorMessage.set('');
        this.notificationService.startConnection()
        this.notificationService.getNotifications()
        this.router.navigate(["/home"])
      },
      error:(err)=>{
        console.log(err)
        this.errorMessage.set('Pogrešno korisničko ime ili lozinka.')
      }
    })
  }
}
