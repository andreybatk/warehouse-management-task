import { Component, NgModule, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ResourceService } from '../../data/services/resource.service';
import { Resource, UpdateResourceRequest } from '../../data/interfaces/resource.interface';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-resource-details',
  imports: [CommonModule, FormsModule],
  templateUrl: './resource-details.html',
  styleUrl: './resource-details.scss'
})
export class ResourceDetails implements OnInit {
  resourceId!: string;
  resource?: Resource;
  newName: string = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private resourceService: ResourceService
  ) {}

  ngOnInit(): void {
    this.resourceId = this.route.snapshot.paramMap.get('id')!;
    this.loadResource();
  }

  loadResource(): void {
    this.resourceService.getById(this.resourceId).subscribe({
      next: (data) => {
        this.resource = data;
        this.newName = data.name;
      },
      error: (err) => console.error('Ошибка загрузки ресурса', err)
    });
  }

  update(): void {
    const request: UpdateResourceRequest = { name: this.newName };
    this.resourceService.update(this.resourceId, request).subscribe({
      next: () => this.loadResource(),
      error: (err) => console.error('Ошибка обновления', err)
    });
  }

  archive(): void {
    this.resourceService.archive(this.resourceId).subscribe({
      next: () => this.loadResource(),
      error: (err) => console.error('Ошибка архивации', err)
    });
  }

  activate(): void {
    this.resourceService.activate(this.resourceId).subscribe({
      next: () => this.loadResource(),
      error: (err) => console.error('Ошибка активации', err)
    });
  }

  delete(): void {
    if (confirm('Удалить этот ресурс?')) {
      this.resourceService.delete(this.resourceId).subscribe({
        next: () => this.router.navigate(['/resources']),
        error: (err) => console.error('Ошибка удаления', err)
      });
    }
  }
}