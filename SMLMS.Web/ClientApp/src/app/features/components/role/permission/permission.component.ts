import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared/services/shared.service.';
import { Router } from '@angular/router';
import { ConfirmDialogService } from '../../../../shared/services/confirm-dialog-service.service';
import readXlsxFile from 'read-excel-file';
import { RoleService } from '../../../../core/services/role.service';

@Component({
  selector: 'app-task-role-permission',
  templateUrl: './permission.component.html',
})
export class TaskRolePermissionComponent implements OnInit {

  rolesPermissionList = [];

 rolepermission: any = {};
  rolePermissionAdd=[];
  constructor(private roleService: RoleService,
    private sharedService: SharedService,
    private confirmDialogService: ConfirmDialogService,
    private router: Router) {

  }



  upload() {
    const input = document.getElementById('input') as any;

    input.addEventListener('change', () => {
      readXlsxFile(input.files[0]).then((rows) => {
        debugger;
        for (var i = 1; i < rows[0].length; i++) {
          for (var j = 1; j < rows.length; j++) {
            this.rolepermission = {};
            this.rolepermission.TaskName = rows[j][0];
            this.rolepermission.RoleName = rows[0][i];
            this.rolepermission.Permission = rows[j][i];
            this.rolePermissionAdd.push(this.rolepermission);
          }
        }
        this.roleService.permission(this.rolePermissionAdd).subscribe((data: any) => {
          console.log(data);
          if (data.isSuccess) {
            this.sharedService.showPopup(data.message);
            this.getAllPermissionList();
          } else {
            this.sharedService.showPopup(data.message);
          }
        })
      
      })
    })
  }

  ngOnInit() {
    this.upload();
    this.getAllPermissionList();
  }


  getPermissionDetails(rec: any, roleName: any, taskName: any) {
    return rec.filter(x => x.roleName == roleName && x.taskName == taskName);
  }
  roleList: any;
  taskList: any;

  getAllPermissionList() {
    this.rolesPermissionList = [];
    this.roleService.getpermission().subscribe((data: any) => {
      console.log(data);
      if (data.isSuccess) {
        var res = data.data;
        this.roleList = [...new Set(res.map(x => x.roleName))];
        this.taskList = [...new Set(res.map(x => x.taskName))];
        for (var i = 0; i < this.taskList.length; i++) {
          var t1 = { 'Task': this.taskList[i] };
          var s1 = "";
          var object3;
          for (var j = 0; j < this.roleList.length; j++) {
           
            if (j < this.roleList.length - 1)
              s1 = s1 + '"' + this.roleList[j] + '"' + ":" + '"' + this.getPermissionDetails(res, this.roleList[j], this.taskList[i])[0].permission.toUpperCase() + '"' + ",";
            else
              s1 = s1 + '"' + this.roleList[j] + '"' + ":"  + '"'+this.getPermissionDetails(res, this.roleList[j], this.taskList[i])[0].permission.toUpperCase()+'"' ;
          }
          s1 = "{" + s1 + "}";
          var t2 = JSON.parse(s1);
          object3 = { ...t1, ...t2 };
          this.rolesPermissionList.push(object3);
         
        }
        console.log(this.rolesPermissionList);
       this.roleList.splice(0, 0, 'Task');
      } else {
        this.sharedService.showPopup(data.message);
      }
    })
  }

}
