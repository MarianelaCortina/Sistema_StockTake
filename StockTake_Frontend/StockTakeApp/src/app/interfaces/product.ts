export interface Product {
    id: number,
    nombre: string,
    descripcion: string,
    precio: number,
    stock: number,
    categoriaId: number,
    esActivo: boolean
}