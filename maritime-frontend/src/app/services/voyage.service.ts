import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface Voyage {
  id: number;
  voyageDate: string;
  voyageStart: string;
  voyageEnd: string;
  departurePort: { id: number; name: string; countryId: number };
  arrivalPort: { id: number; name: string; countryId: number };
  ship: { id: number; name: string; maxSpeed: number };
}


@Injectable({
  providedIn: 'root'
})
export class VoyageService {
  private apiUrl = 'http://localhost:5011/api/voyage';

  constructor(private http: HttpClient) {}

  getVoyages(): Observable<Voyage[]> {
    return this.http.get<Voyage[]>(this.apiUrl);
  }

  getVoyage(id: number): Observable<Voyage> {
    return this.http.get<Voyage>(`${this.apiUrl}/${id}`);
  }

  createVoyage(voyage: Voyage): Observable<Voyage> {
    return this.http.post<Voyage>(this.apiUrl, voyage);
  }

  updateVoyage(id: number, voyage: Voyage): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, voyage);
  }

  deleteVoyage(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
