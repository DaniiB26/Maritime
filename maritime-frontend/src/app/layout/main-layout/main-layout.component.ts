import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-main-layout',
  standalone: true,
  imports: [RouterOutlet],
  template: `
    <header>
      <div class="container">
        <nav class="navbar">
          <ul class="nav--list">
            <li class="item"><a href="/ships">Ships</a></li>
            <li class="item"><a href="/voyages">Voyages</a></li>
            <li class="item"><a href="/ports">Ports</a></li>
            <li class="item"><a href="/countries">Countries</a></li>
          </ul>
        </nav>
      </div>
    </header>

    <main>
      <router-outlet></router-outlet>
    </main>
  `,
  styleUrl: './main-layout.component.css',
})
export class MainLayoutComponent {}
