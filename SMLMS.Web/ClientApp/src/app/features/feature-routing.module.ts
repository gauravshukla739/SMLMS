import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainLayoutComponent } from './layout/main-layout/main-layout.component';
import { AccountLayoutComponent } from './layout/account-layout/account-layout.component';
import { LoginComponent } from './components/account/login/login.component';
import { SettingComponent } from './components/setting/setting.component';
import { TaskComponent } from './components/task/tasksubmit/task.component';
import { AuthGuard } from '../core/auth-guard/auth-guard';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ForgotComponent } from './components/password/forgot/forgot.component';
import { UserComponent } from './components/user/list/list.component';
import { UserFormComponent } from './components/user/create-update/create-update.component';


const routes: Routes = [
  { path: '', component: MainLayoutComponent , 
  //canActivate:[AuthGuard],
  children:[
    { path: '', component: DashboardComponent },
      { path: 'setting', component: SettingComponent },
      { path: 'task', component: TaskComponent },
  ]
},
  { path: 'secure', component: AccountLayoutComponent,
  children :[
    { path: '', component: LoginComponent },
    { path: 'login', component: LoginComponent },
    { path: 'forgot', component: ForgotComponent },
  ]
 }
  
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FeatureRoutingModule { }
