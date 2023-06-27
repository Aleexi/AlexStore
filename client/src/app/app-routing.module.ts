import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { TestsErrorComponent } from './core/tests-error/tests-error.component';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    data: {breadcrumb: 'Home'}
  },
  {
    path: 'errors',
    component: TestsErrorComponent
  },
  {
    path: 'not-found',
    component: NotFoundComponent
  },
  {
    path: 'server-error',
    component: ServerErrorComponent
  },
  {
    path: 'products',
    loadChildren: () => import('./shop/productshop.module').then(x => x.ProductshopModule) 
  },
  {
    path: 'pokemons',
    loadChildren: () => import('./shop/pokemonshop.module').then(x => x.PokemonshopModule) 
  },
  {
    path: '**',
    redirectTo: '',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
