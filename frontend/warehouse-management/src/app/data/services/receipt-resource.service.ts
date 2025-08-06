import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../../../environments/environment";
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})

export class ReceiptResourceService {
  private baseUrl = environment.apiUrl;
  private readonly apiUrl = `${this.baseUrl}/api/receiptresource`

  constructor(private http: HttpClient) {}

  create(data: { receiptDocumentId: string; resourceId: string; unitId: string; quantity: number }): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}`, data);
  }

  update(id: string, data: { receiptDocumentId: string; resourceId: string; unitId: string; quantity: number }): Observable<string> {
    return this.http.put<string>(`${this.apiUrl}/${id}`, data);
  }

  delete(id: string): Observable<string> {
    return this.http.delete<string>(`${this.apiUrl}/${id}`);
  }
}
