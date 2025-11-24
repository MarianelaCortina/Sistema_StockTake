import { Component, OnInit } from '@angular/core';
import { StockService } from '../../../../services/stock.service';
import { Stock, MovimientoStock, AlertaStock } from '../../../../interfaces/stock';
import { Response } from '../../../../interfaces/response';
import { CommonModule, DatePipe } from '@angular/common';
import { MatCard } from "@angular/material/card";
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { NgApexchartsModule } from 'ng-apexcharts';
import {
  ApexAxisChartSeries,
  ApexChart,
  ApexXAxis,
  ApexTitleSubtitle,
  ApexDataLabels,
  ApexStroke,
  ApexLegend,
  ApexTooltip,
  ApexGrid
} from 'ng-apexcharts';

export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  xaxis: ApexXAxis;
  title: ApexTitleSubtitle;
  dataLabels: ApexDataLabels;
  stroke: ApexStroke;
  legend: ApexLegend;
  tooltip: ApexTooltip;
  grid: ApexGrid;
};

@Component({
  selector: 'app-stock',
  templateUrl: './stock.component.html',
  styleUrls: ['./stock.component.css'],
  imports: [CommonModule, MatCard, MatIconModule, MatProgressBarModule, DatePipe, NgApexchartsModule]
})
export class StockComponent implements OnInit {

  productos: Stock[] = [];
  movimientos: MovimientoStock[] = [];
  alertas: AlertaStock[] = [];

  chartOptions!: ChartOptions;

  loadingProductos = true;
  loadingMovimientos = true;
  loadingAlertas = true;

  constructor(private stockService: StockService) {}

  ngOnInit(): void {
    this.obtenerProductos();
    this.obtenerMovimientos();
    this.obtenerAlertas();
  }

  obtenerProductos() {
    this.stockService.getProductos().subscribe({
      next: (res: Response<Stock[]>) => {
        this.productos = res.value;
        this.loadingProductos = false;
      },
      error: () => {
        this.loadingProductos = false;
      }
    });
  }

  obtenerMovimientos() {
    this.stockService.getMovimientos().subscribe({
      next: (res: Response<MovimientoStock[]>) => {
        this.movimientos = res.value;
        this.generarGrafico();
        this.loadingMovimientos = false;
      },
      error: () => {
        this.loadingMovimientos = false;
      }
    });
  }

  obtenerAlertas() {
    this.stockService.getAlertas().subscribe({
      next: (res: Response<AlertaStock[]>) => {
        this.alertas = res.value
          // Ordenamos por más urgentes primero
          .sort((a, b) => b.porcentajeUrgencia - a.porcentajeUrgencia);
        this.loadingAlertas = false;
      },
      error: () => {
        this.loadingAlertas = false;
      }
    });
  }

  // ✅ Helpers opcionales para UI
  getColor(riesgo: string) {
    switch (riesgo) {
      case "critico": return "#ff2d2d";
      case "medio": return "#ff9800";
      case "bajo": return "#ffc107";
      default: return "#4caf50";
    }
  }

  getIcon(riesgo: string) {
    switch (riesgo) {
      case "critico": return "priority_high";
      case "medio": return "warning";
      case "bajo": return "info";
      default: return "check_circle";
    }
  }

  // ✅ Gráfico combinado: Ventas vs Stock Actual
  generarGrafico() {
    // Agrupar ventas por producto
    const resumenVentas = this.movimientos.reduce((acc, mov) => {
      if (mov.tipo === 'Venta') {
        acc[mov.producto] = (acc[mov.producto] || 0) + mov.cantidad;
      }
      return acc;
    }, {} as Record<string, number>);

    // Obtener stock actual de los productos
    const nombresProductos = Object.keys(resumenVentas);
    const stockActual = nombresProductos.map(nombre => {
      const producto = this.productos.find(p => p.nombre === nombre);
      return producto ? producto.stockActual : 0;
    });

    this.chartOptions = {
      series: [
        {
          name: 'Ventas',
          data: Object.values(resumenVentas)
        },
        {
          name: 'Stock Actual',
          data: stockActual
        }
      ],
      chart: {
        type: 'bar',
        height: 360,
        toolbar: { show: false },
        foreColor: '#e0e0e0',
        animations: {
          enabled: true,
          easing: 'easeinout',
          speed: 800
        }
      },
      xaxis: {
        categories: nombresProductos,
        labels: { style: { colors: '#bdbdbd', fontSize: '13px' } }
      },
      dataLabels: { enabled: false },
      //stroke: { show: false },
      stroke: { show: true, width: 2, colors: ['#3007ffff', '#25d62bff'] },
      legend: {
        position: 'top',
        labels: { colors: '#ddd' },
        markers: { radius: 6 }
      },
      title: {
        text: '',
        style: { color: '#ffffff', fontSize: '18px' }
      },
      tooltip: {
        theme: 'dark',
        y: {
          formatter: val => `${val} unidades`
        }
      },
      grid: {
        borderColor: '#444',
        row: { colors: ['rgba(255,255,255,0.03)', 'transparent'] }
      }
    };
  }
}
