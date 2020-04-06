import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


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
