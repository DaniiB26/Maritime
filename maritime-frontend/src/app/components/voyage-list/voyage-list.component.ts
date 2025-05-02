import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Voyage, VoyageService } from '../../services/voyage.service';

@Component({
  selector: 'app-voyage-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './voyage-list.component.html',
  styleUrl: './voyage-list.component.css'
})
export class VoyageListComponent implements OnInit {
  voyages: Voyage[] = [];

  constructor(private voyageService: VoyageService) {}

  ngOnInit(): void {
      this.voyageService.getVoyages().subscribe(data => {
        this.voyages = data;
      });
  }
}
