import { Component, OnInit } from '@angular/core';
import { LeaveService } from '../../../../core/services/leave.service';
import { SharedService } from '../../../../shared/services/shared.service.';

@Component({
  selector: 'app-approve',
  templateUrl: './approve.component.html',
})
export class ApproveComponent implements OnInit {
  approveRequest: any = {};
  pendingLeaves: any = [];
  userRole: string;
  tokendata: any = []



  pageNumber = 1;
  pageSize = 5;
  totalRecord = 0;

  constructor(private leaveService: LeaveService,  private sharedService: SharedService) { }

  ngOnInit() {
    this.getLeaveDataByRoles();

  }

  getLeaveDataByRoles() {
    this.leaveService.getLeaveDataBasedOnId().subscribe((data: any) => {
     
      if (data.isSuccess) {
        this.pendingLeaves = data.data;
        
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
