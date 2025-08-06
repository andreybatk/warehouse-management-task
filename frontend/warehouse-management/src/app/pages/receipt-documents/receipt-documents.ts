import { Component, OnInit } from '@angular/core';
import { ReceiptDocument, ReceiptDocumentFilter } from '../../data/interfaces/receipt-document.interface';
import { ReceiptDocumentService } from '../../data/services/receipt-document.service';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Unit } from '../../data/interfaces/unit.interface';
import { ResourceService } from '../../data/services/resource.service';
import { UnitService } from '../../data/services/unit.service';
import { Resource } from '../../data/interfaces/resource.interface';

@Component({
  selector: 'app-receipt-documents',
  imports: [CommonModule, RouterLink, ReactiveFormsModule],
  templateUrl: './receipt-documents.html',
  styleUrl: './receipt-documents.scss'
})

export class ReceiptDocuments implements OnInit {
  documents: ReceiptDocument[] = [];
  resources: Resource[] = [];
  units: Unit[] = [];
  numbers: number[] = [];
  filterForm!: FormGroup;
  loading = false;
  errorMessage: string | null = null;

  constructor(
    private receiptService: ReceiptDocumentService,
    private resourceService: ResourceService,
    private unitService: UnitService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.filterForm = this.fb.group({
      dateFrom: null,
      dateTo: null,
      documentNumbers: null,
      resourceIds: null,
      unitIds: null
    });

    this.loadDocuments();
    this.loadResources();
    this.loadUnits();
    this.loadNumbers();
  }

  loadResources() {
    this.resourceService.getAll().subscribe({
      next: (res) => (this.resources = res),
      error: (err) => console.error('Ошибка загрузки ресурсов', err)
    });
  }

  loadUnits() {
    this.unitService.getAll().subscribe({
      next: (res) => (this.units = res),
      error: (err) => console.error('Ошибка загрузки единиц', err)
    });
  }

  loadNumbers() {
    this.receiptService.getNumbers().subscribe({
      next: (res) => (this.numbers = res),
      error: (err) => console.error('Ошибка загрузки единиц', err)
    });
  }

  buildFilter(): ReceiptDocumentFilter {
    const f = this.filterForm.value;
    return {
      dateFrom: f.dateFrom || null,
      dateTo: f.dateTo || null,
      documentNumbers: f.documentNumbers == '' ? null : f.documentNumbers,
      resourceIds: f.resourceIds == '' ? null : f.resourceIds,
      unitIds: f.unitIds == '' ? null : f.unitIds,
    };
  }

  applyFilters() {
    this.loadDocuments(this.buildFilter());
  }

  loadDocuments(filter: ReceiptDocumentFilter = {}) {
    if (filter.dateFrom) {
      filter.dateFrom = new Date(filter.dateFrom);
    }

    if (filter.dateTo) {
      filter.dateTo = new Date(filter.dateTo);
    }

    this.loading = true;
    this.receiptService.getAll(filter).subscribe({
      next: (docs) => {
        this.documents = docs;
        this.loading = false;
        this.errorMessage = null;
      },
      error: (err) => {
        this.errorMessage = err.error?.detail || 'Ошибка загрузки документов';
        this.loading = false;
      }
    });
  }
}