import { Component } from '@angular/core';
import { ProductComponent } from "./pages/product/product.component";

@Component({
  selector: 'app-home',
  imports: [ProductComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

}
