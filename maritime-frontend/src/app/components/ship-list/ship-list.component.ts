import { Component, OnInit } from '@angular/core';
import { Ship, ShipService } from '../../services/ship.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-ship-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './ship-list.component.html',
  styleUrl: './ship-list.component.css'
})
export class ShipListComponent implements OnInit {
  ships: Ship[] = [];

  constructor(private shipService: ShipService) {}

  ngOnInit(): void {
      this.shipService.getShips().subscribe(data => {
        this.ships = data;
      });
  }
}
