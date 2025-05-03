import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface Country {
  id: number,
  name: string
}

@Injectable({
  providedIn: 'root'
})
export class CountryService {
  private apiUrl = 'http://localhost:5011/api/country';

  constructor(private http: HttpClient) {}

  getCountries(): Observable<Country[]> {
    return this.http.get<Country[]>(this.apiUrl);
  }

  getCountry(id: number): Observable<Country> {
    return this.http.get<Country>(`${this.apiUrl}/${id}`);
  }

  createCountry(country: Country): Observable<Country> {
    return this.http.post<Country>(this.apiUrl, country);
  }

  updateCountry(id: number, country: Country): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, country);
  }

  deleteVoyage(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
