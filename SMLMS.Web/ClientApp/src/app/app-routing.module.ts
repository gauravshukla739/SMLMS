import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AttendanceComponent } from './features/components/attendance/attendance.component';


const routes: Routes = [
  { path: '', loadChildren: './features/feature.module#FeatureModule' },
  { path: 'attendance', loadChildren: () => import('./features/components/attendance/attendance.component').then(m => m.AttendanceComponent)}
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
