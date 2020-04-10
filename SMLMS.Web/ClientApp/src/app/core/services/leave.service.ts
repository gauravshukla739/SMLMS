import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LeaveType } from '../models/LeaveType';
import { SharedService } from '../../shared/services/shared.service.';

@Injectable({
  providedIn: 'root'
})
export class LeaveService {

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
  delete(id) {
    return this.http.post(this.apiBaseUrl + "/Leave/Delete?id=" + id, null);
  }

  allLeaveRequest() {
    return this.http.get(this.apiBaseUrl + "/Leave/GetLeaveRequest");
  }
  postLeaveRequest(data: any) {
    return this.http.post(this.apiBaseUrl + "/Leave/RequestLeave", data);
  }

  deleteLeaveRequest(id: any) {
    return this.http.post(this.apiBaseUrl + "/Leave/DeleteLeaveRequest?id=" + id, null);
  }

  getLeaveDataBasedOnId() {
    return this.http.get(this.apiBaseUrl + "/Leave/GetDataBasedOnId");
  }

  approveLeave(id: any) {
    return this.http.post(this.apiBaseUrl + "/Leave/ApproveLeaveRequest?id=" + id, null);
  }


  rejectLeave(data: any) {
    return this.http.post(this.apiBaseUrl + "/Leave/RejectLeaveRequest", data);
  }



  getLeaveByDepartment(departmentId: any) {
    return this.http.get(this.apiBaseUrl + "/Leave/getLeaveByDepartmentId?DepartmentId=" + departmentId);
  }



    allEmpLeaves() {
        return this.http.get(this.apiBaseUrl + "/EmployeeLeave");
    }
}
