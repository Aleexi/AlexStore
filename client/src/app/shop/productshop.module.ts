import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { PokemonDetailsComponent } from './pokemon-details/pokemon-details.component';
import { PokemonitemComponent } from './pokemonitem/pokemonitem.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { ProductitemComponent } from './productitem/productitem.component';
import { ProductshopRoutingModule } from './productshop-routing.module';
import { ProductshopComponent } from './productshop/productshop.component';

@NgModule({
  declarations: [
    ProductshopComponent,
    ProductitemComponent,
    ProductDetailsComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    ProductshopRoutingModule,
  ]
})
export class ProductshopModule { }
