import { Component, OnInit } from '@angular/core';
import { Pokemon } from 'src/app/shared/models/pokemon';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-pokemon-details',
  templateUrl: './pokemon-details.component.html',
  styleUrls: ['./pokemon-details.component.scss']
})
export class PokemonDetailsComponent implements OnInit{
  pokemon?: Pokemon

  /* Inject and use our ShopService, activatedRoute into our productshop component */
  constructor(private shopService: ShopService, private activatedRoute: ActivatedRoute, private breadcrumbService: BreadcrumbService) {
    this.breadcrumbService.set('@pokemonDetailsName', ' ');
  }

  ngOnInit(): void {
    this.getPokemon();
  }

  getPokemon() {
    let name = this.activatedRoute.snapshot.paramMap.get('name');

    if (name) {
      this.shopService.getPokemon(name).subscribe({
        next: response => {
          this.pokemon = response; 
          this.breadcrumbService.set('@pokemonDetailsName', this.pokemon.name)
        },
        error: error => console.log(error)
      })
    }
  }
}
