import { Component, OnInit } from '@angular/core';
import { Pokemon } from 'src/app/shared/models/pokemon';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';
import { BasketService } from 'src/app/basket/basket.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-pokemon-details',
  templateUrl: './pokemon-details.component.html',
  styleUrls: ['./pokemon-details.component.scss']
})
export class PokemonDetailsComponent implements OnInit{
  pokemon?: Pokemon
  quantity = 1;
  quantityInBasket = 0;

  /* Inject and use our ShopService, activatedRoute into our productshop component */
  constructor(private shopService: ShopService, private activatedRoute: ActivatedRoute, 
    private breadcrumbService: BreadcrumbService, private basketService: BasketService) {
    this.breadcrumbService.set('@pokemonDetailsName', ' ');
  }

  ngOnInit(): void {
    this.getPokemon();
  }

  getPokemon() {
    const name = this.activatedRoute.snapshot.paramMap.get('name');

    if (name) {
      this.shopService.getPokemon(name).subscribe({
        next: response => {
          this.pokemon = response; 
          this.breadcrumbService.set('@pokemonDetailsName', this.pokemon.name)
          this.basketService.basketSource$.pipe(take(1)).subscribe({
            next: basket => {
              const item = basket?.items.find(x => x.itemName === name);
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
    if (this.pokemon) 
    {
      if (this.quantity > this.quantityInBasket) {
        let amountToAdd = this.quantity - this.quantityInBasket;
        this.quantityInBasket += amountToAdd;
        this.basketService.addItemToBasket(this.pokemon, amountToAdd);
      }

      else if (this.quantity < this.quantityInBasket) {
        let amountToRemove = this.quantityInBasket - this.quantity;
        this.quantityInBasket -= amountToRemove;
        this.basketService.removeItemFromBasket(this.pokemon.id, amountToRemove);
      }
    }
  }

  get buttonText() : string {
    return this.quantityInBasket === 0 ? 'add to cart' : 'update cart';
  }
}
