import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SharedService } from '../../shared/services/shared.service.';

@Injectable({
  providedIn: 'root'
})
export class AttendanceService {

  apiBaseUrl: string;
  constructor(protected http: HttpClient, private sharedService: SharedService) {
    this.apiBaseUrl = sharedService.ApiBaseUrl;
  }

  all(userid, Role, department) {
    return this.http.get(this.apiBaseUrl + `/Attendance/getAllEmployess?userid=` + userid + `&role=` + Role + `&dept=` + department);
  }
  todayPunchIn() {
    return this.http.get(this.apiBaseUrl + `/Attendance/getTodayPunchIn`);
  }


  CreateOrUpDate() {
    return this.http.post(this.apiBaseUrl + `/Attendance/CreateOrUpdate`, null);
  }

  getemployee_attendance(userid, Role) {
    return this.http.get(this.apiBaseUrl + `/Attendance/EmployeeAttendance?userid=` + userid + `&role=` + Role);
  }

  GetEmployeeatendanceDetails(userid) {
    return this.http.get(this.apiBaseUrl + `/Attendance/GetEmployeeatendanceDetails?userid=` + userid );
  }

  Filter_attendance(userid, role, month, department, user) {
    return this.http.get(this.apiBaseUrl + `/Attendance/AttendanceFilter?userid=` + userid + `&role=` + role + `&month=` + month + `&dept=` + department + `&user=` + user);
  }

  FindByEmail(Email) {
    return this.http.get(this.apiBaseUrl + `/Attendance/FindByEmail?email=` + Email);
  }
  getEmployeeAttendance(userid, Role) {
    return this.http.get(this.apiBaseUrl + `/Attendance/Employees_Absent_Present?userid=` + userid + `&role=` + Role);
  }

}
