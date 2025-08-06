import { Component } from '@angular/core';
import { ResourceService } from '../../data/services/resource.service';
import { CreateResourceRequest } from '../../data/interfaces/resource.interface';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-resource-create',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './resource-create.html',
  styleUrl: './resource-create.scss'
})
export class ResourceCreate {
  errorMessage: string | null = null;

  constructor(
      private router: Router,
      private resourceService: ResourceService
    ) {}

  form = new FormGroup({
    name: new FormControl('', [Validators.required]),
    state: new FormControl('Active', [Validators.required])
  })

  onSubmit() {
    if(this.form.valid) {
      const request: CreateResourceRequest = { name: this.form.value.name!, state: this.form.value.state ! as 'Active' | 'Archived'};
      this.resourceService.create(request).subscribe({
        next: (createdId) => {
          this.router.navigate(['/resources', createdId]);
          this.errorMessage = null;
        },
        error: (err) => { 
          this.errorMessage = err.error?.detail || 'Произошла ошибка';
        }
      })
    }
  }
}
