import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/core/services/user.service';
import { SharedService } from 'src/app/shared/services/shared.service.';
import { Router } from '@angular/router';
import { ConfirmDialogService } from '../../../../shared/services/confirm-dialog-service.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './list.component.html',
})
export class UserComponent implements OnInit {


  displayColumn = [
     "email"
    , "phoneNumber"
    , "dateOfJoin"
    , "dateOfAppointment"
    , "dateOfLeave"
    , "departmentId"
    , "address"   
    , "firstName"
    , "lastName"
    , "dateOfBirth"
  ];
  users = [];
  constructor(private userService: UserService,
    private sharedService: SharedService,
    private confirmDialogService: ConfirmDialogService,
    private router: Router) {

  }
  ngOnInit() {
    this.getAllUsers();
  }

  getAllUsers() {
    this.userService.getAll().subscribe((data: any) => {
      console.log(data);
      if (data.isSuccess) {
        this.users = data.data;
      } else {
        this.sharedService.showPopup(data.message);
      }
    })
  }

  delete(userId: any, rec: any) {
    if (confirm("are you sure wan to delete?")) {
      this.userService.delete(userId).subscribe((data: any) => {
        if (data.IsSuccess) {
          let index = this.users.indexOf(rec);
          this.users.splice(index, 1);
          this.sharedService.showPopup("successfullt deleted");
          //rec.LockoutEnabled = lockStatus;
        } else {
          this.sharedService.showPopup(data.Message);
        }
      });
    }
    //let self = this;
    //this.confirmDialogService.confirmThis( "Are you sure want to unblock?", function () {
    //  self.userService.delete(userId).subscribe((data: any) => {
    //    if (data.IsSuccess) {
    //    //
    //      //rec.LockoutEnabled = lockStatus;
    //    } else {
    //      self.sharedService.showPopup(data.Message);
    //    }
    //  });
    //}, function () {

    //});
  }
}
