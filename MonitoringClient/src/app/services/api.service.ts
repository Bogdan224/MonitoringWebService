import { HttpClient, HttpXhrBackend, XhrFactory } from '@angular/common/http';
import { Injectable } from '@angular/core';

class BrowserXhr implements XhrFactory {
  build(): XMLHttpRequest { return new XMLHttpRequest(); }
}

@Injectable({ providedIn: 'root' })
export class ApiService {
  private apiUrl = 'https://localhost:7269/api/DeviceActivity'; // Порт бэкенда

  constructor(private http: HttpClient) { 
    this.http = new HttpClient(
      new HttpXhrBackend(new BrowserXhr())
    );
  }

  // Устройства
  getDevices() {
    console.log('Fetching devices from:', this.apiUrl + '/devices');
    return this.http.get<string[]>(`${this.apiUrl}/devices`);
  }

  // Активность устройства
  getDeviceActivities(userName: string) {
    return this.http.get<any[]>(`${this.apiUrl}/devices/${userName}`);
  }

  deleteDeviceActivities(id: string[]) {
  return this.http.delete<{ deletedCount: number }>(
    `${this.apiUrl}`,
    { body: id }
  );
  }

  getAllActivities() {
  return this.http.get<any[]>(`${this.apiUrl}/all`);
  }
}