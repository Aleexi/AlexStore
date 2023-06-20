import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Product } from './models/product';
import { Pokemon } from './models/pokemon';
import { Pagination } from './models/pagination';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title: string = 'AlexStore';
  products: Product[] = [];
  pokemons: Pokemon[] = [];

  /* Inject Http Client and other services into our App component */
  constructor(private http: HttpClient) {}

  /* Create HTTP request to get data from API */
  ngOnInit(): void {
    this.http.get<Pagination<Product[]>>('https://localhost:5001/api/products').subscribe({
      next: response => this.products = response.data, /* When we get the stream of data, we print it into the console */
      error: error => console.log(error), /* If error occur, print it into the console */
      complete: () => {
        console.log("Received products from https://localhost:5001/api/products");
      }
    });

    this.http.get<Pagination<Pokemon[]>>('https://localhost:5001/api/pokemons').subscribe({
      next: response => this.pokemons = response.data, /* When we get the stream of data, we print it into the console */
      error: error => console.log(error), /* If error occur, print it into the console */
      complete: () => {
        console.log("Received pokemons from https://localhost:5001/api/pokemons");
      }
    });
  }
}
