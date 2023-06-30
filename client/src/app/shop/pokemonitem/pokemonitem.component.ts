import { Component, Input } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { Pokemon } from 'src/app/shared/models/pokemon';

@Component({
  selector: 'app-pokemonitem',
  templateUrl: './pokemonitem.component.html',
  styleUrls: ['./pokemonitem.component.scss']
})
export class PokemonitemComponent {
  @Input() pokemon?: Pokemon;

  constructor(private basketService: BasketService) {}

  addPokemonToBasket() {
    if (this.pokemon) this.basketService.addPokemonItemToBasket(this.pokemon);
  }
}
