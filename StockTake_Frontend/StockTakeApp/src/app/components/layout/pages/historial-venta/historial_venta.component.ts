import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';

import { VentaService } from '../../../../services/venta.service';
import { Venta } from '../../../../interfaces/venta';
import { MatFormField, MatLabel } from "@angular/material/form-field";
import { MatIcon } from "@angular/material/icon";
import { MatDivider } from "@angular/material/divider";
import { MatCard, MatCardContent } from "@angular/material/card";
import { MatOption } from "@angular/material/select";
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-historial-venta',
  standalone: true,
  templateUrl: './historial-venta.component.html',
  styleUrls: ['./historial-venta.component.css'],
  imports: [MatFormField, MatLabel, MatDatepickerModule, MatIcon, MatDivider,
    MatTableModule, MatCard, MatCardContent, ReactiveFormsModule, MatOption,
    CommonModule, MatFormFieldModule, MatSelectModule, MatInputModule,
    MatButtonModule, MatDatepickerModule, MatNativeDateModule, MatPaginatorModule]
})
export class HistorialVentaComponent implements OnInit {

  formularioBusqueda: FormGroup;

  opcionesBusqueda = [
    { value: 'fecha', descripcion: 'Por fechas' },
    { value: 'numero', descripcion: 'Número de venta' }
  ];

  columnasTabla: string[] = ['fechaRegistro', 'numeroDocumento', 'tipoPago', 'total', 'accion'];
  datosListaVenta = new MatTableDataSource<Venta>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private fb: FormBuilder,
    private ventaService: VentaService,
    private dialog: MatDialog
  ) {
    this.formularioBusqueda = this.fb.group({
      buscarPor: ['fecha', Validators.required],
      numero: [''],
      fechaInicio: [''],
      fechaFin: [''],
    });

    // Reset campos según selección
    this.formularioBusqueda.get('buscarPor')?.valueChanges.subscribe(() => {
      this.formularioBusqueda.patchValue({
        numero: '',
        fechaInicio: '',
        fechaFin: ''
      });
    });
  }

  ngOnInit(): void {}

  ngAfterViewInit(): void {
    this.datosListaVenta.paginator = this.paginator;
  }

  aplicarFiltroTabla(event: Event) {
    const filtro = (event.target as HTMLInputElement).value;
    this.datosListaVenta.filter = filtro.trim().toLowerCase();
  }

  buscarVentas() {
    let fechaInicio = '';
    let fechaFin = '';

    if (this.formularioBusqueda.value.buscarPor === 'fecha') {

      const f1 = new Date(this.formularioBusqueda.value.fechaInicio);
      const f2 = new Date(this.formularioBusqueda.value.fechaFin);

      if (isNaN(f1.getTime()) || isNaN(f2.getTime())) {
        alert("Debe ingresar ambas fechas");
        return;
      }
      const formatDate = (d: Date) =>
        `${d.getDate().toString().padStart(2,'0')}/${
          (d.getMonth()+1).toString().padStart(2,'0')
        }/${d.getFullYear()}`;

      fechaInicio = formatDate(f1);
      fechaFin = formatDate(f2);

      /* fechaInicio = f1.toLocaleDateString('es-AR');
      fechaFin = f2.toLocaleDateString('es-AR');  */
    }

    this.ventaService.historial(
      this.formularioBusqueda.value.buscarPor,
      this.formularioBusqueda.value.numero,
      fechaInicio,
      fechaFin
    ).subscribe({
      next: (res) => {
        if (res.status) {
          this.datosListaVenta = new MatTableDataSource(res.value);
          this.datosListaVenta.paginator = this.paginator;
          console.log("Ventas encontradas:", res.value);
        } else {
          alert("No se encontraron resultados");
        }
    }
    });
  }

  verDetalleVenta(venta: Venta) {
    console.log("Abrir modal detalle venta", venta);
    // falta crear el modal
  }

}
