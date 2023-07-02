import { Component, Input } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { Product } from 'src/app/shared/models/product';

@Component({
  selector: 'app-productitem',
  templateUrl: './productitem.component.html',
  styleUrls: ['./productitem.component.scss']
})
export class ProductitemComponent {
  @Input() product?: Product;

  constructor(private basketService: BasketService) {}

  addProductToBasket() {
    if (this.product) this.basketService.addItemToBasket(this.product);
  }
}
