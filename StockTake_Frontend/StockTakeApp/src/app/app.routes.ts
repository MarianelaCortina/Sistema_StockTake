import { Routes } from '@angular/router';
import { HomeComponent } from './components/layout/home.component';


export const routes: Routes = [

  {
    path: 'login',
    loadComponent: () =>
      import('./components/layout/login/login.component').then(m => m.LoginComponent),
  },
  {
    path: '',
    component: HomeComponent,
    children: [
      {
        path: '',
        redirectTo: 'pages/dashboard',
        pathMatch: 'full',
      },
      {
        path: 'pages/dashboard',
        loadComponent: () =>
          import('./components/layout/pages/dashboard/dashboard.component').then(m => m.DashboardComponent),
      },
      {
        path: 'pages/productos',
        loadComponent: () =>
          import('./components/layout/pages/producto/product.component').then(m => m.ProductoComponent),
      },
      {
        path: 'pages/stock',
        loadComponent: () =>
          import('./components/layout/pages/stock/stock.component').then(m => m.StockComponent),
      },
      {
        path: 'pages/venta',
        loadComponent: () =>
          import('./components/layout/pages/venta/venta.component').then(m => m.VentaComponent),
      },
      {
        path: 'pages/usuarios',
        loadComponent: () =>
          import('./components/layout/pages/usuario/usuario.component').then(m => m.UsuariosComponent),
      },
      {
        path: 'pages/historial_venta',
        loadComponent: () =>
          import('./components/layout/pages/historial-venta/historial_venta.component').then(m => m.HistorialVentaComponent),
      },
      {
        path: 'pages/reportes',
        loadComponent: () =>
          import('./components/layout/pages/reporte/reporte.component').then(m => m.ReporteComponent),
      },
      { path: 'pages/producto', redirectTo: 'pages/productos', pathMatch: 'full' },
      { path: 'pages/usuario', redirectTo: 'pages/usuarios', pathMatch: 'full' },
      { path: 'pages/historial-venta', redirectTo: 'pages/historial_venta', pathMatch: 'full' },
      { path: 'pages/reporte', redirectTo: 'pages/reportes', pathMatch: 'full' },
    ],
  },
  /// fallback a Dashboard si escriben algo inexistente
  { path: '**', redirectTo: 'pages/dashboard' },
];
