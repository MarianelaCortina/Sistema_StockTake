import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Category } from '../../../../interfaces/category';
import { Product } from '../../../../interfaces/product';
import { CategoryService } from '../../../../services/category.service';
import { ProductService } from '../../../../services/product.service';
import { material } from '../../../../shared/material';
import { CommonModule } from '@angular/common';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';



@Component({
  selector: 'app-modal-product',
  standalone: true,
  imports: [ 
    CommonModule,
    ReactiveFormsModule,
    ...material
  ],
  templateUrl: './modal-product.component.html',
  styleUrl: './modal-product.component.css'
})
export class ModalProductComponent implements OnInit {

  formProduct: FormGroup;
  titleAction:string="Agregar";
  buttonAction:string="Guardar";
  listCategories:Category[]=[];

  constructor(
    private modalActual: MatDialogRef<ModalProductComponent>,
    @Inject(MAT_DIALOG_DATA) public dataProducts:Product,
    private fb: FormBuilder,
    private _categoryService: CategoryService,
    private _productService: ProductService,
    private _snackBar: MatSnackBar
   
    ){
      this.formProduct = this.fb.group({
        nombre:['', Validators.required],
        categoriaId:['', Validators.required],
        stock:['', Validators.required],
        precio:['', Validators.required],
        esActivo:['1', Validators.required]
      });

      if(this.dataProducts != null){
        this.titleAction = "Editar";
        this.buttonAction = "Actualizar";
      }
      this._categoryService.getAllCategories().subscribe({
        next: (data) => {
          if(data)this.listCategories = data
        },
        error:(e)=>{}
      })


    }

  ngOnInit(): void {
    if(this.dataProducts != null){
      this.formProduct.patchValue({
        nombre: this.dataProducts.nombre,
        categoriaId: this.dataProducts.categoriaId,
        stock: this.dataProducts.stock,
        precio: this.dataProducts.precio,
        esActivo: this.dataProducts.esActivo  ? '1' : '0'
      });
    }
  }

    guardarEditarProducto() {
    const producto: Product = {
      id: this.dataProducts?.id ?? 0,
      nombre: this.formProduct.value.nombre,
      descripcion: this.formProduct.value.descripcion,
      categoriaId: this.formProduct.value.categoriaId,
      stock: +this.formProduct.value.stock,
      precio: +this.formProduct.value.precio,
      esActivo: this.formProduct.value.esActivo === '1'
    };

    if (this.dataProducts == null) {
      // Crear nuevo producto
      this._productService.createProduct(producto).subscribe({
        next: (_data) => {
          this._snackBar.open('Producto creado', 'Cerrar', {
          duration: 3000,
          horizontalPosition: 'right',
          verticalPosition: 'top'
        });
          this.modalActual.close(true); 
        },
        error: (e) => {
          console.error("Error al crear el producto", e);
        }
      });
    } else {
      // Editar producto existente
      this._productService.editProduct(producto.id!, producto).subscribe({
        next: () => {
          this._snackBar.open('Producto actualizado', 'Cerrar', {
          duration: 3000,
          horizontalPosition: 'right',
          verticalPosition: 'top'
        });
          this.modalActual.close(true); 
        },
        error: (e) => {
          console.error("Error al editar el producto", e);
        }
      });
    }
  }

   cerrarModal() {
    this.modalActual.close(false); // false = sin cambios
  }

}
