import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';
import { RoleEnum } from "../enum/role.enum";
import { RoleTaskEnum } from "../enum/task.enum";

@Injectable({ providedIn: 'root' })
export class SharedService {
  ApiBaseUrl: string ="http://localhost:52710/api";
  roleEnum = RoleEnum;
  taskEnum = RoleTaskEnum;
  promoteRoleId: any;
  promoteDepartmentId: any;
  constructor(protected http: HttpClient) {
    this.roleEnum = RoleEnum;
    this.taskEnum = RoleTaskEnum;
    this.accessToken = localStorage.getItem("user-token");
    this.user = localStorage.getItem("user") as any != null ? JSON.parse(localStorage.getItem("user") as any) : {};
    this.rolePermisssion = localStorage.getItem("role-permission") as any != null ? JSON.parse(localStorage.getItem("role-permission") as any) : {};
    debugger;
    if ((this.rolePermisssion.length || 0) > 0) {
      this.setRolePermissionList(this.rolePermisssion);
    }
    if ((this.user.id || "NA") != "NA") {
      this.dept();
    }
  }
  rolePermisssion: any;
  setRolePermission: any = {};
  user :any;
  currentUserId: any;
  currentMember: any;
  currentProjectId: any;
  accessToken = "";
  deptShow: boolean = true;
  getData() {
    let r  = Request;
    //return this.http.send(r);
  }
  setUser(user: any){
    this.user = user;
    this.dept();
    localStorage.setItem("user" , JSON.stringify(user));
  }
  dept() {
    return this.deptShow = ((this.user.roleName == RoleEnum.Admin) || (this.user.roleName == RoleEnum.HR) || (this.user.roleName == RoleEnum.PM));
  }
  setPermission(permission: any) {
    debugger;
    this.rolePermisssion = permission;
    this.setRolePermissionList(permission);

    localStorage.setItem("role-permission", JSON.stringify(permission));
  }

  setRolePermissionList(permission: any) {
    this.setRolePermission.Attendance = (this.permissionFilter(permission, RoleTaskEnum.Attendance) == "Y") ? true : false;
    this.setRolePermission.Punch = (this.permissionFilter(permission, RoleTaskEnum.Punch) == "Y") ? true : false;
    this.setRolePermission.LeaveRequest = (this.permissionFilter(permission, RoleTaskEnum.LeaveRequest) == "Y") ? true : false;
    this.setRolePermission.LeaveApprove = (this.permissionFilter(permission, RoleTaskEnum.LeaveApprove) == "Y") ? true : false;
    this.setRolePermission.LeaveType = (this.permissionFilter(permission, RoleTaskEnum.LeaveType) == "Y") ? true : false;
    this.setRolePermission.TaskCRUD = (this.permissionFilter(permission, RoleTaskEnum.TaskCRUD) == "Y") ? true : false;
    this.setRolePermission.Dept = (this.permissionFilter(permission, RoleTaskEnum.Dept) == "Y") ? true : false;
    this.setRolePermission.EmpLeaveStatus = (this.permissionFilter(permission, RoleTaskEnum.EmpLeaveStatus) == "Y") ? true : false;
    this.setRolePermission.Employee = (this.permissionFilter(permission, RoleTaskEnum.Employee) == "Y") ? true : false;
    this.setRolePermission.Role = (this.permissionFilter(permission, RoleTaskEnum.Role) == "Y") ? true : false;
    this.setRolePermission.RolePermission = (this.permissionFilter(permission, RoleTaskEnum.RolePermission) == "Y") ? true : false;
    this.setRolePermission.TodayAttendance = (this.permissionFilter(permission, RoleTaskEnum.TodayAttendance) == "Y") ? true : false;

  }
  permissionFilter(permission: any, task: any) {
    var data = permission.filter(x => x.taskName == task)[0];
    if (data == undefined) {
      return "N";
    } else {
      return data.permission;
    }
  }
  

  private _loading: boolean = false;
  loadingStatus: any = new Subject();

  get loading():boolean {
    return this._loading;
  }

  set loading(value) {
    this._loading = value;
    this.loadingStatus.next(value);
  }

  startLoading() {
    this.loading = true;
  }

  stopLoading() {
    this.loading = false;
  }

  private _showPopup: boolean = false;
  popupMsg: any;
  popupStatus: any = new Subject();

  get showpopup(): boolean {
    return this._showPopup;
  }

  set showpopup(value) {
    this._showPopup = value;
    this.popupStatus.next(this.popupMsg);
  }

  showPopup(msg: any) {
    this.popupMsg = msg;
    this.showpopup = true;
  }

}
