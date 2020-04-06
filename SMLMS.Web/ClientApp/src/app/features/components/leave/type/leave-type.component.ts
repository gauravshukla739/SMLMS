import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/core/services/user.service';
import { SharedService } from 'src/app/shared/services/shared.service.';
import { Router } from '@angular/router';
import { ConfirmDialogService } from '../../../../shared/services/confirm-dialog-service.service';
import { LeaveService } from '../../../../core/services/leave.service';

@Component({
  selector: 'app-leave-type',
  templateUrl: './leave-type.component.html',
})
export class LeaveTypeComponent implements OnInit {
  leavetype = {};
  leaveTypes = [];
  isAddEdit = false;
  constructor(private userService: UserService,
    private sharedService: SharedService,
    private confirmDialogService: ConfirmDialogService,
    private leaveService: LeaveService,
    private router: Router) {

  }
  ngOnInit() {
    this.getAll();
  }

  getAll() {
    this.leaveService.all().subscribe((data: any) => {
      if (data.isSuccess) {
        this.leaveTypes = data.data;
      } else {
        this.sharedService.showPopup(data.Message);
      }
    })
  }
  addNew() {
    this.isAddEdit = true;
    this.leavetype = {};
  }
  edit(rec) {
    this.isAddEdit = true;
    this.leavetype = rec;
  }
  cancel() {
    this.leavetype = {};
    this.isAddEdit = false;
  }
  onSubmit(formValid: any) {
    this.leaveService.post(this.leavetype).subscribe((res: any) => {
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
      this.leaveService.delete(id).subscribe((data: any) => {
        if (data.isSuccess) {
          let index = this.leaveTypes.indexOf(rec);
          this.leaveTypes.splice(index, 1);
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
