import { Routes } from '@angular/router';
import { MainLayoutComponent } from './layout/main-layout/main-layout.component';
import { HomeComponent } from './pages/home/home.component';
import { ShipListComponent } from './components/ship-list/ship-list.component';
import { VoyageListComponent } from './components/voyage-list/voyage-list.component';
import { PortListComponent } from './components/port-list/port-list.component';
import { CountryListComponent } from './components/country-list/country-list.component';

export const routes: Routes = [

    {
        path: '',
        component: MainLayoutComponent,
        children: [
            {path: '', component: HomeComponent},
            {path: 'ships', component: ShipListComponent},
            {path: 'voyages', component: VoyageListComponent},
            {path: 'ports', component: PortListComponent},
            {path: 'countries', component: CountryListComponent}
        ]
    }

];
