import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared/services/shared.service.';
import { TaskService } from 'src/app/core/services/Task.service';

@Component({
  selector: 'app-setting',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css']
})
export class TaskComponent implements OnInit {

  constructor(private taskService: TaskService, private sharedService: SharedService ) { }
  isAddEdit = false;
  userRole: string;
  task: any = {};   
  taskList: any;
  ngOnInit() {
    this.task.employeeId = this.sharedService.user.id;
    this.userRole = this.sharedService.user.roleName || "";
    this.getTask();
   
  }

  addNew() {
    this.isAddEdit = true;
    this.task = {};
  }
  edit(rec) {
    this.isAddEdit = true;
    this.task = rec;
  }
  cancel() {
    this.task = false;
    this.isAddEdit = false;
  } 
  getTask() {
    this.task.employeeId = this.sharedService.user.id;
    this.taskService.getTaskByUser(this.task.employeeId).subscribe((data: any) => {
      this.taskList = data.data;
    });
  }
  
  onSubmit(formValid: any) {
    this.task.departmentId = "2951FE55-9466-4F37-A515-D3F9B0915EEB";
    this.task.employeeId = this.sharedService.user.id;
    this.taskService.addTask(this.task).subscribe((res: any) => {
      debugger;
      if (res.isSuccess) {
        this.isAddEdit = false;
        this.getTask();
      }
      else {
        alert(res.message);
      }
    });
  }


  delete(taskId: any) {
    if (confirm("are you sure want to delete?")) {
      this.taskService.delete(taskId).subscribe((res: any) => {
        if (res.isSuccess) {
          this.getTask();
        }
        else {
          alert(res.message);
        }
      });
    }
  }
}
