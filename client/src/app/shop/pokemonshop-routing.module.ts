import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PokemonDetailsComponent } from './pokemon-details/pokemon-details.component';
import { PokemonshopComponent } from './pokemonshop/pokemonshop.component';

const routes: Routes = [
  {
    path: '',
    component: PokemonshopComponent
  },
  {
    path: ':name',
    component: PokemonDetailsComponent,
    data: {breadcrumb: {alias: 'pokemonDetailsName'}}
  }
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class PokemonshopRoutingModule { }
