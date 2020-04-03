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
  task: any = {};   
  taskList: any;
  ngOnInit() {
    this.task.employeeId = "7D4E4733-5E63-427D-3AF2-08D7D736AD59";
    debugger;
    this.taskService.getTaskByUser(this.task.employeeId).subscribe((data: any) => {
      this.taskList = data.data;
    });
  }
  
  onSubmit(formValid: any) {
    this.task.departmentId = "7D4E4733-5E63-427D-3AF2-08D7D736AD45";
    this.task.employeeId = "7D4E4733-5E63-427D-3AF2-08D7D736AD59";
    debugger;   
    this.taskService.addTask(this.task).subscribe((data: any) => {
      console.log(data);
    });
    this.taskService.getTaskByUser(this.task.employeeId).subscribe((data: any) => {
      this.taskList = data.data;
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
