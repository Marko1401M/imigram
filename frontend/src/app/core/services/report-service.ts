import { Injectable, Service } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ReportResponse } from '../../features/models/reportResponseDto';

@Injectable({
    providedIn:'root'
})
export class ReportService {
    private apiUrl = `https://localhost:7109/api/report`

    constructor(private http: HttpClient){}
    
    createReport(formData: FormData){
        return this.http.post(this.apiUrl, formData);
    }

    getAllReports() : Observable<ReportResponse[]>{
        return this.http.get<ReportResponse[]>(`${this.apiUrl}/get/all`);
    }

    getReportsByStatus(status: string) : Observable<ReportResponse[]>{
        return this.http.get<ReportResponse[]>(`${this.apiUrl}/get/all/${status}`)
    }

    getReportById(id: string): Observable<ReportResponse>{
        return this.http.get<ReportResponse>(`${this.apiUrl}/${id}`)
    }

    updateReportStatus(id: string, status: string){
        return this.http.put(`${this.apiUrl}/${id}/${status}`, {})
    }
}
