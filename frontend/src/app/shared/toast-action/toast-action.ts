import { Component, signal } from '@angular/core';
import { ToastDto } from '../../features/models/toastDto';
import { ToastService } from '../../core/services/toast-service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-toast-action',
  imports: [CommonModule],
  templateUrl: './toast-action.html',
  styleUrl: './toast-action.css',
})
export class ToastAction {
  toast = signal<ToastDto | null>(null)

  private timeout?: ReturnType<typeof setTimeout>;

  constructor(private toastService: ToastService){}

  ngOnInit(){
    this.toastService.toast$.subscribe(toast=>{
      this.toast.set(toast);
      
      if(this.timeout){
        clearTimeout(this.timeout);
      }

      this.timeout = setTimeout(()=>{
        this.toast.set(null);
      }, 3000)
    })
  }

  close(){
    this.toast.set(null);
    if(this.timeout) clearTimeout(this.timeout);
  }
}
