import { Component, OnInit } from '@angular/core';
import { Pokemon } from '../../shared/models/pokemon';
import { ShopService } from '../shop.service';
import { Ability } from 'src/app/shared/models/ability';
import { Type } from 'src/app/shared/models/type';
import { PokemonParams } from 'src/app/shared/models/pokemonParams';

@Component({
  selector: 'app-pokemonshop',
  templateUrl: './pokemonshop.component.html',
  styleUrls: ['./pokemonshop.component.scss']
})
export class PokemonshopComponent implements OnInit{
  pokemons: Pokemon[] = [];
  abilities: Ability[] = [];
  types: Type[] = [];
  pokemonParams = new PokemonParams();
  sortAlternatives = [
    {name: 'Name Ordering', ApiValue: 'name'},
    {name: 'Strength Ascending', ApiValue: 'strengthAsc'},
    {name: 'Strength Descending', ApiValue: 'strengthDesc'}
  ];
  Count : number = 0;

  /* Inject and use our ShopService into our pokemonshop component */
  constructor(private shopService: ShopService) {}

  ngOnInit(): void {
    this.getPokemons();
    this.getPokemonTypes();
    this.getPokemonAbilities();
  }
  getPokemons()
  {
    this.shopService.getPokemons(this.pokemonParams).subscribe({
      next: response => {
        this.pokemons = response.data; 
        this.pokemonParams.pageNumber = response.pageIndex; 
        this.pokemonParams.pageSize = response.pageSize;
        this.Count = response.count;
      },
      error: error => console.log(error)
    });
  }

  getPokemonTypes()
  {
    this.shopService.getPokemonTypes().subscribe({
      next: response => this.types = [{id: 0, name: 'All'}, ...response],
      error: error => console.log(error)
    });
  }

  getPokemonAbilities()
  {
    this.shopService.getPokemonAbilities().subscribe({
      next: response => this.abilities = [{id: 0, name: 'All'}, ...response],
      error: error => console.log(error)
    });
  }

  onTypeSelected(typeId: number)
  {
    if (this.pokemonParams.typeId !== typeId){
      this.pokemonParams.typeId = typeId;
      this.getPokemons();
    }
  }

  onAbilitySelected(abilityId: number)
  {
    if (this.pokemonParams.abilityId !== abilityId) {
      this.pokemonParams.abilityId = abilityId;
      this.getPokemons();
    }
    
  }
  onSortSelected(event: any) {
    if (this.pokemonParams.sort !== event.target.value) {
      this.pokemonParams.sort = event.target.value;
      this.getPokemons();
    }
  }
  onPageChanged(event: any) {
    if (this.pokemonParams.pageNumber !== event.page) {
      this.pokemonParams.pageNumber = event.page;
      this.getPokemons();
    }
  }
}
