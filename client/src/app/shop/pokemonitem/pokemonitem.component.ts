import { Component, Input } from '@angular/core';
import { Pokemon } from 'src/app/shared/models/pokemon';

@Component({
  selector: 'app-pokemonitem',
  templateUrl: './pokemonitem.component.html',
  styleUrls: ['./pokemonitem.component.scss']
})
export class PokemonitemComponent {
  @Input() pokemon?: Pokemon;
}
