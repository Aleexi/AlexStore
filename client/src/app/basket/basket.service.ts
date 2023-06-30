import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Basket, BasketItem, BasketTotals } from '../shared/models/basket';
import { HttpClient } from '@angular/common/http';
import { Product } from '../shared/models/product';
import { Pokemon } from '../shared/models/pokemon';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  baseUrl = environment.apiUrl;
  private basketSource = new BehaviorSubject<Basket | null>(null);
  private basketTotalSource = new BehaviorSubject<BasketTotals | null>(null);

  /* Create an observable that components can use */
  basketSource$ = this.basketSource.asObservable();
  basketTotalSource$ = this.basketTotalSource.asObservable();

  constructor(private http: HttpClient) { }

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

  addProductItemToBasket(item: Product, quantity = 1) {
    let basketItemToAdd = this.mapProductToBasketItem(item); 

    let basket = this.getBasketData() ?? this.createBasket();
    console.log(this.getBasketData())

    basket.items = this.addOrUpdateItems(basket.items, basketItemToAdd, quantity);
    this.setBasketToApi(basket);
  }

  addPokemonItemToBasket(item: Pokemon, quantity = 1) {
    let basketItemToAdd = this.mapPokemonToBasketItem(item); 

    let basket = this.getBasketData() ?? this.createBasket();

    basket.items = this.addOrUpdateItems(basket.items, basketItemToAdd, quantity);
    this.setBasketToApi(basket);
  }

  private addOrUpdateItems(currentItems: BasketItem[], basketItemToAdd: BasketItem, quantity: number): BasketItem[] {
    /* Check if the Basket Item array contains the product that is being added */
    let item = currentItems.find(x => x.id === basketItemToAdd.id);

    /* If it is increase quantity */
    if (item) {
      item.quantity += quantity;
    }

    /* If not, add the item and the quantity */
    else {
      basketItemToAdd.quantity = quantity;
      currentItems.push(basketItemToAdd);
    }
    return currentItems;
  }

  private createBasket(): Basket {
    let basket = new Basket();
    localStorage.setItem('basketId', basket.id);
    return basket;
  }

  private mapProductToBasketItem(item: Product): BasketItem {
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

  private mapPokemonToBasketItem(item: Pokemon): BasketItem {
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

  private calculateTotals() {
    let basket = this.getBasketData();
    /* If there is no basket, return */
    if (!basket) return;
    
    let shippingPrice = 0;
    let subtotal = basket.items.reduce((sum, item) => (item.itemPrice * item.quantity) + sum, 0);
    let total = subtotal + shippingPrice;
    this.basketTotalSource.next({
      shippingPrice, subtotal, total
    });
  }
}
