import { Component, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';

import { CommonModule } from '@angular/common';

import { Reporte } from '../../../../interfaces/reporte';
import { VentaService } from '../../../../services/venta.service';


@Component({
  selector: 'app-reporte',
  standalone: true,
  templateUrl: './reporte.component.html',
  styleUrls: ['./reporte.component.css'],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatTableModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatPaginatorModule
]
})
export class ReporteComponent {

  formularioFiltro: FormGroup;
  columnasTabla = ['fechaRegistro', 'numeroDocumento', 'tipoPago', 'producto', 'cantidad', 'precio', 'total'];
  dataVentaReporte = new MatTableDataSource<Reporte>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private fb: FormBuilder,
    private ventaService: VentaService
  ) {
    this.formularioFiltro = this.fb.group({
      fechaInicio: ['', Validators.required],
      fechaFin: ['', Validators.required],
    });
  }

  ngAfterViewInit(): void {
    this.dataVentaReporte.paginator = this.paginator;
  }

  buscarVentas() {
    const f1 = this.formularioFiltro.value.fechaInicio;
    const f2 = this.formularioFiltro.value.fechaFin;

    if (!f1 || !f2) return;

    const format = (d: Date) =>
      `${d.getDate().toString().padStart(2,'0')}/${
        (d.getMonth()+1).toString().padStart(2,'0')
      }/${d.getFullYear()}`;

    const fechaInicio = format(f1);
    const fechaFin = format(f2);

    this.ventaService.reporte(fechaInicio, fechaFin).subscribe({
      next: res => {
        if (res.status) {
          this.dataVentaReporte.data = res.value;
        } else {
          this.dataVentaReporte.data = [];
        }
      }
    });
  }

  exportarExcel() {
    import('xlsx').then(XLSX => {
      const wb = XLSX.utils.book_new();
      const ws = XLSX.utils.json_to_sheet(this.dataVentaReporte.data);
      XLSX.utils.book_append_sheet(wb, ws, 'Reporte');
      XLSX.writeFile(wb, 'ReporteVentas.xlsx');
    });
  }

}
