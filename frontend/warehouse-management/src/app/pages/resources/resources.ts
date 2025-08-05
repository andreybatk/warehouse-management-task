import { Component, OnInit } from '@angular/core';
import { Resource } from '../../data/interfaces/resource.interface';
import { ResourceService } from '../../data/services/resource.service';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-resources',
  imports: [CommonModule, RouterLink],
  templateUrl: './resources.html',
  styleUrl: './resources.scss'
})

export class Resources implements OnInit {
  resources: Resource[] = [];

  constructor(private resourceService: ResourceService) {}

  ngOnInit(): void {
    this.loadResources();
  }

  loadResources(state?: 'Active' | 'Archived'): void {
    this.resourceService.getAll(state).subscribe({
      next: (data) => (this.resources = data),
      error: (err) => console.error('Ошибка загрузки ресурсов', err)
    });
  }

  showAll() {
    this.loadResources();
  }

  showActive() {
    this.loadResources('Active');
  }

  showArchived() {
    this.loadResources('Archived');
  }
}