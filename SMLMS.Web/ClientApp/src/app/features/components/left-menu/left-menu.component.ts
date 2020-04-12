import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared/services/shared.service.';

@Component({
  selector: 'app-left-menu',
  templateUrl: './left-menu.component.html',
})
export class LeftMenuComponent implements OnInit {

  isOpne = true;
  constructor(private sharedService :SharedService) { 
  }

  userRole: string;
  permission: any;
  ngOnInit() {
    this.userRole = this.sharedService.user.roleName || "";
    this.permission = this.sharedService.setRolePermission;
  }

  showHideLeftNav() {
    this.isOpne = !this.isOpne;
  }
  openNav() {

  }
}
