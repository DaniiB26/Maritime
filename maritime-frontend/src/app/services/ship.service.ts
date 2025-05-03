import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Ship {
  id: number;
  name: string;
  maxSpeed: number;
}

@Injectable({
  providedIn: 'root'
})
export class ShipService {
  private apiUrl = 'http://localhost:5011/api/ship';


  constructor(private http: HttpClient) { }

  getShips(): Observable<Ship[]> {
    return this.http.get<Ship[]>(this.apiUrl);
  }

  getShip(id: number): Observable<Ship> {
    return this.http.get<Ship>(`${this.apiUrl}/${id}`);
  }

  createShip(ship: Ship): Observable<Ship> {
    return this.http.post<Ship>(this.apiUrl, ship);
  }

  updateShip(id: number, ship: Ship): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, ship);
  }

  deleteShip(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
