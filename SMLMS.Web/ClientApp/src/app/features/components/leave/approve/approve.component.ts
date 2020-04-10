import { Component, OnInit } from '@angular/core';
import { LeaveService } from '../../../../core/services/leave.service';
import { SharedService } from '../../../../shared/services/shared.service.';
import { DepartmentService } from '../../../../core/services/department.service';
import { UserService } from '../../../../core/services/user.service';


@Component({
  selector: 'app-approve',
  templateUrl: './approve.component.html',
})
export class ApproveComponent implements OnInit {
  approveRequest: any = {};
  pendingLeaves: any = [];

  leavesListById: any = [];
  isAddEdit: boolean = false;
  userRole: string;
  tokendata: any = []
  rejectLeave: any = {};

  text: string;
  Selecteddepartment: any;
  Selecteduser: any;

  departments: any = [];
  users: any = [];

  pageNumber = 1;
  pageSize = 5;
  totalRecord = 0;
  prevLeaveid: any;
  dept: any = "";


  constructor(private leaveService: LeaveService, private sharedService: SharedService, private deptService: DepartmentService, private userService: UserService) {
    this.prevLeaveid = null;
  }

  ngOnInit() {

    this.userRole = this.sharedService.user.roleName || "";

    this.getLeaveDataByRoles();
    this.getDepartments();


  }

  getLeaveByDept() {
    if (this.dept == "" || this.dept == null) {
      this.getLeaveDataByRoles();
    } else {
      this.leaveService.getLeaveByDepartment(this.dept).subscribe((data: any) => {
        debugger;
        this.pendingLeaves = data.data;
        this.totalRecord = data.data.length;
      });
    }

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
  getUsers() {
    var response = this.userService.getAll().subscribe((data: any) => {
      console.log(data);
      if (data.isSuccess) {
        this.users = data.data;
      } else {
        this.sharedService.showPopup(data.message);
      }
    })
  }


  getLeaveDataByRoles() {
    this.leaveService.getLeaveDataBasedOnId().subscribe((data: any) => {

      if (data.isSuccess) {
        this.pendingLeaves = data.data;
        this.totalRecord = data.data.length;

      } else {

      }
    })
  }



  onApproval(id: any) {

    this.leaveService.approveLeave(id).subscribe((res: any) => {
      if (res.isSuccess) {
        alert("Sucessfully Approved");
        this.getLeaveDataByRoles();
      }
      else {
        alert(res.message);
      }
    });
  }




  onRejection(id: any, isRejected: boolean) {

    if (this.prevLeaveid != null) {
      this.pendingLeaves.find(item => item.id == this.prevLeaveid).isRejected = isRejected;
    }

    this.pendingLeaves.find(item => item.id == id).isRejected = !isRejected;
    this.prevLeaveid = id;
    this.rejectLeave.reason = "";

  }

  rejectLeaves(id: any) {
    if (this.rejectLeave.reason == "" || this.rejectLeave.reason == null) {
      alert("Reason required.");
      return;
    }
    this.rejectLeave.id = id;
    this.leaveService.rejectLeave(this.rejectLeave).subscribe((res: any) => {

      if (res.isSuccess) {
        alert("Leave Rejected");
        this.getLeaveByDept();

      }
      else {
        alert(res.message);
      }
    });
  }



  //PAgination

  sliceStart = 0;
  sliceEnd = 5;
  goToPage(n: number): void {
    this.pageNumber = n;
    this.sliceArray();
    this.getLeaveDataByRoles();
  }

  onNext(): void {
    debugger;
    this.pageNumber++;
    this.sliceArray();
    this.getLeaveDataByRoles();
  }

  sliceArray(): void {
    this.sliceStart = this.pageSize * (this.pageNumber - 1);
    this.sliceEnd = this.sliceStart + this.pageSize;
  }

  onPrev(): void {
    this.pageNumber--;
    this.sliceArray();
    this.getLeaveDataByRoles();
  }
  changePageSize() {
    this.pageNumber = 1;
    this.sliceStart = 0;
    this.sliceEnd = this.pageSize;
    this.getLeaveDataByRoles();
  }

}
