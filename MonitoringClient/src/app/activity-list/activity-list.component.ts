import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-activity-list',
  templateUrl: './activity-list.component.html'
})

export class ActivityListComponent implements OnInit {
  userName: string = '';
  activities: any[] = [];
  isLoading = true;
  error: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private api: ApiService,
    private router: Router
  ) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.userName = params['userName'];
      this.loadActivities();
    });
  }

  loadActivities() {
    this.isLoading = true;
    this.error = null;
    
    this.api.getDeviceActivities(this.userName).subscribe({
      next: (data) => {
        this.activities = data;
        this.isLoading = false;
      },
      error: (err) => {
        this.error = 'Ошибка загрузки данных';
        this.isLoading = false;
        console.error('Ошибка загрузки активностей:', err);
      }
    });
  }

  deleteActivity(id: string){
     if (!confirm('Вы уверены, что хотите удалить эту запись?')) {
      return;
    }

    this.api.deleteDeviceActivities([id]).subscribe({
      next: () => {
        // Удаляем запись из локального списка
        this.activities = this.activities.filter(a => a.id !== id);
        
        // Если записей не осталось - возвращаемся к списку устройств
        if (this.activities.length === 0) {
          this.router.navigate(['/']);
        }
      },
      error: (err) => {
        console.error('Ошибка удаления:', err);
        alert('Не удалось удалить запись');
      }
    });
  }
}