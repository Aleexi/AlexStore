import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductshopComponent } from './productshop/productshop.component';
import { PokemonshopComponent } from './pokemonshop/pokemonshop.component';
import { ProductitemComponent } from './productitem/productitem.component';
import { PokemonitemComponent } from './pokemonitem/pokemonitem.component';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    ProductshopComponent,
    PokemonshopComponent,
    ProductitemComponent,
    PokemonitemComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports: [
    ProductshopComponent,
    PokemonshopComponent
  ]
})
export class ShopModule { }
