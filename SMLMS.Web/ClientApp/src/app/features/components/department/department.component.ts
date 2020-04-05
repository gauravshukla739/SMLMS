import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/core/services/user.service';
import { SharedService } from 'src/app/shared/services/shared.service.';
import { Router } from '@angular/router';
import { ConfirmDialogService } from '../../../shared/services/confirm-dialog-service.service';
import { DepartmentService } from '../../../core/services/department.service';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
})
export class DepartmentComponent implements OnInit {
  dep = {};
  departments = [];
  isAddEdit = false;
  constructor(private userService: UserService,
    private sharedService: SharedService,
    private confirmDialogService: ConfirmDialogService,
    private deptService: DepartmentService,
    private router: Router) {

  }
  ngOnInit() {
    this.getAll();
  }

  getAll() {
    this.deptService.all().subscribe((data: any) => {
      if (data.isSuccess) {
        this.departments = data.data;
      } else {
        this.sharedService.showPopup(data.Message);
      }
    })
  }
  addNew() {
    this.isAddEdit = true;
    this.dep = {};
  }
  edit(rec) {
    this.isAddEdit = true;
    this.dep = rec;
  }
  cancel() {
    this.dep = false;
    this.isAddEdit = false;
  }
  onSubmit(formValid: any) {
    this.deptService.post(this.dep).subscribe((res: any) => {
      if (res.isSuccess) {
        this.isAddEdit = false;
        this.getAll();
      }
      else {
        alert(res.message);
      }
    });
  }

  delete(id: any, rec: any) {
    if (confirm("are you sure wan to delete?")) {
      this.deptService.delete(id).subscribe((data: any) => {
        if (data.isSuccess) {
          let index = this.departments.indexOf(rec);
          this.departments.splice(index, 1);
          this.sharedService.showPopup("successfully deleted");
        } else {
          this.sharedService.showPopup(data.Message);
        }
      });
    }
    else {
      this.sharedService.showPopup("successfully deleted");
    }

  }
}
