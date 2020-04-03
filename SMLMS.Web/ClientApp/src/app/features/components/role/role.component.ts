import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/core/services/user.service';
import { SharedService } from 'src/app/shared/services/shared.service.';
import { Router } from '@angular/router';
import { ConfirmDialogService } from '../../../shared/services/confirm-dialog-service.service';
import { DepartmentService } from '../../../core/services/department.service';
import { RoleService } from '../../../core/services/role.service';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
})
export class RoleComponent implements OnInit {
  role = {};
  roles = [];
  isAddEdit = false;
  constructor(private userService: UserService,
    private sharedService: SharedService,
    private confirmDialogService: ConfirmDialogService,
    private roleService: RoleService,
    private router: Router) {

  }
  ngOnInit() {
    this.getAll();
  }

  getAll() {
    this.roleService.all().subscribe((data: any) => {
      if (data.isSuccess) {
        this.roles = data.data;
      } else {
        this.sharedService.showPopup(data.Message);
      }
    })
  }
  addNew() {
    this.isAddEdit = true;
    this.role = {};
  }
  edit(rec) {
    this.isAddEdit = true;
    this.role = rec;
  }
  cancel() {
    this.role = false;
    this.isAddEdit = false;
  }
  onSubmit(formValid: any) {
    this.roleService.post(this.role).subscribe((res: any) => {
      if (res.isSuccess) {
        this.isAddEdit = false;
        this.getAll();
      }
      else {
        alert(res.message);
      }
    });
  }
}
