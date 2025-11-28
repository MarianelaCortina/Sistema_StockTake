import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthService } from '../../../services/auth.service';
import { AuthResponse } from '../../../interfaces/auth-response';
import { MatCardModule } from "@angular/material/card";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { CommonModule } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-login',
  standalone: true,
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatProgressSpinnerModule,
    MatButtonModule
  ]
})
export class LoginComponent {

  loading = false;
  formularioLogin: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {

    this.formularioLogin = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      clave: ['', Validators.required]
    });
  }

  iniciarSesion() {
    if (this.formularioLogin.invalid) {
      return;
    }

    this.loading = true;

    this.authService.login(
      this.formularioLogin.value.email,
      this.formularioLogin.value.clave
    ).subscribe({
      next: (res: AuthResponse) => {

        localStorage.setItem('token', res.token);
        localStorage.setItem('usuario', JSON.stringify(res.sesion));

        this.loading = false;
        this.router.navigate(['pages/dashboard']);
      },
      error: () => {
        this.loading = false;
        alert("Credenciales inválidas");
      }
    });

  }
}
