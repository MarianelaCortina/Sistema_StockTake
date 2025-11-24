import { Component, OnInit } from '@angular/core';
import { AlertaStock } from '../../../../interfaces/stock';
import { StockService } from '../../../../services/stock.service';
import { MatCard } from "@angular/material/card";
import { MatIcon } from "@angular/material/icon";
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-alertas-stock',
  templateUrl: './alertas-stock.component.html',
  styleUrls: ['./alertas-stock.component.css'],
  imports: [MatCard, MatIcon, CommonModule]
})
export class AlertasStockComponent implements OnInit {
  alertas: AlertaStock[] = [];
  cargando = true;

  constructor(private stockService: StockService) {}

  ngOnInit(): void {
    this.stockService.getAlertas().subscribe({
      next: (resp) => {
        this.alertas = resp.value;
        this.cargando = false;
      },
      error: (err) => {
        console.error('Error al obtener alertas', err);
        this.cargando = false;
      }
    });
  }
}
