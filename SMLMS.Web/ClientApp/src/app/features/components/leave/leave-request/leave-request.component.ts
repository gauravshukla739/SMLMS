import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/core/services/user.service';
import { SharedService } from 'src/app/shared/services/shared.service.';
import { Router } from '@angular/router';
import { ConfirmDialogService } from '../../../../shared/services/confirm-dialog-service.service';
import { LeaveService } from '../../../../core/services/leave.service';



@Component({
  selector: 'app-leave-request',
  templateUrl: './leave-request.component.html'
})
export class LeaveRequestComponent implements OnInit {
  leaveRequest :any= {};
  
  leaveRequests = [];
  typesofleave: any = [];
  isAddEdit = false;
  istest: boolean = false;
  userRole: string;


  pageNumber = 1;
  pageSize = 5;
  totalRecord = 0;
  minDate: Date;
 


  constructor(private userService: UserService,
    private sharedService: SharedService,
    private confirmDialogService: ConfirmDialogService,
    private leaveService: LeaveService,
    private router: Router) {
  }


  ngOnInit() {
    this.getAll();
    this.getLeaveTypes();
  }

  getLeaveTypes() {
    var response = this.leaveService.all().subscribe((data: any) => {
     // console.log(data);
      if (data.isSuccess) {
        this.typesofleave = data.data;
      } else {
        this.sharedService.showPopup(data.message);
      }
    });
    response.add(() => {
      this.sharedService.stopLoading();
    })
  }

  getAll() {
    this.leaveService.allLeaveRequest().subscribe((data: any) => {
      debugger;
      if (data.isSuccess) {
        this.leaveRequests = data.data;
        debugger;
        this.totalRecord = data.data.length;
        debugger;
      } else {
        this.sharedService.showPopup(data.Message);
      }
    })
  }
  formatDate(date) {
    var d = new Date(date),
      month = '' + (d.getMonth() + 1),
      day = '' + d.getDate(),
      year = d.getFullYear();

    if (month.length < 2)
      month = '0' + month;
    if (day.length < 2)
      day = '0' + day;

    return [year, month, day].join('-');
  }
  addNew() {
    debugger;
    this.isAddEdit = true;
    this.leaveRequest = {};

 
  }
  edit(rec) {
    debugger;
    this.isAddEdit = true;
    this.leaveRequest = rec;
    if (this.leaveRequest.fromDate != null || this.leaveRequest.fromDate != undefined)
      this.leaveRequest.fromDate = this.formatDate(this.leaveRequest.fromDate)
    if (this.leaveRequest.toDate != null || this.leaveRequest.toDate != undefined)
      this.leaveRequest.toDate = this.formatDate(this.leaveRequest.toDate)
  }
  cancel() {
    this.leaveRequest = {};
    this.isAddEdit = false;
  }
  onSubmit(formValid: any) {
    this.leaveService.postLeaveRequest(this.leaveRequest).subscribe((res: any) => {
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
      this.leaveService.deleteLeaveRequest(id).subscribe((data: any) => {
        if (data.isSuccess) {
          alert("successfully deleted")
          this.getAll();
          //this.sharedService.showPopup("successfully deleted");
        } else {
          this.sharedService.showPopup(data.Message);
        }
      });
    }
  }

  //PAgination

  sliceStart = 0;
  sliceEnd = 5;
  goToPage(n: number): void {
    this.pageNumber = n;
    this.sliceArray();
    this.getAll();
  }

  onNext(): void {
    debugger;
    this.pageNumber++;
    this.sliceArray();
    this.getAll();
  }

  sliceArray(): void {
    this.sliceStart = this.pageSize * (this.pageNumber - 1);
    this.sliceEnd = this.sliceStart + this.pageSize;
  }

  onPrev(): void {
    this.pageNumber--;
    this.sliceArray();
    this.getAll();
  }
  changePageSize() {
    this.pageNumber = 1;
    this.sliceStart = 0;
    this.sliceEnd = this.pageSize;
    this.getAll();
  }
}
