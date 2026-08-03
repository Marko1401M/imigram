import { Component } from '@angular/core';
import { AuthService } from '../../../core/services/auth';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  username = '';
  password ='';
  constructor(private authService: AuthService){}

  login(){
    this.authService.login(this.username, this.password).subscribe({
      next: (response) =>{
        console.log("success", response);
      },
      error:(err)=>{
        console.log(err)
      }
    })
  }
}
