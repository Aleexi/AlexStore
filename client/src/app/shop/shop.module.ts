import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductshopComponent } from './productshop/productshop.component';
import { PokemonshopComponent } from './pokemonshop/pokemonshop.component';
import { ProductitemComponent } from './productitem/productitem.component';
import { PokemonitemComponent } from './pokemonitem/pokemonitem.component';
import { SharedModule } from '../shared/shared.module';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { PokemonDetailsComponent } from './pokemon-details/pokemon-details.component';
import { ProductshopRoutingModule } from './productshop-routing.module';
import { PokemonshopRoutingModule } from './pokemonshop-routing.module';

@NgModule({
  declarations: [
    ProductshopComponent,
    PokemonshopComponent,
    ProductitemComponent,
    PokemonitemComponent,
    ProductDetailsComponent,
    PokemonDetailsComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    ProductshopRoutingModule,
    PokemonshopRoutingModule
  ]
})
export class ShopModule { }
