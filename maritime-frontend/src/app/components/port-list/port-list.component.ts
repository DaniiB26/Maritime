import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Port, PortService } from '../../services/port.service';

@Component({
  selector: 'app-port-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './port-list.component.html',
  styleUrl: './port-list.component.css'
})
export class PortListComponent implements OnInit{
  ports: Port[] = [];

  constructor(private portService: PortService) {}

  ngOnInit(): void {
      this.portService.getPorts().subscribe(data => {
        this.ports = data;
      });
  }

}
