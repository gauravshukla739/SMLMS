import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../../core/services/authentication.service';
import { SharedService } from '../../../shared/services/shared.service.';
import { Router } from '@angular/router';
import { AttendanceService } from '../../../core/services/attendance.service';
import { NgForm } from '@angular/forms';

 
@Component({
  selector: 'app-attendance',
  templateUrl: './attendance.component.html',
  styleUrls: ['./attendance.component.css']
})
export class AttendanceComponent implements OnInit {

  displayColumn = ["firstName", "lastName", "signIn", , "signOut", "createdOn", "totalTime"];
  userAttendance = [];
  disableBtn: boolean = false;
  constructor(private authService: AuthenticationService, private sharedService: SharedService, private router: Router, private attendanceService: AttendanceService, ) {

  }

  ngOnInit() {
    debugger;
   this.getAllUsers();
    //this.Cre();
  }


  PunchIn_Out() {
    this.sharedService.startLoading();
    
    this.attendanceService.CreateOrUpDate().subscribe((data: any) => {
      if (data.isSuccess) {
        this.getAllUsers();
        this.disableBtn = true;
        this.sharedService.showPopup("Successfully punchin");
      }
      else {
        this.sharedService.showPopup("Failed punchin");
      }
    })
  }

  onSubmit(form: NgForm) {
    console.log(form);
  }

  getAllUsers() {
    debugger;
    this.attendanceService.all().subscribe((data: any) => {
      if (data.isSuccess) {
        this.userAttendance = data.data;
        console.log(data);
        //this.router.navigate(['/permissions']);
      } else {
        this.sharedService.showPopup(data.Message);
        //this.sharedService.showPopup("Login failed , Invalid user");
      }
    })
  }


  Cre() {
    this.attendanceService.CreateOrUpDate().subscribe((data: any) => {
      debugger;
    });
  }
}
