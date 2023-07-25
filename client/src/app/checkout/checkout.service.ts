import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { DelieveryMethod } from '../shared/models/delieveryMethod';
import { map } from 'rxjs';
import { Order, OrderToCreate } from '../shared/models/order';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getDelieveryMethods() {
    return this.http.get<DelieveryMethod[]>(this.baseUrl + 'orders/delieveryMethods').pipe(
      map(delieveryMethod => {
        return delieveryMethod.sort((x, y) => x.priceOfDelievery - y.priceOfDelievery)
      })
    )
  }

  createOrder(order: OrderToCreate) {
    return this.http.post<Order>(this.baseUrl + 'orders', order);
  }
}
