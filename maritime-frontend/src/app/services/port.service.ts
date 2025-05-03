import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface Port {
  id: number;
  name: string;
  country: { id: number; name: string };
}

@Injectable({
  providedIn: 'root',
})
export class PortService {
  private apiUrl = 'http://localhost:5011/api/port';

  constructor(private http: HttpClient) {}

  getPorts(): Observable<Port[]> {
    return this.http.get<Port[]>(this.apiUrl);
  }

  getPort(id: number): Observable<Port> {
    return this.http.get<Port>(`${this.apiUrl}/${id}`);
  }

  createPort(port: Port): Observable<Port> {
    return this.http.post<Port>(this.apiUrl, port);
  }

  updatePort(id: number, port: Port): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, port);
  }

  deletePort(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
