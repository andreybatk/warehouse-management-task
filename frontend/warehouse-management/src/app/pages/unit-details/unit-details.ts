import { Component, OnInit } from '@angular/core';
import { Unit, UpdateUnitRequest } from '../../data/interfaces/unit.interface';
import { ActivatedRoute, Router } from '@angular/router';
import { UnitService } from '../../data/services/unit.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-unit-details',
  imports: [CommonModule, FormsModule],
  templateUrl: './unit-details.html',
  styleUrl: './unit-details.scss'
})

export class UnitDetails implements OnInit {
  unitId!: string;
  unit?: Unit;
  newName: string = '';

  errorMessage: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private unitService: UnitService
  ) {}

  ngOnInit(): void {
    this.unitId = this.route.snapshot.paramMap.get('id')!;
    this.loadUnit();
  }

  loadUnit(): void {
    this.unitService.getById(this.unitId).subscribe({
      next: (data) => {
        this.unit = data;
        this.newName = data.name;
      },
      error: (err) => console.error('Ошибка загрузки единицы', err)
    });
  }

  update(): void {
    const request: UpdateUnitRequest = { name: this.newName };
    this.unitService.update(this.unitId, request).subscribe({
      next: () => this.loadUnit(),
      error: (err) => console.error('Ошибка обновления', err)
    });
  }

  archive(): void {
    this.unitService.archive(this.unitId).subscribe({
      next: () => this.loadUnit(),
      error: (err) => console.error('Ошибка архивации', err)
    });
  }

  activate(): void {
    this.unitService.activate(this.unitId).subscribe({
      next: () => this.loadUnit(),
      error: (err) => console.error('Ошибка активации', err)
    });
  }

  delete(): void {
    if (confirm('Удалить эту единицу?')) {
      this.unitService.delete(this.unitId).subscribe({
        next: () =>  {
          this.router.navigate(['/units']);
          this.errorMessage = null;
        },
        error: (err) => {
          console.error('Ошибка удаления', err);
          this.errorMessage = err.error?.detail || 'Произошла ошибка';
        }
      });
    }
  }
}