import { Component } from '@angular/core';
import { BasketService } from './basket.service';
import { BasketItem } from '../shared/models/basket';
import { Router } from '@angular/router';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent {
  constructor(public basketService: BasketService, private router: Router) {}

  incrementQuantity(item: BasketItem) {
    this.basketService.addItemToBasket(item);
  }

  decrementQuantity(item: BasketItem, quantity?: number) {
    this.basketService.removeItemFromBasket(item.id, quantity)
  }

  navigateToRouterLink(item: BasketItem): void {
    /* Check if should be routed to to products or pokemons */
    if (item.abilitie === null) {
      this.router.navigate(['/products', item.id]);
    } 
    else if (item.brand === null) {
      this.router.navigate(['/pokemons', item.itemName]);
    }
  }
}
