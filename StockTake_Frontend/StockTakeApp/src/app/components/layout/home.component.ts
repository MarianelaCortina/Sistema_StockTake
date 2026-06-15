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

  private readonly rutasMenu: Record<string, string> = {
    dashboard: '/pages/dashboard',
    inicio: '/pages/dashboard',
    producto: '/pages/productos',
    productos: '/pages/productos',
    stock: '/pages/stock',
    venta: '/pages/venta',
    ventas: '/pages/venta',
    usuario: '/pages/usuarios',
    usuarios: '/pages/usuarios',
    historialventa: '/pages/historial_venta',
    historialventas: '/pages/historial_venta',
    reporte: '/pages/reportes',
    reportes: '/pages/reportes'
  };

  constructor(
    private _menuService: MenuService,
    private router: Router,
    private _authService: AuthService
  ) {}


  ngOnInit(): void {
  
    const usuario = this._authService.obtenerSesionUsuario();

    if (usuario != null) {
      this.correoUsuario = usuario.correo;
      this.rolUsuario = usuario.rolNombre;
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

  obtenerRutaMenu(menu: Menu): string {
    const rutaGuardada = this.normalizarClave(menu.url);
    const nombreMenu = this.normalizarClave(menu.nombreMenu);

    return this.rutasMenu[rutaGuardada]
      ?? this.rutasMenu[nombreMenu]
      ?? '/pages/dashboard';
  }

  private normalizarClave(valor: string | null | undefined): string {
    if (!valor) {
      return '';
    }

    const segmentos = valor
      .trim()
      .toLowerCase()
      .replace(/^https?:\/\/[^/]+/, '')
      .split('/')
      .filter(Boolean);

    return (segmentos.at(-1) ?? '')
      .normalize('NFD')
      .replace(/[\u0300-\u036f]/g, '')
      .replace(/[^a-z0-9]/g, '');
  }

  cerrarSesion() {
    localStorage.clear();
    this.router.navigate(['/login']);
  }
}
