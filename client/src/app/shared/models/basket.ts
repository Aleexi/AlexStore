import * as cuid from "cuid";

export interface Basket {
    id: string
    items: BasketItem[]
  }

export class Basket implements Basket {
    id = cuid();
    items: BasketItem[] = [];
}
  
  
export interface BasketItem {
    id: number
    itemName: string
    itemPrice: number
    quantity: number
    pictureUrl: string
    type: string
    brand?: string
    abilitie?: string
  }

export interface BasketTotals {
  shippingPrice: number
  subtotal: number
  total: number
}