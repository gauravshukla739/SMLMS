import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LeaveType } from '../models/LeaveType';
import { SharedService } from '../../shared/services/shared.service.';

@Injectable({
  providedIn: 'root'
})
export class LeaveService {
  //apiBaseUrl = "http://localhost:52710";
  //url = this.apiBaseUrl + `/api/Leave/Get`;
  //constructor(private http: HttpClient) { } // inject kar liya htpclient se make function/method  to get data  of employee(this func will return data) to bind.

  //GetAllLeaveTypes(): Observable<LeaveType[]> {
  //  return this.http.get<LeaveType[]>(this.url)
  //}
  //addLeaveTypes(data: any): any {
  //  var req = this.apiBaseUrl + `/api/Leave/Post`;
  //  return this.http.post(req,data);
  //}
  //deleteLeaveTypes(id: string): any {
  //  var req = this.apiBaseUrl + `/api/Leave/Delete?id=` + id;
  //  return this.http.post(req, { body: null });
  //}

  apiBaseUrl: string;
  constructor(protected http: HttpClient, private sharedService: SharedService) {
    this.apiBaseUrl = sharedService.ApiBaseUrl;
  }
  all() {
    return this.http.get(this.apiBaseUrl + "/Leave/Get");
  }
  post(data: any) {
    return this.http.post(this.apiBaseUrl + "/Leave/Post", data);
  }
  delete(id: any) {
    return this.http.post(this.apiBaseUrl + "Leave/Delete?id="+id, null);
  }

}
