import { Address } from "./user"

export interface OrderToCreate {
    basketId: string;
    delieveryMethodId: number;
    shippingAddress: Address;
}

export interface Order {
    id: number
    userEmail: string
    orderDate: string
    shippingAddress: Address
    delieveryMethod: string
    shippingPrice: number
    orderItems: OrderItem[]
    subTotal: number
    total: number
    orderStatus: string
  }
  
export interface OrderItem {
    id: number
    name: string
    pictureUrl: string
    price: number
    quantity: number
  }
  