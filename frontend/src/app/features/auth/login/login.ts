import { Component } from '@angular/core';
import { AuthService } from '../../../core/services/auth';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  username = '';
  password ='';
  constructor(private authService: AuthService, private router: Router){}

  login(){
    this.authService.login(this.username, this.password).subscribe({
      next: (response) =>{
        console.log("success", response);
        this.router.navigate(["/home"])
      },
      error:(err)=>{
        console.log(err)
      }
    })
  }
}
