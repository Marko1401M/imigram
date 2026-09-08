import { CommonModule } from '@angular/common';
import { Component, signal } from '@angular/core';
import { AdminService } from '../../../core/services/admin-service';
import { ReportService } from '../../../core/services/report-service';
import { filter } from 'rxjs';
import { ReportResponse } from '../../models/reportResponseDto';
import { UserDto } from '../../models/userDto';
import { MatIcon } from '@angular/material/icon';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-panel',
  imports: [CommonModule, MatIcon],
  templateUrl: './admin-panel.html',
  styleUrl: './admin-panel.css',
})
export class AdminPanel {

  constructor(private adminService: AdminService, 
              private reportService: ReportService,
              private router: Router
            ){}
  activeTab: 'reports' | 'banned' = 'reports';

  selectedStatus = 'all'

  reports = signal<ReportResponse[]>([]);
  filteredReports = signal<ReportResponse[]>([])

  bannedUsers = signal<UserDto[]>([])

  ngOnInit(){
    this.loadReports();
    this.loadBannedUsers();
  }
  viewPost(postId: string){
    const url = this.router.serializeUrl(
      this.router.createUrlTree(['/post-details', postId])
    );

    window.open(url, '_blank')
  }
  loadReports(){
    this.reportService.getAllReports().subscribe({
      next: res=>{
        this.reports.set(res);
        console.log(res);
        this.filterReports();
      },
      error: err=>{
        console.error(err);
      }
    })
  }

  loadBannedUsers(){
    this.adminService.getBannedUsers().subscribe({
      next: res=>{
        this.bannedUsers.set(res);
      },
      error: err=>{
        console.error(err);
      }
    })
  }
  filterReports(){
    if(this.selectedStatus === 'all'){
      this.filteredReports.set(this.reports())
      return;
    }
    const reps = this.reports();
    const newFiltered = reps.filter(r => r.status == this.selectedStatus)
    this.filteredReports.set(newFiltered);
    // TODO
  }
  deletePost(report: ReportResponse){
    this.adminService.deletePost(report.post.id).subscribe({
      next: res=>{

      },
      error: err=>{
        console.error(err);
      }
    })
    this.reportService.updateReportStatus(report.id, "ActionTaken").subscribe({
      next: res=>{

      },
      error: err=>{
        console.error(err);
      }
    })
    this.loadReports();
  }
  deleteAndBan(report: ReportResponse){
    this.adminService.deletePost(report.post.id).subscribe({
      next: res =>{

      },
      error: err=>{
        console.error(err)
      }
    })

    this.adminService.banUser(report.post.userId).subscribe({
      next: res=>{

      },
      error: err=>{
        console.error(err);
      }
    })
    this.reportService.updateReportStatus(report.id, "ActionTaken").subscribe({
      next: res=>{

      },
      error: err=>{
        console.error(err);
      }
    })
    this.loadReports()
  }

  ignoreReport(report: any){
    this.reportService.updateReportStatus(report.id, "Ignored").subscribe({
      next: res=>{

      },
      error: err=>{
        console.error(err);
      }
    })
  }

  unbanUser(user:UserDto){
    this.adminService.unbanUser(user.id).subscribe({
      next: res=>{
        this.loadBannedUsers();
      },
      error: err=>{
        console.error(err);
      }
    })
  }
}
