import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket/basket.service';
import { AccountService } from './account/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title: string = 'AlexStore';

  constructor(private basketService: BasketService, private accountService: AccountService) {}

  ngOnInit(): void 
  {
    this.loadBasket();
    this.loadCurrentUser();
  }

  loadBasket()
  {
    let basketId = localStorage.getItem('basketId');
    basketId && this.basketService.getBasketFromApi(basketId);
  }

  loadCurrentUser() 
  {
    const JWToken = localStorage.getItem('JWToken');
    this.accountService.loadCurrentUser(JWToken).subscribe();
  }
}
