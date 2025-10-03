import { Routes } from '@angular/router';
import { HomeComponent } from './components/layout/home.component';


export const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    children: [
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
        path: 'pages/venta',
        loadComponent: () =>
          import('./components/layout/pages/venta/venta.component').then(m => m.VentaComponent),
      },
      // Faltan usuarios, historial_venta y reportes (cuando los crees, los agregás aquí)
    ],
  },
  // { path: '**', redirectTo: '' }
];