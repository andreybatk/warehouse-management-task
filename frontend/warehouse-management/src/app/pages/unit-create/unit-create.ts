import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CreateUnitRequest } from '../../data/interfaces/unit.interface';
import { CommonModule } from '@angular/common';
import { UnitService } from '../../data/services/unit.service';

@Component({
  selector: 'app-unit-create',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './unit-create.html',
  styleUrl: './unit-create.scss'
})
export class UnitCreate {
  errorMessage: string | null = null;

  constructor(
        private router: Router,
        private unitService: UnitService
      ) {}

  form = new FormGroup({
    name: new FormControl('', [Validators.required]),
    state: new FormControl('Active', [Validators.required])
  })

  onSubmit() {
    if(this.form.valid) {
      const request: CreateUnitRequest = { name: this.form.value.name!, state: this.form.value.state ! as 'Active' | 'Archived'};
      this.unitService.create(request).subscribe({
        next: (createdId) => {
          this.router.navigate(['/units', createdId]);
          this.errorMessage = null;
        },
        error: (err) => { 
          this.errorMessage = err.error?.detail || 'Произошла ошибка';
        }
      })
    }
  }
}
