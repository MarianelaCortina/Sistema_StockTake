//menu.component.ts
import { Component } from '@angular/core';
import { Menu } from '../../../../interfaces/menu';
import { MenuService } from '../../../../services/menu.service';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [ RouterModule, CommonModule ],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.css'
})
export class MenuComponent {

  menus: Menu[] = [];

  constructor(private menuService: MenuService) {}

  ngOnInit(): void {
    const idUsuario = 1; // ⚡ Luego lo reemplazamos con el id del JWT o sessionStorage
    this.menuService.lista(idUsuario).subscribe({
      next: (res) => {
        if (res.status) {
          this.menus = res.value;
        } else {
          console.warn(res.msg);
        }
      },
      error: (err) => console.error(err)
    });
  }

}
