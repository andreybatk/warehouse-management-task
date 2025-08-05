import { Component, OnInit } from '@angular/core';
import { Unit } from '../../data/interfaces/unit.interface';
import { UnitService } from '../../data/services/unit.service';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-units',
  imports: [RouterLink, CommonModule],
  templateUrl: './units.html',
  styleUrl: './units.scss'
})

export class Units implements OnInit {
  units: Unit[] = [];

  constructor(private unitService: UnitService) {}

  ngOnInit(): void {
    this.loadUnits();
  }

  loadUnits(state?: 'Active' | 'Archived'): void {
    this.unitService.getAll(state).subscribe({
      next: (data) => (this.units = data),
      error: (err) => console.error('Ошибка загрузки единиц', err)
    });
  }

  showAll() {
    this.loadUnits();
  }

  showActive() {
    this.loadUnits('Active');
  }

  showArchived() {
    this.loadUnits('Archived');
  }
}
