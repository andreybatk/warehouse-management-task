import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreateReceiptDocumentRequest, ReceiptDocument, ReceiptDocumentFilter, UpdateReceiptDocumentRequest } from '../interfaces/receipt-document.interface';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class ReceiptDocumentService {
  private baseUrl = environment.apiUrl;
  private readonly apiUrl = `${this.baseUrl}/api/receiptdocuments`

  constructor(private http: HttpClient) {}

  getAll(filter: ReceiptDocumentFilter): Observable<ReceiptDocument[]> {
    return this.http.post<ReceiptDocument[]>(`${this.apiUrl}/search`, filter);
    }
  
  getNumbers(): Observable<number[]> {
    return this.http.get<number[]>(`${this.apiUrl}/numbers`);
  }

  getById(id: string): Observable<ReceiptDocument> {
    return this.http.get<ReceiptDocument>(`${this.apiUrl}/${id}`);
  }

  create(request: CreateReceiptDocumentRequest): Observable<string> {
    return this.http.post<string>(this.apiUrl, request);
  }

  update(id: string, request: UpdateReceiptDocumentRequest): Observable<string> {
    return this.http.put<string>(`${this.apiUrl}/${id}`, request);
  }

  delete(id: string): Observable<string> {
    return this.http.delete<string>(`${this.apiUrl}/${id}`);
  }
}
