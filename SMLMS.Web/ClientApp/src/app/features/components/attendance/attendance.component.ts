import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { AuthenticationService } from '../../../core/services/authentication.service';
import { SharedService } from '../../../shared/services/shared.service.';
import { Router } from '@angular/router';
import { AttendanceService } from '../../../core/services/attendance.service';
import { NgForm } from '@angular/forms';
import { CountupTimerService } from '../../../core/services/countup.service';
import { countUpTimerConfigModel, timerTexts } from '../../../core/models/countup-timer';
import { DepartmentService } from '../../../core/services/department.service';
import { UserService } from '../../../core/services/user.service';



@Component({
  selector: 'app-attendance',
  templateUrl: './attendance.component.html',
  styleUrls: ['./attendance.component.css']
})
export class AttendanceComponent implements OnInit {

  displayColumn = ["firstName", "lastName", "signIn", , "signOut", "createdOn", "totalTime"];
  userAttendance = [];
  employeeAttendanceTrack = [];
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
  departments: any = [];
  users: any = [];
  SelectedMonth: any = "";
  Selecteddepartment: any = "";
  Selecteduser: any = "";


  pageNumber = 1;
  pageSize = 5;
  totalRecord = 0;

  constructor(private countupTimerService: CountupTimerService, private authService: AuthenticationService, private sharedService: SharedService, private router: Router, private attendanceService: AttendanceService, private deptService: DepartmentService, private userService: UserService) {

  }



  ngOnInit() {
    this.userRole = this.sharedService.user.roleName || "";
    //this.getAllUsers();
    if (this.userRole != "Admin") {
      this.GetEmployee_attendance();
    }
    else {
      this.Employee_PresentAbsent();
    }
   
    this.currentDate = new Date();

    //Timer
    this.getTimerValue();
    this.timerConfig = new countUpTimerConfigModel();
    this.timerTextConfig = new timerTexts();
    this.timerConfig = this.countUpTimerConfig ? Object.assign(this.countUpTimerConfig) : null;
    this.timerTextConfig = this.countUpTimerConfig && this.countUpTimerConfig.timerTexts ? Object.assign(this.countUpTimerConfig.timerTexts) : null;
    this.getDepartments();
    this.getUsers();
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


  //PAgination

  sliceStart = 0;
  sliceEnd = 5;
  goToPage(n: number): void {
    this.pageNumber = n;
    this.sliceArray();
    this.GetEmployee_attendance();
  }

  onNext(): void {
    debugger;
    this.pageNumber++;
    this.sliceArray();
    this.GetEmployee_attendance();
  }

  sliceArray(): void {
    this.sliceStart = this.pageSize * (this.pageNumber - 1);
    this.sliceEnd = this.sliceStart + this.pageSize;
  }

  onPrev(): void {
    this.pageNumber--;
    this.sliceArray();
    this.GetEmployee_attendance();
  }
  changePageSize() {
    this.pageNumber = 1;
    this.sliceStart = 0;
    this.sliceEnd = this.pageSize;
    this.GetEmployee_attendance();
  }


  getDepartments() {
    var response = this.deptService.all().subscribe((data: any) => {
      console.log(data);
     
      if (data.isSuccess) {
        this.departments = data.data;
      } else {
        this.sharedService.showPopup(data.message);
      }
    });
    response.add(() => {
      this.sharedService.stopLoading();
    })
  }

  Punch_In() {
    this.sharedService.startLoading();

    this.attendanceService.CreateOrUpDate().subscribe((data: any) => {
      if (data.isSuccess) {
        this.GetEmployee_attendance();
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
        this.GetEmployee_attendance();
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
    let userRole = userDetails.roleName;
    debugger;
    this.attendanceService.getemployee_attendance(userId, userRole).subscribe((data: any) => {
      if (data.isSuccess) {
        this.userAttendance = data.data;
        this.totalRecord = this.userAttendance.length;
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

  selectedFilter() {

    let userDetails = JSON.parse(localStorage.getItem("user"));
    let userId = userDetails.id;
    debugger;
    this.attendanceService.Filter_attendance(userId, this.userRole, this.SelectedMonth, this.Selecteddepartment, this.Selecteduser).subscribe((data: any) => {
      if (data.isSuccess) {
        debugger;
        if (this.userRole == "Admin") {
          this.employeeAttendanceTrack = data.data;
          this.sharedService.stopLoading();
        }
        else {
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
        }
      } else {
        this.sharedService.showPopup(data.Message);
      }
    })
  }

  getUsers() {
    this.userService.getAll().subscribe((data: any) => {
      console.log(data);
      if (data.isSuccess) {
        this.users = data.data;
      } else {
        this.sharedService.showPopup(data.message);
      }
    })
  }


  Employee_PresentAbsent() {
    let userDetails = JSON.parse(localStorage.getItem("user"));
    let userId = userDetails.id;
    let role = userDetails.roleName;
    this.attendanceService.getEmployeeAttendance(userId, role).subscribe((data: any) => {
      debugger;
      if (data.isSuccess) {
        
        this.employeeAttendanceTrack = data.data;
        this.sharedService.stopLoading();
        
      } else {
        this.sharedService.showPopup(data.Message);
      }
    });
  }


}
