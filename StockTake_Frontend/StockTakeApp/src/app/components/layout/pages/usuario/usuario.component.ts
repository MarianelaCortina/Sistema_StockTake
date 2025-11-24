import { Component, OnInit, ViewChild } from '@angular/core';
import { UsuarioService } from '../../../../services/usuario.service';
import { User } from '../../../../interfaces/user';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import Swal from 'sweetalert2';
import { MatCardModule } from "@angular/material/card";
import { MatGridListModule } from "@angular/material/grid-list";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatIconModule } from "@angular/material/icon";
import { CommonModule } from '@angular/common';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule } from '@angular/forms';
import { MatDividerModule } from '@angular/material/divider';

import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { ModalUsuarioComponent } from '../../modals/modal-usuario/modal-usuario.component';


@Component({
  selector: 'app-usuarios',
  templateUrl: './usuario.component.html',
  styleUrls: ['./usuario.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatGridListModule,
    MatFormFieldModule,
    MatInputModule,
    MatAutocompleteModule,
    MatSelectModule,
    MatButtonModule,
    MatTableModule,
    MatIconModule, 
    MatDividerModule, 
    MatPaginator,
    MatDialogModule
  ]
})
export class UsuariosComponent implements OnInit {

  columnasTabla: string[] = ['nombreCompleto', 'email', 'rolNombre', 'estado', 'acciones'];
  dataListaUsuarios = new MatTableDataSource<User>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private usuarioService: UsuarioService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.cargarUsuarios();
  }

  cargarUsuarios() {
    this.usuarioService.getUsers().subscribe({
      next: (res) => {
        this.dataListaUsuarios.data = res.value;
        this.dataListaUsuarios.paginator = this.paginator;
      }
    });
  }

  aplicarFiltroTabla(event: Event) {
    const input = (event.target as HTMLInputElement).value;
    this.dataListaUsuarios.filter = input.trim().toLowerCase();
  }

  nuevoUsuario() {
    const dialogRef = this.dialog.open(ModalUsuarioComponent, {
    disableClose: true,
    width: '420px'
  });

  dialogRef.afterClosed().subscribe((resultado) => {
    if (resultado) {
      this.cargarUsuarios();
    }
  });
  }

  updateUser(user: User) {
    const dialogRef = this.dialog.open(ModalUsuarioComponent, {
    disableClose: true,
    width: '420px',
    data: user
  });

  dialogRef.afterClosed().subscribe((resultado) => {
    if (resultado) {
      this.cargarUsuarios();
    }
  });
  }

  deleteUser(user: User) {
    Swal.fire({
      icon: 'warning',
      title: '¿Eliminar usuario?',
      text: user.nombreCompleto,
      showCancelButton: true,
      confirmButtonText: 'Eliminar',
      cancelButtonText: 'Cancelar'
    }).then(result => {
      if (result.isConfirmed) {
        this.usuarioService.deleteUser(user.id).subscribe(() => {
          this.cargarUsuarios();
        });
      }
    });
  }
}
