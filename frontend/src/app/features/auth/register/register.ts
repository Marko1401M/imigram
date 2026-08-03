import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../../core/services/auth';
import {Router, RouterLink} from '@angular/router'
@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  username = '';
  password = '';
  confirmPassword ='';
  firstName = '';
  lastName = '';
  email = '';
  profileImage?:File;

  constructor(private authService: AuthService){}

  onFileSelected(event: any){
    this.profileImage = event.target.files[0];
  }

  register(){
    const formData = new FormData();

    formData.append("Username", this.username);
    formData.append("Email", this.email)
    formData.append("Password", this.password)
    formData.append("ConfirmPassword", this.confirmPassword)
    formData.append("FirstName", this.firstName)
    formData.append("LastName", this.lastName);
    if(this.profileImage){
      formData.append("ProfileImage", this.profileImage)
    }
    this.authService.register(formData).subscribe({
      next:(res) => {
        console.log("Success", res)
      },
      error:(err) => {
        console.log(err)
      }
    })
  }
}
