import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/core/services/user.service';
import { SharedService } from 'src/app/shared/services/shared.service.';
import { Router } from '@angular/router';
import { ConfirmDialogService } from '../../../../shared/services/confirm-dialog-service.service';
//import readXlsxFile from 'read-excel-file';
import { DepartmentService } from '../../../../core/services/department.service';
import { RoleEnum } from '../../../../shared/enum/role.enum';

@Component({
  selector: 'app-user-list',
  templateUrl: './list.component.html',
})
export class UserComponent implements OnInit {

  pageNumber = 1;
  pageSize = 5;
  totalRecord = 0;
  enum = RoleEnum;
  displayColumn = [
     "email"
    , "phoneNumber"
    , "dateOfJoin"
    , "departmentName"
    , "address"   
    , "firstName"
    , "lastName"
    , "dateOfBirth"
  ];
  users = [];

  userDetail: any = {};
  user = [];

  selectedDepartment: any="";
  selectedUser: any="";
  constructor(private userService: UserService,
    private sharedService: SharedService,
    private confirmDialogService: ConfirmDialogService,
    private router: Router, private deptService: DepartmentService) {
   
  }
  sliceStart = 0;
  sliceEnd = 5;
  departments: any = [];
  roleName: any;

  formatImage(img: any) {
    return (img == "") ? "assets/images/user.png" : img;
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

  goToPage(n: number): void {
    this.pageNumber = n;
    this.sliceArray();
    this.getAllUsers();
  }

  onNext(): void {   
    this.pageNumber++;
    this.sliceArray();
    this.getAllUsers();
  }

  sliceArray(): void {
    this.sliceStart = this.pageSize * (this.pageNumber - 1);
    this.sliceEnd = this.sliceStart + this.pageSize;
  }

  onPrev(): void {
    this.pageNumber--;
    this.sliceArray();
    this.getAllUsers();
  }
  changePageSize() {
    this.pageNumber = 1;
    this.sliceStart = 0;
    this.sliceEnd = this.pageSize;
    this.getAllUsers();
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

  //upload() {
  //  const input = document.getElementById('input') as any;

  //  input.addEventListener('change', () => {
  //    readXlsxFile(input.files[0]).then((rows) => {
  //      debugger;
  //      for (var i = 1; i <= rows.length - 1; i++) {
  //        this.userDetail = {};
  //        this.userDetail.firstName = rows[i][0];
  //        this.userDetail.lastName = rows[i][1];
  //        this.userDetail.email = rows[i][2];
  //        this.userDetail.roleName = rows[i][3];
  //        this.userDetail.departmentName = rows[i][4];
  //        this.userDetail.dateOfBirth = (rows[i][5] != null) ? this.formatDate(rows[i][5]) : null;
  //        this.userDetail.dateOfJoin = (rows[i][6] != null) ? this.formatDate(rows[i][6])  : null;
  //        //this.userDetail.dateOfAppointment = (rows[i][7] != null) ? this.formatDate(rows[i][7]) : null;
  //        //this.userDetail.dateOfLeave = (rows[i][8] != null) ? this.formatDate(rows[i][8])  : null;
  //        this.userDetail.phoneNumber = rows[i][7];
  //        this.userDetail.address = rows[i][8];
  //          this.user.push(this.userDetail);
  //      }
  //      this.userService.import(this.user).subscribe((data: any) => {
  //        console.log(data);
  //        if (data.isSuccess) {
  //          this.sharedService.showPopup(data.message);
  //          this.getAllUsers();
  //        } else {
  //          this.sharedService.showPopup(data.message);
  //        }
  //      })
  //      // `rows` is an array of rows
  //      // each row being an array of cells.
  //    })
  //  })
  //}
  deptShow: boolean=true;
  ngOnInit() {
    this.deptShow = this.sharedService.dept();
    this.roleName = this.sharedService.user.roleName;
   // this.upload();
    this.getAllUsers();
    this.getDepartments();
  }

  getAllUsers() {
    this.userService.getAll().subscribe((data: any) => {
      console.log(data);
      debugger;
      if (data.isSuccess) {
        this.users = data.data;
        if (this.roleName == RoleEnum.TL)
          this.users = data.data.filter(x => x.departmentId == this.sharedService.user.departmentId && ((this.selectedUser != "") ? x.email == this.selectedUser : true));
        else
        this.users = data.data.filter(x => ((this.selectedDepartment != "") ? x.departmentId == this.selectedDepartment : true) && ((this.selectedUser != "") ? x.email == this.selectedUser : true));
        this.totalRecord = this.users.length;
      } else {
        this.sharedService.showPopup(data.message);
      }
    })
  }

  selectedFilter() {
    this.pageNumber = 1;
    this.sliceStart = 0;
    this.sliceEnd = this.pageSize;
    this.getAllUsers();
  }

  promote(userId: any, roleId: any, departmentId: any) {
    debugger;
    this.sharedService.promoteRoleId = roleId;
    this.sharedService.promoteDepartmentId = departmentId;
    if (confirm("are you sure wan to promote this user?")) {
      this.router.navigate(['/user/promote'], { queryParams: { id: userId } });
    }
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
