import { CommonModule } from '@angular/common';
import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { Ship, ShipService } from '../../services/ship.service';
import Chart from 'chart.js/auto';

@Component({
  selector: 'app-ship-speed-chart',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './ship-speed-chart.component.html',
  styleUrls: ['./ship-speed-chart.component.css']
})
export class ShipSpeedChartComponent implements OnInit {

  @ViewChild('myChartCanvas') chartRef!: ElementRef<HTMLCanvasElement>;
  chart!: Chart;

  ships: Ship[] = [];

  constructor(private shipService: ShipService) {}

  ngOnInit(): void {
    this.shipService.getShips().subscribe(data => {
      this.ships = data;
      this.createChart();
    });
  }

  createChart() {
    const labels = this.ships.map(s => s.name);
    const data = this.ships.map(s => s.maxSpeed);

    this.chart = new Chart(this.chartRef.nativeElement, {
      type: 'bar',
      data: {
        labels: labels,
        datasets: [{
          label: 'Max Speed (knots)',
          data: data,
          backgroundColor: '#070049',
          borderColor: '#ffffff',
          borderWidth: 1
        }]
      },
      options: {
        aspectRatio: 2,
        scales: {
          y: {
            beginAtZero: true
          }
        }
      }
    });
  }
}
