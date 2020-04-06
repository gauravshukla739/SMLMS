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

}
