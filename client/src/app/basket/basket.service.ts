import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Basket, BasketItem, BasketTotals } from '../shared/models/basket';
import { HttpClient } from '@angular/common/http';
import { Product } from '../shared/models/product';
import { Pokemon } from '../shared/models/pokemon';
import { DelieveryMethod } from '../shared/models/delieveryMethod';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  baseUrl = environment.apiUrl;
  private basketSource = new BehaviorSubject<Basket | null>(null);
  private basketTotalSource = new BehaviorSubject<BasketTotals | null>(null);
  shipping = 0;

  /* Create an observable that components can use */
  basketSource$ = this.basketSource.asObservable();
  basketTotalSource$ = this.basketTotalSource.asObservable();

  constructor(private http: HttpClient) { }

  setShippingPrice(delieveryMethod: DelieveryMethod) {
    this.shipping = delieveryMethod.priceOfDelievery;
    this.calculateTotals();
  }

  /* Communication between client and API goes here */

  getBasketFromApi(basketId: string) {
    /* Get basket from API */
    return this.http.get<Basket>(this.baseUrl + 'basket?basketId=' + basketId).subscribe({
      next: basket => {this.basketSource.next(basket), this.calculateTotals()}
    })
  }

  setBasketToApi(basket: Basket) {
    /* Send the basket as body of the HTTP Post */
    return this.http.post<Basket>(this.baseUrl + 'basket', basket).subscribe({
      next: basket => {this.basketSource.next(basket), this.calculateTotals()}
    })
  }

  getBasketData(): Basket | null{
    /* Get basket from the client */
    return this.basketSource.value;
  }

  /* Adding Item to the Basket */

  addItemToBasket(item: Product | Pokemon | BasketItem, quantity = 1) {

    /* We know it is a type of product */
    if (this.isProduct(item)) {
      item = this.mapToBasketItem(item); 
    }

    /* We know it is a type of pokemon */
    else if (this.isPokemon(item)) {
      item = this.mapToBasketItem(item); 
    }

    let basket = this.getBasketData() ?? this.createBasket();

    basket.items = this.addOrUpdateItems(basket.items, item, quantity);
    this.setBasketToApi(basket);
  }

  removeItemFromBasket(id: number, quantity = 1){
    let basket = this.getBasketData();
    if (!basket) {
      return;
    }
    let item = basket.items.find(x => x.id === id);
    if (item) {
      item.quantity -= quantity;
      // If quantity of item is 0, remove it 
      if (item.quantity === 0) {
        basket.items = basket.items.filter(x => x.id !== id);
      }
      if (basket.items.length > 0) {
        this.setBasketToApi(basket);
      }
      else {
        this.deleteBasket(basket);
      }
    }
  }

  deleteLocalBasket() {
    this.basketSource.next(null);
    this.basketTotalSource.next(null);
    localStorage.removeItem('basketId');
  }

  private addOrUpdateItems(currentItems: BasketItem[], item: BasketItem, quantity: number): BasketItem[] {
    /* Check if the Basket Item array contains the product that is being added */
    let currentItem = currentItems.find(x => x.id === item.id);

    /* If it is increase quantity */
    if (currentItem) {
      currentItem.quantity += quantity;
    }

    /* If not, add the item and the quantity */
    else {
      item.quantity = quantity;
      currentItems.push(item);
    }
    return currentItems;
  }

  private createBasket(): Basket {
    let basket = new Basket();
    localStorage.setItem('basketId', basket.id);
    return basket;
  }

  private deleteBasket(basket: Basket) {
    return this.http.delete(this.baseUrl + 'basket?basketId=' + basket.id).subscribe({
      next: () => {
        this.deleteLocalBasket();
      }
    })
  }

  private mapToBasketItem(item: Product | Pokemon): BasketItem{
    if (this.isProduct(item)) {
      return {
        id: item.id,
        itemName: item.name,
        itemPrice: item.price,
        quantity: 0,
        pictureUrl: item.pictureURL,
        type: item.productType,
        brand: item.productBrand,
        abilitie: undefined
      }
    }
    else if (this.isPokemon(item)) {
      return {
        id: item.id,
        itemName: item.name,
        itemPrice: item.strength,
        quantity: 0,
        pictureUrl: item.pictureURL,
        type: item.pokemonType,
        brand: undefined,
        abilitie: item.pokemonAbilitie
      }
    }
    throw new Error('Invalid item');
  }

  private calculateTotals() {
    let basket = this.getBasketData();
    /* If there is no basket, return */
    if (!basket) return;
    
    let subtotal = basket.items.reduce((sum, item) => (item.itemPrice * item.quantity) + sum, 0);
    let total = subtotal + this.shipping;
    this.basketTotalSource.next({
      shippingPrice: this.shipping, subtotal, total
    });
  }

  /* Typeguards */
  private isProduct(item: Product | Pokemon | BasketItem): item is Product {
    return (item as Product).productType !== undefined;
  }

  private isPokemon(item: Product | Pokemon | BasketItem): item is Pokemon {
    return (item as Pokemon).pokemonType !== undefined;
  }
}
