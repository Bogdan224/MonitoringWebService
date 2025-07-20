import { RouterModule, Routes } from '@angular/router';
import { DeviceListComponent } from './device-list/device-list.component';
import { NgModule } from '@angular/core';
import { ActivityListComponent } from './activity-list/activity-list.component';

const routes: Routes = [
  { path: '', component: DeviceListComponent },
  { path: 'device/:userName', component: ActivityListComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }