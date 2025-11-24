import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogContent, MatDialogActions, MatDialogModule } from '@angular/material/dialog';
import { UsuarioService } from '../../../../services/usuario.service';
import { User, CreateUser, UpdateUser } from '../../../../interfaces/user';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-modal-usuario',
  standalone: true,
  templateUrl: './modal-usuario.component.html',
  styleUrls: ['./modal-usuario.component.css'],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatIconModule,
    MatDialogContent,
    MatDialogActions,
    MatDialogModule
]
})
export class ModalUsuarioComponent {

  formularioUsuario: FormGroup;
  ocultarPassword = true;

  tituloAccion = 'Crear';
  botonAccion = 'Guardar';

  roles = [
    { id: 1, nombre: 'Administrador' },
    { id: 2, nombre: 'Empleado' }
  ];

  constructor(
    private dialogRef: MatDialogRef<ModalUsuarioComponent>,
    @Inject(MAT_DIALOG_DATA) public data: User | null,
    private fb: FormBuilder,
    private usuarioService: UsuarioService
  ) {

    this.formularioUsuario = this.fb.group({
      nombreCompleto: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      rolId: ['', Validators.required],
      clave: ['', Validators.required], // ✅ solo en creación
      esActivo: [true, Validators.required]
    });

    if (data) {
      this.tituloAccion = 'Editar';
      this.botonAccion = 'Actualizar';

      this.formularioUsuario.patchValue({
        nombreCompleto: data.nombreCompleto,
        email: data.email,
        rolId: data.rolId,
        esActivo: data.esActivo
      });

      this.formularioUsuario.get('clave')?.clearValidators();
      this.formularioUsuario.get('clave')?.updateValueAndValidity();
    }
  }

  guardarUsuario() {
    if (!this.data) {
      const nuevoUsuario: CreateUser = {
        nombreCompleto: this.formularioUsuario.value.nombreCompleto,
        email: this.formularioUsuario.value.email,
        clave: this.formularioUsuario.value.clave,
        rolId: this.formularioUsuario.value.rolId
      };

      this.usuarioService.createUser(nuevoUsuario).subscribe(() => {
        this.dialogRef.close(true);
      });
    } else {
      const usuarioActualizado: UpdateUser = {
        id: this.data.id,
        nombreCompleto: this.formularioUsuario.value.nombreCompleto,
        email: this.formularioUsuario.value.email,
        rolId: this.formularioUsuario.value.rolId,
        esActivo: this.formularioUsuario.value.esActivo
      };

      this.usuarioService.updateUser(usuarioActualizado).subscribe(() => {
        this.dialogRef.close(true);
      });
    }
  }
}
