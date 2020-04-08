import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared/services/shared.service.';
import { TaskService } from 'src/app/core/services/Task.service';
import { UserService } from '../../../../core/services/user.service';
import { DepartmentService } from '../../../../core/services/department.service';
import { ENGINE_METHOD_PKEY_ASN1_METHS } from 'constants';

@Component({
  selector: 'app-setting',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css']
})
export class TaskComponent implements OnInit {

  constructor(private taskService: TaskService, private deptService: DepartmentService, private sharedService: SharedService,private userService:UserService ) { }
  isAddEdit = false;

  pageNumber = 1;
  pageSize = 5;
  totalRecord = 0;
  userRole: string;
  task: any = {};   
  taskList: any;
  users: any;
  myTaskList: any;
  dept: any = "";/*this.sharedService.user.departmentId;
*/  departments: any = [];
  ngOnInit() {
    debugger;
    this.task.employeeId = this.sharedService.user.id;
    this.userRole = this.sharedService.user.roleName || "";
    this.getTask();
    this.getMyTask();
    this.getUsers();
    this.getDepartments();
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
  getUsers() {
    this.userService.getAll().subscribe((data: any) => {
     if (data.isSuccess) {
        this.users = data.data;
      } else {
        this.sharedService.showPopup(data.message);
      } 
    });
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
  getTask() {
    this.task.employeeId = this.sharedService.user.id;
    if (this.userRole === 'Admin') {
      this.taskService.getTask().subscribe((data: any) => {
        this.taskList = data.data;
      });
    }
    else {
      this.taskService.getTaskByUser(this.task.employeeId).subscribe((data: any) => {
        this.taskList = data.data;
      });
    }   
  }
  getTaskByDept() {
    this.taskService.getTaskByDepartment(this.dept).subscribe((data: any) => {
      this.taskList = data.data;
    });
  }
  getMyTask() {
    this.task.employeeId = this.sharedService.user.id;
    this.taskService.getMyTaskByUser(this.task.employeeId).subscribe((data: any) => {
      this.myTaskList = data.data;
      this.totalRecord = data.data.length;
    });
  }
  
  onSubmit(formValid: any) {
    this.task.departmentId = this.sharedService.user.departmentId;
    this.task.employeeId = this.sharedService.user.id;
    this.taskService.addTask(this.task).subscribe((res: any) => {
      debugger;
      if (res.isSuccess) {
        this.isAddEdit = false;
        this.getTask();
        this.getMyTask();
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
          this.getMyTask();
        }
        else {
          alert(res.message);
        }
      });
    }
  }
  goToPage(n: number): void {
    this.pageNumber = n;
    this.getMyTask();
  }

  onNext(): void {
    this.pageNumber++;
    this.getMyTask();
  }

  onPrev(): void {
    this.pageNumber--;
    this.getMyTask();
  }
  changePageSize() {
    this.pageNumber = 1;
    this.getMyTask();
  }
}
