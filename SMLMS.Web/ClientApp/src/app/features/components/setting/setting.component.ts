import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/core/services/user.service';
import { SharedService } from 'src/app/shared/services/shared.service.';

@Component({
  selector: 'app-setting',
  templateUrl: './setting.component.html',
  styleUrls: ['./setting.component.css']
})
export class SettingComponent implements OnInit {

  // constructor(private userService: UserService, private sharedService: SharedService) { }

   ngOnInit() {
  //   this.getPermissions();
  }
  // permissions: any;
  // getPermissions() {
  //   let response = this.userService.getPermissions().subscribe((data: any) => {
  //     console.log(data);
  //     if (data.IsSuccess) {
  //       this.permissions = data.Data;
  //     } else {
  //       this.sharedService.showPopup(data.Message);
  //     }
  //   },
  //     (error) => {
  //       this.sharedService.showPopup("Unable to process your request ...");
  //     });
  // }
  // confirmClick(event: any) {
  //   if (confirm("are you sure ?")) {

  //   }
  //   else
  //     event.preventDefault();
  // }
  // setPermission(event: any, permissions: any) {
  //   let response = this.userService.updatePermissions(permissions).subscribe((data: any) => {
  //     if (data.IsSuccess) {
  //       this.getPermissions();
  //     } else {
  //       this.sharedService.showPopup(data.Message);
  //     }
  //   });
  // }

}
