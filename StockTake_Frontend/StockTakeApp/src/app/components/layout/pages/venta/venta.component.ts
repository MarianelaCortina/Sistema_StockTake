import { Component } from '@angular/core';
import { Product } from '../../../../interfaces/product';
import { DetalleVenta } from '../../../../interfaces/detalle-venta';
import { Venta } from '../../../../interfaces/venta';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { ProductService } from '../../../../services/product.service';
import { VentaService } from '../../../../services/venta.service';
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



@Component({
  selector: 'app-venta',
  standalone: true,
  imports: [ CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatGridListModule,
    MatFormFieldModule,
    MatInputModule,
    MatAutocompleteModule,
    MatSelectModule,
    MatButtonModule,
    MatTableModule,
    MatIconModule],
  templateUrl: './venta.component.html',
  styleUrl: './venta.component.css'
})
export class VentaComponent {

  listaProductos: Product[]=[];
  listaProductosFiltro: Product[]=[];
  listaProductosParaVenta: DetalleVenta[]=[];
  bloquearBotonRegistrar: boolean=false;
  productoSeleccionado!: Product;
  tipoPagoPorDefecto: string='Efectivo';
  totalPagar: number=0;
  formularioProductoVenta: FormGroup;
  columnasTabla: string[]=['producto', 'cantidad', 'precio', 'total', 'accion'];
  datosDetalleVenta= new MatTableDataSource(this.listaProductosParaVenta);


  retornarProductosPorFiltro(busqueda:any): Product[]{
    const valorBuscado = typeof busqueda === "string" ? busqueda.toLocaleLowerCase() : busqueda.nombre.toLocaleLowerCase();

    return this.listaProductos.filter(item => item.nombre.toLocaleLowerCase().includes(valorBuscado));
  }

  constructor(
    private fb: FormBuilder,
    private _productoService: ProductService,
    private _ventasService: VentaService,
    // private _utilidadService: UtilidadService
  ){
    this.formularioProductoVenta = this.fb.group({
      producto: ['', Validators.required],
      cantidad:['', Validators.required]
    });

    this._productoService.getAllProducts().subscribe({
      next: (response: Product[]) => {
        this.listaProductos = response.filter(p => p.esActivo == true && p.stock > 0);
      },
      error:(e)=>{}
    })

    this.formularioProductoVenta.get('producto')?.valueChanges.subscribe(value => {
      this.listaProductosFiltro = this.retornarProductosPorFiltro(value);
    })

  }

  ngOnInit(): void {
    
  }

  mostrarProducto(producto:Product): string {
    return producto.nombre;
  }

  productoParaVenta(event:any){
    this.productoSeleccionado = event.option.value;
  }

  agregarProductoParaVenta(){
    const _cantidad: number = this.formularioProductoVenta.value.cantidad;
    const _precio: number = this.productoSeleccionado.precio;
    const _total: number = _cantidad * _precio;
    this.totalPagar = this.totalPagar + _total;

    this.listaProductosParaVenta.push({
      idProducto: this.productoSeleccionado.id,
      descripcionProducto: this.productoSeleccionado.nombre,
      cantidad: _cantidad,
      precioTexto: String(_precio),
      totalTexto: String(_total)
    })

    //actualizar la tabla
    this.datosDetalleVenta = new MatTableDataSource(this.listaProductosParaVenta);
    this.formularioProductoVenta.patchValue({
      producto:'',
      cantidad:''
    })
  }
  
  eliminarProducto(detalle: DetalleVenta){
    this.totalPagar = this.totalPagar - parseFloat(detalle.totalTexto),
    this.listaProductosParaVenta = this.listaProductosParaVenta.filter(p => p.idProducto != detalle.idProducto);
    this.datosDetalleVenta = new MatTableDataSource(this.listaProductosParaVenta);
  }

  registrarVenta(){
    if(this.listaProductosParaVenta.length > 0){
      this.bloquearBotonRegistrar = true;

      const request: Venta = {
        tipoPago: this.tipoPagoPorDefecto,
        totalTexto: String(this.totalPagar),
        detalleVenta: this.listaProductosParaVenta
      }

      this._ventasService.registrar(request).subscribe({
        next: (response) => {
          if(response.status){
            this.totalPagar = 0.00;
            this.listaProductosParaVenta = [];
            this.datosDetalleVenta = new MatTableDataSource(this.listaProductosParaVenta);

            Swal.fire({
              icon: 'success',
              title: 'Venta Registrada!',
              text: `Numero de venta: ${response.value.numeroDocumento}`
            })
          } else {
              Swal.fire({
              icon: 'error',
              title: 'Oops...',
              text: 'No se pudo registrar la venta'
            });
          }
           this.bloquearBotonRegistrar = false;
        },
          
        error:(e) => {
          this.bloquearBotonRegistrar = false;
          Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Hubo un problema al registrar la venta'
          });
        }
      })
    }
  }

}
