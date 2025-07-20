import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { DeviceListComponent } from './device-list/device-list.component';
import { ActivityListComponent } from './activity-list/activity-list.component';

@NgModule({
  declarations: [
    AppComponent,
    DeviceListComponent,
    ActivityListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }