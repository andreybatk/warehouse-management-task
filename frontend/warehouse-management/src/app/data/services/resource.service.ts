import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreateResourceRequest, Resource, UpdateResourceRequest } from '../interfaces/resource.interface';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ResourceService {
  private baseUrl = environment.apiUrl;
  private readonly apiUrl = `${this.baseUrl}/api/resources`

  constructor(private http: HttpClient) {}

  getAll(state?: 'Active' | 'Archived'): Observable<Resource[]> {
     let params = new HttpParams();
    if (state) {
      params = params.set('state', state);
    }
    return this.http.get<Resource[]>(this.apiUrl, { params });
  }

  getById(id: string): Observable<Resource> {
    return this.http.get<Resource>(`${this.apiUrl}/${id}`);
  }

  create(resource: CreateResourceRequest): Observable<string> {
    return this.http.post<string>(this.apiUrl, resource);
  }

  update(id: string, request: UpdateResourceRequest): Observable<string> {
    return this.http.put<string>(`${this.apiUrl}/${id}`, request);
  }

  archive(id: string): Observable<string> {
    return this.http.patch<string>(`${this.apiUrl}/${id}/archive`, {});
  }

  activate(id: string): Observable<string> {
    return this.http.patch<string>(`${this.apiUrl}/${id}/activate`, {});
  }

  delete(id: string): Observable<string> {
    return this.http.delete<string>(`${this.apiUrl}/${id}`);
  }
}
