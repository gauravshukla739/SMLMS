import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AttendanceComponent } from './features/components/attendance/attendance.component';


const routes: Routes = [
  { path: '', loadChildren: './features/feature.module#FeatureModule' },
  //{
  //  path: 'leaves',
  //  loadChildren: () => import('./features/components/leave-management/leave-management.module').then(m => m.LeaveManagementModule)
  //}
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
