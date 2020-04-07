import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { AuthenticationService } from '../../../core/services/authentication.service';
import { SharedService } from '../../../shared/services/shared.service.';
import { Router } from '@angular/router';
import { AttendanceService } from '../../../core/services/attendance.service';
import { NgForm } from '@angular/forms';
import { CountupTimerService } from '../../../core/services/countup.service';
import { countUpTimerConfigModel, timerTexts } from '../../../core/models/countup-timer';



@Component({
  selector: 'app-attendance',
  templateUrl: './attendance.component.html',
  styleUrls: ['./attendance.component.css']
})
export class AttendanceComponent implements OnInit {

  displayColumn = ["firstName", "lastName", "signIn", , "signOut", "createdOn", "totalTime"];
  userAttendance = [];
  disable_SignIn: boolean = false;
  disable_SignOut: boolean = false;
  userRole: string;
  currentDate: Date;

  @Input() startTime: String;
  @Input() countUpTimerConfig: countUpTimerConfigModel;

  //Init
  timerObj: any = {};
  private timerSubscription;
  timerConfig: countUpTimerConfigModel;
  timerTextConfig: timerTexts;

  constructor(private countupTimerService: CountupTimerService, private authService: AuthenticationService, private sharedService: SharedService, private router: Router, private attendanceService: AttendanceService, ) {

  }



  ngOnInit() {
    this.userRole = this.sharedService.user.roleName || "";
    //this.getAllUsers();
    this.GetEmployee_attendance();
    this.currentDate = new Date();

    //Timer
    this.getTimerValue();
    this.timerConfig = new countUpTimerConfigModel();
    this.timerTextConfig = new timerTexts();
    this.timerConfig = this.countUpTimerConfig ? Object.assign(this.countUpTimerConfig) : null;
    this.timerTextConfig = this.countUpTimerConfig && this.countUpTimerConfig.timerTexts ? Object.assign(this.countUpTimerConfig.timerTexts) : null;

  }

  //get timer value
  getTimerValue = () => {
    this.timerSubscription = this.countupTimerService.getTimerValue().subscribe(res => {
      this.timerObj = Object.assign(res);
    }, error => {
      console.log(error);
      console.log('Failed to get timer value');
    });
  }

  ngOnDestroy() {
    this.timerSubscription.unsubscribe();
  }


  Punch_In() {
    this.sharedService.startLoading();

    this.attendanceService.CreateOrUpDate().subscribe((data: any) => {
      if (data.isSuccess) {
        this.getAllUsers();
        this.disable_SignIn = true;
        debugger;
        this.countupTimerService.startTimer();
        this.sharedService.showPopup("Successfully punchin");
      }
      else {
        this.sharedService.showPopup("Failed punchin");
      }
    })
  }


  Punch_out() {
    this.sharedService.startLoading();

    this.attendanceService.CreateOrUpDate().subscribe((data: any) => {
      if (data.isSuccess) {
        this.getAllUsers();
        this.disable_SignIn = false;
        this.countupTimerService.stopTimer();
        this.sharedService.showPopup("Successfully punch Out");
      }
      else {
        this.sharedService.showPopup("Failed punch Out");
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
        debugger;
        this.userAttendance = data.data;
        console.log(data);

        for (var i = 0; i < data.data.length; i++) {
          let SignOutDate = new Date(data.data[i].signOut);

          if (data.data[i].signOut == null) {
            this.disable_SignIn = true;
            var signIn_Date = new Date(data.data[i].signIn);
            this.countupTimerService.startTimer(signIn_Date);
          }
          else if (data.data[i].signOut != null && SignOutDate.getDate() == this.currentDate.getDate()) {
            this.disable_SignOut = true;
            this.disable_SignIn = true;
          }
        }
      } else {
        this.sharedService.showPopup(data.Message);
        //this.sharedService.showPopup("Login failed , Invalid user");
      }
    })
  }

  GetEmployee_attendance() {

    let userDetails = JSON.parse(localStorage.getItem("user"));
    let userId = userDetails.id;

    this.attendanceService.getemployee_attendance(userId).subscribe((data: any) => {
      if (data.isSuccess) {
        this.userAttendance = data.data;
        console.log(data.data);

        for (var i = 0; i < data.data.length; i++) {
          let SignOutDate = new Date(data.data[i].signOut);

          if (data.data[i].signOut == null) {
            this.disable_SignIn = true;
            var signIn_Date = new Date(data.data[i].signIn);
            this.countupTimerService.startTimer(signIn_Date);
          }
          else if (data.data[i].signOut != null && SignOutDate.getDate() == this.currentDate.getDate()) {
            this.disable_SignOut = true;
            this.disable_SignIn = true;
          }
        }
      } else {
        this.sharedService.showPopup(data.Message);
      }
    })
  }

}
