export interface ProductDto {
  id: number;
  origin: string;
  name: string;
  quantite: number;
  usage: Usages;
}

export enum Usages {
  Autre = 0,
  Informatique = 1,
  Transport = 2,
  Recherche = 3,
}
