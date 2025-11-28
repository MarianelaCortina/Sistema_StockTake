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
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';


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
    
],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
 listaMenu: Menu[] = [];

 correoUsuario: string | null = '';
  rolUsuario: string | null = '';


  constructor(
    private _menuService: MenuService,
    private router: Router,
    private _authService: AuthService
  ) {}


  ngOnInit(): void {
  
    const usuario = this._authService.obtenerSesionUsuario();

    if (usuario != null) {
      this.correoUsuario = usuario.correo;
      this.rolUsuario = usuario.rolDescripcion;
    }

    const idUsuario = usuario?.idUsuario ?? 0;
    
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

  cerrarSesion() {
  localStorage.clear(); // o si guardás token, podés usar removeItem
  this.router.navigate(['/login']);
}
}
