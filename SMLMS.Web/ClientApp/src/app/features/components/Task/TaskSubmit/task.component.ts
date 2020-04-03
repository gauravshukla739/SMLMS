import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared/services/shared.service.';
import { TaskService } from 'src/app/core/services/Task.service';

@Component({
  selector: 'app-setting',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css']
})
export class TaskComponent implements OnInit {

   constructor(private taskService: TaskService) { }

   ngOnInit() {
  }
  task: any = {};

  onSubmit(formValid: any) {
    debugger;
    this.task.departmentId = "073F0934-AE40-4E41-958E-5792B3C78BEA";
    this.task.employeeId = "524B7F42-A369-42D4-0B96-08D7D63E1A88";
    this.taskService.addTask(this.task).subscribe((data: any) => {
      console.log(data);
    });
    //var response = this.authService.login(this.user).subscribe((data: any) => {
    //  console.log(data);
    //  if (data.IsSuccess) {
    //    this.sharedService.showPopup("Successfully login");
    //    localStorage.setItem("user-token", data.Data.Token);
    //    this.sharedService.accessToken = data.Data.Token;
    //    this.sharedService.setUser(data.Data.User);
    //    this.router.navigate(['/permissions']);
    //  } else {
    //    this.sharedService.showPopup(data.Message);
    //    //this.sharedService.showPopup("Login failed , Invalid user");
    //  }
    //});
  }
}
