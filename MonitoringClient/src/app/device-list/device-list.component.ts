import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-device-list',
  templateUrl: './device-list.component.html'
})
export class DeviceListComponent implements OnInit {
  devices: string[] = [];
  error: string | null = null;

  constructor(private api: ApiService) { }

  ngOnInit() {
    this.api.getDevices().subscribe({
      next: (data) => {
        console.log('Received devices:', data);
        this.devices = data;
      },
      error: (err) => {
        console.error('Error fetching devices:', err);
        this.error = 'Failed to load devices';
      }
    });
  }
}