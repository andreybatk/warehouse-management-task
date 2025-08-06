import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Unit } from '../../data/interfaces/unit.interface';
import { ResourceService } from '../../data/services/resource.service';
import { ReceiptDocumentService } from '../../data/services/receipt-document.service';
import { UnitService } from '../../data/services/unit.service';
import { Router } from '@angular/router';
import { CreateReceiptDocumentRequest } from '../../data/interfaces/receipt-document.interface';
import { Resource } from '../../data/interfaces/resource.interface';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-receipt-document-create',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './receipt-document-create.html',
  styleUrl: './receipt-document-create.scss'
})

export class ReceiptDocumentCreate implements OnInit {
  form: FormGroup;
  availableResources: Resource[] = [];
  availableUnits: Unit[] = [];
  errorMessage = '';

  constructor(
    private fb: FormBuilder,
    private service: ReceiptDocumentService,
    private resourceService: ResourceService,
    private unitService: UnitService,
    private router: Router
  ) {
    this.form = this.fb.group({
      number: [null, Validators.required],
      createdAt: [null, Validators.required],
      receiptResources: this.fb.array([])
    });
  }

  ngOnInit() {
    this.resourceService.getAll('Active').subscribe(r => (this.availableResources = r));
    this.unitService.getAll('Active').subscribe(u => (this.availableUnits = u));
    this.addResource();
  }

  get resources(): FormArray {
    return this.form.get('receiptResources') as FormArray;
  }

  addResource() {
    this.resources.push(
      this.fb.group({
        resourceId: [null, Validators.required],
        unitId: [null, Validators.required],
        quantity: [null, [Validators.required, Validators.min(1)]]
      })
    );
  }

  removeResource(index: number) {
    this.resources.removeAt(index);
  }

  onSubmit() {
    if (this.form.invalid) return;

    const request: CreateReceiptDocumentRequest = this.form.value;

    if (request.createdAt) {
      request.createdAt = new Date(request.createdAt);
    }
    
    this.service.create(request).subscribe({
      next: (id) => this.router.navigate(['/receipt-documents', id]),
      error: (err) => {
        this.errorMessage = err.error?.detail || 'Ошибка при создании документа';
      }
    });
  }
}
