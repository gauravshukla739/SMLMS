import { Component, OnInit, ViewChild } from '@angular/core';
import { SharedService } from 'src/app/shared/services/shared.service.';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthenticationService } from '../../../../core/services/authentication.service';
import { RoleService } from '../../../../core/services/role.service';
import { DepartmentService } from '../../../../core/services/department.service';
import { UserService } from '../../../../core/services/user.service';
@Component({
  selector: 'app-account-detail',
  templateUrl: './detail.component.html',
})

export class AccountDetailComponent implements OnInit {

  constructor(private authService: AuthenticationService, private userService: UserService, private sharedService: SharedService, private route: ActivatedRoute, private roleService: RoleService, private deptService: DepartmentService, private router: Router) { }

  queryFields: any;
  user: any = {};
  roleName: any;
  department: any;
  attachment: any;
  formData: FormData;
  image: any;
  imagesrc: any;
  ngOnInit() {
    this.roleName = this.sharedService.user.roleName;
    this.department = this.sharedService.user.departmentName;
    this.image = (this.sharedService.user.image == "") ? "/assets/images/user.png" : this.sharedService.user.image;
    this.route.queryParamMap
      .subscribe(params => {
        this.queryFields = { ...params };
        if (this.queryFields.params.id != undefined) {
          this.user.id = this.queryFields.params.id;
          this.getAccountInfo();
        }
      });
 
  }
  uploadImage() {
    debugger;
    this.formData = new FormData();
    this.formData.append("file", this.attachment);
    var response = this.userService.uploadImage(this.formData).subscribe((data: any) => {
      debugger;
      console.log(data);
      if (data.isSuccess) {
        this.userService.imageChange(this.imagesrc);
        this.sharedService.showPopup(data.message);
      } else {
        this.sharedService.showPopup(data.message);
      }
     
    });
    response.add(() => {
      this.sharedService.stopLoading();
    })
  }
 /* @ViewChild('fileInput')*/ fileInput: any;
  public previewImage(event:any) {
    debugger;
   // let nativeElement: HTMLInputElement = this.fileInput.nativeElement;
    this.imagesrc = event;
    var reader = new FileReader();
    reader.onload = function () {
      var output = document.getElementById('profileImage') as any;
      output.src = reader.result;
    }
    this.attachment = event.target.files[0];
    reader.readAsDataURL(event.target.files[0]);
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

}
