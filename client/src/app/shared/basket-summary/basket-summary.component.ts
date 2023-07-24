import { Component, EventEmitter, Input, Output } from '@angular/core';
import { BasketItem } from '../models/basket';
import { BasketService } from 'src/app/basket/basket.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-basket-summary',
  templateUrl: './basket-summary.component.html',
  styleUrls: ['./basket-summary.component.scss']
})
export class BasketSummaryComponent {
  @Input() isBasket = true;
  @Output() addItem = new EventEmitter<BasketItem>();
  @Output() removeItem = new EventEmitter<{id: number, quantity: number}>();

  constructor(public basketService: BasketService, private router: Router) {}

  addBasketItem(item: BasketItem) {
    this.addItem.emit(item);
  }

  removeBasketItem(id: number, quantity = 1) {
    this.removeItem.emit({id, quantity});
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
