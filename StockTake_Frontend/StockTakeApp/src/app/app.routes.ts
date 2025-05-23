import { Routes } from '@angular/router';
import { HomeComponent } from './components/layout/home.component';


export const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: '**', redirectTo: '' }
];
