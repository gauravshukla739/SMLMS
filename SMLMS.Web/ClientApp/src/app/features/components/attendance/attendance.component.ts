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
  disableBtn: boolean = false;

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
    debugger;
    this.getAllUsers();
    //this.Cre();
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
        this.disableBtn = true;
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
        this.disableBtn = false;
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
          if (data.data[i].signOut == null) {
            this.disableBtn = true;
            var signIn_Date = new Date(data.data[i].signIn);
            this.countupTimerService.startTimer(signIn_Date);
          }
        }

      } else {
        this.sharedService.showPopup(data.Message);
        //this.sharedService.showPopup("Login failed , Invalid user");
      }
    })
  }


  public timerValue = {
    seconds: '00',
    mins: '00',
    hours: '00',
  }

  Cre() {
    this.attendanceService.CreateOrUpDate().subscribe((data: any) => {
      debugger;
    });
  }
}
