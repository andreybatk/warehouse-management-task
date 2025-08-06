import { Component, OnInit } from '@angular/core';
import { ReceiptDocument, UpdateReceiptDocumentRequest } from '../../data/interfaces/receipt-document.interface';
import { ActivatedRoute, Router } from '@angular/router';
import { ReceiptDocumentService } from '../../data/services/receipt-document.service';
import { CommonModule } from '@angular/common';
import { FormArray, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Unit } from '../../data/interfaces/unit.interface';
import { Resource } from '../../data/interfaces/resource.interface';
import { ResourceService } from '../../data/services/resource.service';
import { UnitService } from '../../data/services/unit.service';

@Component({
  selector: 'app-receipt-document-details',
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './receipt-document-details.html',
  styleUrl: './receipt-document-details.scss'
})

export class ReceiptDocumentDetails implements OnInit {
  form: FormGroup;
  availableResources: Resource[] = [];
  availableUnits: Unit[] = [];
  errorMessage = '';
  private documentId = '';

  constructor(
    private fb: FormBuilder,
    private service: ReceiptDocumentService,
    private resourceService: ResourceService,
    private unitService: UnitService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.form = this.fb.group({
      number: [null, Validators.required],
      createdAt: [null, Validators.required],
      receiptResources: this.fb.array([])
    });
  }

  ngOnInit() {
    this.documentId = this.route.snapshot.paramMap.get('id') || '';

    this.resourceService.getAll('Active').subscribe(r => (this.availableResources = r));
    this.unitService.getAll('Active').subscribe(u => (this.availableUnits = u));

    this.service.getById(this.documentId).subscribe({
      next: (doc) => this.fillForm(doc),
      error: () => (this.errorMessage = 'Не удалось загрузить документ')
    });
  }

  get resources(): FormArray {
    return this.form.get('receiptResources') as FormArray;
  }

  addResource(data?: any) {
    this.resources.push(
      this.fb.group({
        resourceId: [data?.resourceId || null, Validators.required],
        unitId: [data?.unitId || null, Validators.required],
        quantity: [data?.quantity || null, [Validators.required, Validators.min(1)]]
      })
    );
  }

  removeResource(index: number) {
    this.resources.removeAt(index);
  }

  private fillForm(doc: ReceiptDocument) {
    this.form.patchValue({
      number: doc.number,
      createdAt: doc.createdAt?.toString().substring(0, 10)
    });

    this.resources.clear();
    doc.receiptResources.forEach(r => this.addResource(r));
  }

  onSubmit() {
    if (this.form.invalid) return;

    const request: UpdateReceiptDocumentRequest = this.form.value;

    if (request.createdAt) {
      request.createdAt = new Date(request.createdAt);
    }

    this.service.update(this.documentId, request).subscribe({
      next: () => this.router.navigate(['/receipt-documents', this.documentId]),
      error: (err) => {
        this.errorMessage = err.error?.detail || 'Ошибка при обновлении документа';
      }
    });
  }
}