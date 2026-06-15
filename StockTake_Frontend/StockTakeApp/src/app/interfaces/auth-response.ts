export interface AuthResponse {
  token: string;
  sesion: Sesion;
}


export interface Sesion {
  idUsuario: number;
  nombreCompleto: string;
  correo: string;
  rolNombre: string;
}
