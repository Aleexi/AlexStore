import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Pagination } from '../shared/models/pagination';
import { Product } from '../shared/models/product';
import { Pokemon } from '../shared/models/pokemon';
import { Brand } from '../shared/models/brand';
import { Ability } from '../shared/models/ability';
import { Type } from '../shared/models/type';
import { PokemonParams } from '../shared/models/pokemonParams';
import { ProductParams } from '../shared/models/productParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/';

  /* Inject Http Client into our ShopService */
  constructor(private http: HttpClient) { }

  /* Returns an observable that can be subscribed to in the component */
  getProducts(productParams: ProductParams) 
  {
    let params = new HttpParams();

    if (productParams.brandId && productParams.brandId !== 0){
      params = params.append('brandId', productParams.brandId);
    }

    if (productParams.typeId && productParams.typeId !== 0){
      params = params.append('typeId', productParams.typeId);
    }
    
    /* In API sort is already sorted by name, therefore don't apply parameter if name order is requested */
    if (productParams.sort && productParams.sort !== 'name'){
      params = params.append('sort', productParams.sort);
    }

    params = params.append('pageIndex', productParams.pageNumber);
    params = params.append('pageSize', productParams.pageSize);

    if (productParams.search !== '') {
      params = params.append('search', productParams.search);
    }
  
    return this.http.get<Pagination<Product[]>>(this.baseUrl + 'products', { params: params });
  }

  getProductTypes()
  {
    return this.http.get<Type[]>(this.baseUrl + 'products/types');
  }

  getProductBrands()
  {
    return this.http.get<Brand[]>(this.baseUrl + 'products/brands');
  }

  getPokemons(pokemonParams: PokemonParams)   
  {
    let params = new HttpParams();

    if (pokemonParams.abilityId && pokemonParams.abilityId !== 0){
      params = params.append('abilitieId', pokemonParams.abilityId);
    }

    if (pokemonParams.typeId && pokemonParams.typeId !== 0){
      params = params.append('typeId', pokemonParams.typeId);
    }

    /* In API sort is already sorted by name, therefore don't apply parameter if name order is requested */
    if (pokemonParams.sort && pokemonParams.sort !== 'name'){
      params = params.append('sort', pokemonParams.sort);
    }

    params = params.append('pageIndex', pokemonParams.pageNumber);
    params = params.append('pageSize', pokemonParams.pageSize);

    if (pokemonParams.search !== '') {
      params = params.append('search', pokemonParams.search);
    }

    return this.http.get<Pagination<Pokemon[]>>(this.baseUrl + 'pokemons', { params: params });
  }

  getPokemonTypes()
  {
    return this.http.get<Type[]>(this.baseUrl + 'pokemons/types');
  }

  getPokemonAbilities()
  {
    return this.http.get<Ability[]>(this.baseUrl + 'pokemons/abilities');
  }
}
