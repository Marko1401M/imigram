import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../../core/services/auth';
import {Router, RouterLink} from '@angular/router'
import { NotificationService } from '../../../core/services/notification-service';
import { ToastAction } from '../../../shared/toast-action/toast-action';
import { ToastService } from '../../../core/services/toast-service';
import { signal } from '@angular/core';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, CommonModule],
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
  errorMessage = signal('');

  private emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

  private usernameRegex = /^[A-Za-z0-9_]+$/;

  private passwordRegex = /^(?=.*[A-Za-z])(?=.*\d).{9,}$/;

  constructor(
    private authService: AuthService,
    private toastService: ToastService,
    private router: Router

  ){}

  onFileSelected(event: any){
    this.profileImage = event.target.files[0];
  }

  async register(){
    const formData = new FormData();
    this.errorMessage.set('');
    if(!this.usernameRegex.test(this.username) || this.username.length < 4){ 
      this.errorMessage.set("Korisničko ime mora da ima barem 4 karaktera i sme da sadrži samo slova, brojeve i _.")
      return;

    }
    if(this.password != this.confirmPassword){
      this.errorMessage.set("Šifre moraju da se poklapaju!");
      return;
    }
    if(!this.emailRegex.test(this.email)){
      this.errorMessage.set("Email nije validnog formata.")
      return;
    }

    if(!this.passwordRegex.test(this.password)){
      this.errorMessage.set("Šifra mora biti duža od 9 karaktera i da sadrži barem 1 slovo i barem 1 broj.")
      return;
    }

    if(!this.profileImage){
      const response = await fetch("images/default_profile.png");
      const blob = await response.blob();

      this.profileImage = new File([blob],"default_image.png",{type: blob.type});
    }

    formData.append("Username", this.username);
    formData.append("Email", this.email)
    formData.append("Password", this.password)
    formData.append("ConfirmPassword", this.confirmPassword)
    formData.append("FirstName", this.firstName)
    formData.append("LastName", this.lastName);
    if(this.profileImage){
      formData.append("ProfileImage", this.profileImage)
    }
    else {
      this.toastService.error("Potrebno je odabrati sliku!");
      return;
    }
    this.authService.register(formData).subscribe({
      next:(res) => {
        console.log("Success", res)
        this.toastService.success("Uspešna registracija!")
        this.router.navigate(['/login'])
      },
      error:(err) => {
        //this.toastService.error(err.error)
        if(err.error.includes("Username")) this.errorMessage.set("Korisničko ime je zauzeto.")
        else if(err.error.includes("Email")) this.errorMessage.set("Email je zauzet.")
        else this.errorMessage.set("Došlo je do greške.")
      }
    })
  }
}
