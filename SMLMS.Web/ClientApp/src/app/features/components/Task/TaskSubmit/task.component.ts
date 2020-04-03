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
  task: any = {};   
  taskList: any;
  ngOnInit() {
    this.task.employeeId = this.sharedService.user.id;
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
    this.taskService.getTaskByUser(this.task.employeeId).subscribe((data: any) => {
      this.taskList = data.data;
    });
  }
  
  onSubmit(formValid: any) {
    this.task.departmentId = "7D4E4733-5E63-427D-3AF2-08D7D736AD45";
    //this.task.employeeId = "7D4E4733-5E63-427D-3AF2-08D7D736AD59";
    this.task.employeeId = this.sharedService.user.id;
    this.taskService.addTask(this.task).subscribe((res: any) => {
      if (res.isSuccess) {
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
