import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared/services/shared.service.';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthenticationService } from '../../../../core/services/authentication.service';
import { RoleService } from '../../../../core/services/role.service';
import { DepartmentService } from '../../../../core/services/department.service';
import { UserService } from '../../../../core/services/user.service';
@Component({
  selector: 'app-user-promote',
  templateUrl: './promote.component.html',
})

export class PromoteUserComponent implements OnInit {

  constructor(private authService: AuthenticationService, private sharedService: SharedService, private route: ActivatedRoute, private roleService: RoleService, private deptService: DepartmentService, private router: Router, private userService: UserService) { }

  queryFields: any;
  user: any = {};
  roles: any=[];
  departments: any=[];
  ngOnInit() {
    this.route.queryParamMap
      .subscribe(params => {
        this.queryFields = { ...params };
        debugger;
        if (this.queryFields.params.id != undefined) {
          this.user.id = this.queryFields.params.id;
          this.getAccountInfo();
        }
      });
  }

  getAccountInfo() {
    debugger;
    var response = this.authService.detail(this.user.id).subscribe((data: any) => {
      debugger;
      console.log(data);
      if (data.isSuccess) {
        this.user = data.data;
      } else {
        this.sharedService.showPopup(data.message);
      }
    });
    response.add(() => {
      this.sharedService.stopLoading();
    })
  }

  getRoles() {
    var response = this.roleService.all().subscribe((data: any) => {
      console.log(data);
      if (data.isSuccess) {
        this.roles = data.data;
      } else {
        this.sharedService.showPopup(data.message);
      }
    });
    response.add(() => {
      this.sharedService.stopLoading();
    })
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

  onSubmit(formValid: any) {
    this.sharedService.startLoading();
    var response = this.userService.promote(this.user).subscribe((data: any) => {
        console.log(data);
        if (data.isSuccess) {
          this.sharedService.showPopup(data.message);
          this.router.navigate(['/user']);
        } else {
          this.sharedService.showPopup(data.message);
        }
      });
      response.add(() => {
        this.sharedService.stopLoading();
      })
    }
  


}
