import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainLayoutComponent } from './layout/main-layout/main-layout.component';
import { AccountLayoutComponent } from './layout/account-layout/account-layout.component';
import { LoginComponent } from './components/account/login/login.component';
import { SettingComponent } from './components/setting/setting.component';
import { TaskComponent } from './components/task/tasksubmit/task.component';
import { AuthGuard } from '../core/auth-guard/auth-guard';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ForgotPasswordComponent } from './components/password/forgot/forgot.component';
import { ResetPasswordComponent } from './components/password/reset/reset.component';
import { ChangePasswordComponent } from './components/password/change/change.component';
import { UserCreateUpdateComponent } from './components/user/create-update/create-update.component';
import { UserComponent } from './components/user/list/list.component';
import { DepartmentComponent } from './components/department/department.component';
import { RoleComponent } from './components/role/role.component';
import { LeaveTypeComponent } from './components/leave/type/leave-type.component';
import { LeaveRequestComponent } from './components/leave/leave-request/leave-request.component';
import { ApproveComponent } from './components/leave/approve/approve.component';


const routes: Routes = [
  {
    path: '', component: MainLayoutComponent,
    canActivate: [AuthGuard],
    children: [
      { path: '', component: DashboardComponent },
      { path: 'setting', component: SettingComponent },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'password/change', component: ChangePasswordComponent },
      {
        path: 'user',
        children: [
          { path: '', component: UserComponent },
          { path: 'add', component: UserCreateUpdateComponent },
          { path: 'update', component: UserCreateUpdateComponent }
        ]
      },
      { path: 'department', component: DepartmentComponent },
      { path: 'role', component: RoleComponent },
      {
        path: 'task', component: TaskComponent,
        children: [
          { path: '', component: TaskComponent },
          { path: 'create', component: TaskComponent },
          { path: 'edit/:id', component: TaskComponent },
        ]
      },
      {
        path: 'leave', component: LeaveTypeComponent,
        children: [
          { path: '', component: LeaveTypeComponent },
          { path: 'type', component: LeaveTypeComponent }
          
        ]
      },

      { path: 'request', component: LeaveRequestComponent },
      { path: 'approve', component: ApproveComponent },
    ]
  },
  {
    path: 'secure', component: AccountLayoutComponent,
    children: [
      { path: '', component: LoginComponent },
      { path: 'login', component: LoginComponent },
      { path: 'password/forgot', component: ForgotPasswordComponent },
      { path: 'password/reset', component: ResetPasswordComponent },
    ]
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FeatureRoutingModule { }
