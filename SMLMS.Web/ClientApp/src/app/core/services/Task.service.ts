import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { SharedService } from 'src/app/shared/services/shared.service.';


@Injectable({ providedIn: 'root' })

export class TaskService {
  apiBaseUrl = "";

  constructor(protected http: HttpClient ,private sharedService: SharedService) {
    this.apiBaseUrl = sharedService.ApiBaseUrl;
  }
  getTask(){
    return this.http.get(this.apiBaseUrl+"/Task/get");
  }
  getTaskById(id:string) {
    return this.http.get(this.apiBaseUrl + "/Task/getTaskById/" + id);
  }
  getTaskByUser(employeeId:any){
    return this.http.get(this.apiBaseUrl + "/Task/getTaskByEmployeeId/" + employeeId);
  }
  getTaskByDepartment(departmentId:any){
    return this.http.get(this.apiBaseUrl + "/Task/getTaskByDepartmentId/" + departmentId);
  }
  addTask(task: any) {
    debugger;
    return this.http.post(this.apiBaseUrl + "/Task/Post",task);
  }


}
