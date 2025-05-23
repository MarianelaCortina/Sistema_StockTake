import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { ProductService } from '../../../../services/product.service';
import { Product } from '../../../../interfaces/product';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-product',
  imports: [CommonModule, MatPaginator, MatTableModule],
  templateUrl: './product.component.html',
  styleUrl: './product.component.css'
})

export class ProductComponent implements OnInit {

  constructor(
    private productService: ProductService,
    //private dialog: MatDialog,
  ) { }

  columnasTabla: string[]=['nombre', 'descripcion',  'precio', 'stock', 'estado'];
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



}
