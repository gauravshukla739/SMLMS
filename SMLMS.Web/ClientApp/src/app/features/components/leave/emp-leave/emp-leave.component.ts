import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/core/services/user.service';
import { SharedService } from 'src/app/shared/services/shared.service.';
import { Router } from '@angular/router';
import { ConfirmDialogService } from '../../../../shared/services/confirm-dialog-service.service';
import { LeaveService } from '../../../../core/services/leave.service';

@Component({
  selector: 'app-emp-leave',
  templateUrl: './emp-leave.component.html',
})
export class EmployeeLeaveComponent implements OnInit {
  leavetype = {};
  data = [];
  isAddEdit = false;
  constructor(private userService: UserService,
    private sharedService: SharedService,
    private confirmDialogService: ConfirmDialogService,
    private leaveService: LeaveService,
    private router: Router) {

  }

  pageNumber = 1;
  pageSize = 5;
  totalRecord = 0;

  ngOnInit() {
    this.getAll();
  }

  getAll() {
    this.leaveService.allEmpLeaves().subscribe((data: any) => {
      if (data.isSuccess) {
        console.log(data);
        this.data = data.data;
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


  goToPage(n: number): void {
    this.pageNumber = n;
    this.getAll();
  }

  onNext(): void {
    this.pageNumber++;
    this.getAll();
  }

  onPrev(): void {
    this.pageNumber--;
    this.getAll();
  }
  changePageSize() {
    this.pageNumber = 1;
    this.getAll();
  }

}
