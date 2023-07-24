import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { DelieveryMethod } from 'src/app/shared/models/delieveryMethod';
import { CheckoutService } from '../checkout.service';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-checkout-delievery',
  templateUrl: './checkout-delievery.component.html',
  styleUrls: ['./checkout-delievery.component.scss']
})
export class CheckoutDelieveryComponent implements OnInit {
  @Input() checkoutForm? : FormGroup;
  delieveryMethods: DelieveryMethod[] = [];

  constructor(private checkoutService: CheckoutService, private basketService: BasketService) {}

  ngOnInit(): void {
    this.checkoutService.getDelieveryMethods().subscribe({
      next: delieveryMethods => this.delieveryMethods = delieveryMethods
    })
  }

  setShippingPrice(delieveryMethod: DelieveryMethod) {
    this.basketService.setShippingPrice(delieveryMethod);
  }
}
