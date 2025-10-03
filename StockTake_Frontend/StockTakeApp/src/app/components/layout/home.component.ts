//home.component.ts
import { Component } from '@angular/core';
import { Menu } from '../../interfaces/menu';
import { MenuService } from '../../services/menu.service';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { DashboardComponent } from "./pages/dashboard/dashboard.component";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    RouterModule,
    CommonModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatSidenavModule,
    MatListModule,
    DashboardComponent
],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
 listaMenu: Menu[] = [];

  constructor(private _menuService: MenuService) {
     console.log("Métodos disponibles:", Object.getOwnPropertyNames(Object.getPrototypeOf(_menuService)));
  }

  ngOnInit(): void {
    // Por ahora usamos un idUsuario fijo (ejemplo: 1)
    const idUsuario = 1;

    this._menuService.lista(idUsuario).subscribe({
      next: (data) => {
        if (data.status) {
          this.listaMenu = data.value;
        } else {
          console.warn("No se encontraron menús:", data.msg);
        }
      },
      error: (e) => {
        console.error("Error cargando menú:", e);
      }
    });
  }
}
