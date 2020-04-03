import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainLayoutComponent } from './layout/main-layout/main-layout.component';
import { AccountLayoutComponent } from './layout/account-layout/account-layout.component';
import { LoginComponent } from './components/account/login/login.component';
import { SettingComponent } from './components/setting/setting.component';
import { AuthGuard } from '../core/auth-guard/auth-guard';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ForgotPasswordComponent } from './components/password/forgot/forgot.component';
import { ResetPasswordComponent } from './components/password/reset/reset.component';
import { ChangePasswordComponent } from './components/password/change/change.component';
import { UserCreateUpdateComponent } from './components/user/create-update/create-update.component';
import { UserComponent } from './components/user/list/list.component';

const routes: Routes = [
  { path: '', component: MainLayoutComponent , 
  //canActivate:[AuthGuard],
  children:[
    { path: '', component: DashboardComponent },
    { path: 'setting', component: SettingComponent },
      { path: 'password/change', component: ChangePasswordComponent },
    {
      path: 'users', component: UserComponent,
      children: [
        { path: '', component: UserComponent },
          { path: 'add', component: UserCreateUpdateComponent },
          { path: 'update', component: UserCreateUpdateComponent }


      ]
    }
  ]
},
  { path: 'secure', component: AccountLayoutComponent,
  children :[
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
