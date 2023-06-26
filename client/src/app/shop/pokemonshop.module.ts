import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { PokemonDetailsComponent } from './pokemon-details/pokemon-details.component';
import { PokemonitemComponent } from './pokemonitem/pokemonitem.component';
import { PokemonshopRoutingModule } from './pokemonshop-routing.module';
import { PokemonshopComponent } from './pokemonshop/pokemonshop.component';


@NgModule({
  declarations: [
    PokemonshopComponent,
    PokemonitemComponent,
    PokemonDetailsComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    PokemonshopRoutingModule
  ]
})
export class PokemonshopModule { }
