export interface AuthResponse {
  token: string;
  sesion: {
    idUsuario: number;
    nombreCompleto: string;
    email: string;
    rolNombre: string;
  };
}


export interface Sesion {
  idUsuario: number;
  nombreCompleto: string;
  correo: string;
  rolDescripcion: string;
}