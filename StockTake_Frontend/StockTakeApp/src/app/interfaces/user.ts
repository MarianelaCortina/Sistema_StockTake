export interface User {
  id: number;
  nombreCompleto: string;
  email: string;
  esActivo: boolean;
  emailVerificado: boolean;
  rolId: number;
  rolNombre: string;
}

export interface CreateUser {
  nombreCompleto: string;
  email: string;
  clave: string;
  rolId: number;
  esActivo?: boolean;
}

export interface UpdateUser {
  id: number;
  nombreCompleto: string;
  email: string;
  rolId: number;
  esActivo: boolean;
}
