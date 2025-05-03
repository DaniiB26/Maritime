import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Country, CountryService } from '../../services/country.service';

@Component({
  selector: 'app-country-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './country-list.component.html',
  styleUrl: './country-list.component.css'
})
export class CountryListComponent implements OnInit{
  countries: Country[] = [];

  constructor(private countryService: CountryService) {}

  ngOnInit(): void {
      this.countryService.getCountries().subscribe(data => {
        this.countries = data;
      });
  }
}
