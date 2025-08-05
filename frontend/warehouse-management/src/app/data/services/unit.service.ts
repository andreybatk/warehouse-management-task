import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreateUnitRequest, Unit, UpdateUnitRequest } from '../interfaces/unit.interface';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class UnitService {
  private baseUrl = environment.apiUrl;
  private readonly apiUrl = `${this.baseUrl}/api/units`

  constructor(private http: HttpClient) {}

  getAll(state?: 'Active' | 'Archived'): Observable<Unit[]> {
    let params = new HttpParams();
    if (state) {
      params = params.set('state', state);
    }
    return this.http.get<Unit[]>(this.apiUrl, { params });
  }

  getById(id: string): Observable<Unit> {
    return this.http.get<Unit>(`${this.apiUrl}/${id}`);
  }

  create(request: CreateUnitRequest): Observable<string> {
    return this.http.post<string>(this.apiUrl, request);
  }

  update(id: string, request: UpdateUnitRequest): Observable<string> {
    return this.http.put<string>(`${this.apiUrl}/${id}`, request);
  }

  delete(id: string): Observable<string> {
    return this.http.delete<string>(`${this.apiUrl}/${id}`);
  }

  archive(id: string): Observable<string> {
    return this.http.patch<string>(`${this.apiUrl}/${id}/archive`, {});
  }

  activate(id: string): Observable<string> {
    return this.http.patch<string>(`${this.apiUrl}/${id}/activate`, {});
  }
}