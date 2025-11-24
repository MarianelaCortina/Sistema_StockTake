export interface Stock {
  idProducto: number;
  nombre: string;
  stockActual: number;
}

export interface MovimientoStock {
  idMovimiento: number;
  fecha: string;
  tipo: string;
  cantidad: number;
  stockFinal: number;
  producto: string;
}

export interface AlertaStock {
  id: number;
  nombre: string;
  stock: number;
  promedioVentasDiario: number;
  diasEstimadosRestantes: number;
  alerta: string;
  nivelRiesgo: string;
  color: string;
  icono: string;
  estado: string;
  porcentajeUrgencia: number;
  stockMinimoRecomendado: number;
}
