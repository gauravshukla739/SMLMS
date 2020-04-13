import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared/services/shared.service.';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthenticationService } from '../../../../core/services/authentication.service';
import { RoleService } from '../../../../core/services/role.service';
import { DepartmentService } from '../../../../core/services/department.service';
@Component({
  selector: 'app-user-create-update',
  templateUrl: './create-update.component.html',
})

export class UserCreateUpdateComponent implements OnInit {

  constructor(private authService: AuthenticationService, private sharedService: SharedService, private route: ActivatedRoute, private roleService: RoleService, private deptService: DepartmentService, private router: Router) { }

  queryFields: any;
  isAdd: boolean = true;
  user: any = {};
  roles: any=[];
  departments: any=[];
  ngOnInit() {
    this.user.roleName = "";
    this.user.departmentId = "";
    this.route.queryParamMap
      .subscribe(params => {
        this.queryFields = { ...params };
        debugger;
        if (this.queryFields.params.id != undefined) {
          this.user.id = this.queryFields.params.id;
          this.isAdd = false;
          this.getAccountInfo();
        }
      });
    if (this.isAdd) {
      this.getRoles();
      this.getDepartments();
    }
  }
  getRoleId() {
    var roleDetail = this.roles.filter(x => x.name == this.user.roleName)[0];
    if (roleDetail != null || roleDetail != undefined) {
      this.user.roleId = roleDetail.id;
    }
  }

  getDepartmentName() {
    debugger;
    var deptDetail = this.departments.filter(x => x.id == this.user.departmentId)[0];
    if (deptDetail != null || deptDetail != undefined) {
      this.user.departmentName = deptDetail.name;
    }
  }
  getAccountInfo() {
    debugger;
    var response = this.authService.detail(this.user.id).subscribe((data: any) => {
      debugger;
      console.log(data);
      if (data.isSuccess) {
        this.user = data.data;
        if (this.user.dateOfBirth != null || this.user.dateOfBirth != undefined)
          this.user.dateOfBirth = this.formatDate(this.user.dateOfBirth)
        if (this.user.dateOfJoin != null || this.user.dateOfJoin != undefined)
          this.user.dateOfJoin = this.formatDate(this.user.dateOfJoin)
        if (this.user.dateOfAppointment != null || this.user.dateOfAppointment != undefined)
          this.user.dateOfAppointment = this.formatDate(this.user.dateOfAppointment)
        if (this.user.dateOfLeave != null || this.user.dateOfLeave != undefined)
          this.user.dateOfLeave = this.formatDate(this.user.dateOfLeave)
      } else {
        this.sharedService.showPopup(data.message);
      }
    });
    response.add(() => {
      this.sharedService.stopLoading();
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

  back() {
    this.router.navigate(['/user']);
  }

  onSubmit(formValid: any) {
    this.sharedService.startLoading();
    if (this.isAdd) {
      var response = this.authService.register(this.user).subscribe((data: any) => {
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
    } else {
      var response = this.authService.update(this.user).subscribe((data: any) => {
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


}
