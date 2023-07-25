import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { BasketService } from 'src/app/basket/basket.service';
import { CheckoutService } from '../checkout.service';
import { ToastrService } from 'ngx-toastr';
import { Basket } from 'src/app/shared/models/basket';
import { Address } from 'src/app/shared/models/user';
import { NavigationExtras, Router } from '@angular/router';

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss']
})
export class CheckoutPaymentComponent {
  @Input() checkoutForm?: FormGroup;

  constructor(private basketService: BasketService, private checkoutService: CheckoutService
    ,private toaster: ToastrService, private router: Router) {}

  submitOrder() {
    let basket = this.basketService.getBasketData();

    if (!basket) {
      return;
    }

    let orderToCreate = this.getOrderToCreate(basket);

    if (!orderToCreate) {
      return;
    }
    this.checkoutService.createOrder(orderToCreate).subscribe({
      next: order => {
        this.toaster.success("Order Created");
        this.basketService.deleteLocalBasket();
        let navExtras: NavigationExtras = {state: order};
        this.router.navigate(['checkout/success'], navExtras);
      }
    })
  }

  getOrderToCreate(basket: Basket) {
    let delieveryMethodId = this.checkoutForm?.get('delieveryForm')?.get('delieveryMethod')?.value;
    let shippingAddress = this.checkoutForm?.get('addressForm')?.value as Address;

    if (!delieveryMethodId && !shippingAddress) {
      return;
    }

    return {basketId: basket.id, delieveryMethodId: delieveryMethodId, shippingAddress: shippingAddress}
  }
}
