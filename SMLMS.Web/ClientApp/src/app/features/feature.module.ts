import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { MainLayoutComponent } from './layout/main-layout/main-layout.component';
import { FeatureRoutingModule } from './feature-routing.module';
import { LeftMenuComponent } from './components/left-menu/left-menu.component';
import { HeaderNavComponent } from './components/header-nav/header-nav.component';
import { LoginComponent } from './components/account/login/login.component';
import { SettingComponent } from './components/setting/setting.component';
import { SharedModule } from '../shared/shared.module';
import { AccountLayoutComponent } from './layout/account-layout/account-layout.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ForgotPasswordComponent } from './components/password/forgot/forgot.component';
import { ResetPasswordComponent } from './components/password/reset/reset.component';
import { ChangePasswordComponent } from './components/password/change/change.component';
import { UserCreateUpdateComponent } from './components/user/create-update/create-update.component';
import { TaskComponent } from './components/task/tasksubmit/task.component';


import { UserComponent } from './components/user/list/list.component';
import { DepartmentComponent } from './components/department/department.component';
import { RoleComponent } from './components/role/role.component';
import { LeaveTypeComponent } from './components/leave/type/leave-type.component';
import { LeaveRequestComponent } from './components/leave/leave-request/leave-request.component';
import { ApproveComponent } from './components/leave/approve/approve.component';
import { EmployeeLeaveComponent } from './components/leave/emp-leave/emp-leave.component';
import { LeaveComponent } from './components/leave/leave.component';



@NgModule({
  declarations: [
    MainLayoutComponent,
    LeftMenuComponent,
    HeaderNavComponent,
    LoginComponent,
    SettingComponent,
    AccountLayoutComponent,
    DashboardComponent,
    ForgotPasswordComponent,
    ResetPasswordComponent,
    ChangePasswordComponent,
    UserCreateUpdateComponent,
    UserComponent,
    DashboardComponent,
    TaskComponent,
    DepartmentComponent,
    RoleComponent,
    LeaveTypeComponent,
    LeaveRequestComponent,
        ApproveComponent,
        EmployeeLeaveComponent,
        LeaveComponent
  ],
  imports: [
    FeatureRoutingModule,
    //BrowserModule,
    FormsModule,
    SharedModule
  ],
  providers: [],
  //bootstrap: []
})
export class FeatureModule { }
