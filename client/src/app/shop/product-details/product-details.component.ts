import { Component, OnInit } from '@angular/core';
import { ShopService } from '../shop.service';
import { Product } from 'src/app/shared/models/product';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';
import { BasketService } from 'src/app/basket/basket.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit{
  product?: Product;
  quantity = 1;
  quantityInBasket = 0;

  /* Inject and use our ShopService into our productshop component */
  constructor(private shopService: ShopService, private activatedRoute: ActivatedRoute, 
    private breadcrumbService: BreadcrumbService, private basketService: BasketService) {
    this.breadcrumbService.set('@productDetailsName', ' ');
  }
  
  ngOnInit(): void {
    this.getProduct();
  }

  getProduct() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');

    if (id) {
      this.shopService.getProduct(+id).subscribe({
        next: response => {
          this.product = response; 
          this.breadcrumbService.set('@productDetailsName', this.product.name);
          this.basketService.basketSource$.pipe(take(1)).subscribe({
            next: basket => {
              const item = basket?.items.find(x => x.id === +id);
              /* If item is in the basket already */
              if (item) {
                this.quantity = item.quantity;
                this.quantityInBasket = item.quantity;
              }
            }
          })
        },
        error: error => console.log(error)
      })
    }
  }

  incrementQuantity() {
    this.quantity++;
  }
  
  decrementQuantity() {
    this.quantity--;
  }

  updateBasket() {
    if (this.product) 
    {
      if (this.quantity > this.quantityInBasket) {
        let amountToAdd = this.quantity - this.quantityInBasket;
        this.quantityInBasket += amountToAdd;
        this.basketService.addItemToBasket(this.product, amountToAdd);
      }

      else if (this.quantity < this.quantityInBasket) {
        let amountToRemove = this.quantityInBasket - this.quantity;
        this.quantityInBasket -= amountToRemove;
        this.basketService.removeItemFromBasket(this.product.id, amountToRemove);
      }
    }
  }

  get buttonText() : string {
    return this.quantityInBasket === 0 ? 'add to cart' : 'update cart';
  }

}
