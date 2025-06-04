import { Component, OnInit,AfterViewInit, ViewChild } from '@angular/core';
import { ProductService } from '../../../../services/product.service';
import { Product } from '../../../../interfaces/product';
import { CommonModule } from '@angular/common';
import { material } from '../../../../shared/material';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { ModalProductComponent } from '../../modals/modal-product/modal-product.component';
import Swal from 'sweetalert2';



@Component({
  selector: 'app-product',
  standalone: true,
  imports: [
    CommonModule,
    ...material
  ],
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})

export class ProductComponent implements OnInit, AfterViewInit {

  constructor(
    private productService: ProductService,
    private dialog: MatDialog,
    
  ) { }

  columnasTabla: string[]=['nombre', 'descripcion',  'precio', 'stock', 'estado', 'acciones'];
  dataInicio: Product[]=[];
  dataListaProductos= new MatTableDataSource(this.dataInicio); 
  @ViewChild(MatPaginator) paginacionTabla!: MatPaginator;


  ngOnInit(): void {
    this.getProducts();
  }

  getProducts(){
    this.productService.getAllProducts().subscribe({
      next: (data) => {
        if(data.length > 0){ 
          this.dataListaProductos = new MatTableDataSource(data);
          console.log("Lista de productos:", this.dataListaProductos.data);
        }
        else
          console.log("No se encontraron datos", "Oops!")  
      },
      error:(e)=>{}
    })
  }

  ngAfterViewInit(): void {
    this.dataListaProductos.paginator=this.paginacionTabla;
  }

  applyTableFilter(event: Event){
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataListaProductos.filter = filterValue.trim().toLocaleLowerCase();
  }

  newProduct(){
    this.dialog.open(ModalProductComponent, {
      disableClose:true
    }).afterClosed().subscribe(result => {
      if(result === true)
        this.getProducts();
    });
  }

  editProduct(producto: Product){
    this.dialog.open(ModalProductComponent, {
      disableClose:true,
      data: producto
    }).afterClosed().subscribe(result => {
      if(result === true)
        this.getProducts();
    });
  }

  deleteProduct(producto: Product){
    Swal.fire({
      title: '¿Estás seguro?',
      text: `¿Deseas eliminar el producto ${producto.nombre}?`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sí, eliminar'
    }).then((result) => {
      if (result.isConfirmed) {
        this.productService.deleteProduct(producto.id).subscribe({
          next: (data) => {
            Swal.fire(
              'Eliminado!',
              `El producto ${producto.nombre} ha sido eliminado.`,
              'success'
            );
            this.getProducts();
          },
          error:(e)=>{}
        });
      }
    });
  }

}
