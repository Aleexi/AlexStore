import { Component, OnInit } from '@angular/core';
import { Product } from '../../shared/models/product';
import { ShopService } from '../shop.service';
import { Brand } from 'src/app/shared/models/brand';
import { Type } from 'src/app/shared/models/type';
import { ProductParams } from 'src/app/shared/models/productParams';

@Component({
  selector: 'app-productshop',
  templateUrl: './productshop.component.html',
  styleUrls: ['./productshop.component.scss']
})
export class ProductshopComponent implements OnInit{
  products: Product[] = [];
  brands: Brand[] = [];
  types: Type[] = [];
  productParams = new ProductParams();
  sortAlternatives = [
    {name: 'Name Ordering', ApiValue: 'name'},
    {name: 'Price Ascending', ApiValue: 'priceAsc'},
    {name: 'Price Descending', ApiValue: 'priceDesc'}
  ];
  Count: number = 0;

  /* Inject and use our ShopService into our productshop component */
  constructor(private shopService: ShopService) {}

  ngOnInit(): void {
    this.getProducts();
    this.getProductTypes();
    this.getProductBrands();
  }

  getProducts()
  {
    this.shopService.getProducts(this.productParams).subscribe({
      next: response => {
        this.products = response.data; 
        this.productParams.pageNumber = response.pageIndex; 
        this.productParams.pageSize = response.pageSize;
        this.Count = response.count; 
      },
      error: error => console.log(error)
    });
  }

  getProductTypes()
  {
    this.shopService.getProductTypes().subscribe({
      next: response => this.types = [{id: 0, name: 'All'}, ...response],
      error: error => console.log(error)
    });
  }

  getProductBrands()
  {
    this.shopService.getProductBrands().subscribe({
      next: response => this.brands = [{id: 0, name: 'All'}, ...response],
      error: error => console.log(error)
    });
  }

  onTypeSelected(typeId: number)
  {
    if (this.productParams.typeId !== typeId){
      this.productParams.typeId = typeId;
      this.getProducts();
    }
    
  }

  onBrandSelected(brandId: number)
  {
    if (this.productParams.brandId !== brandId){
      this.productParams.brandId = brandId;
      this.getProducts();
    }
  }

  onSortSelected(event: any) {
    if (this.productParams.sort !== event.target.value){
      this.productParams.sort = event.target.value;
      this.getProducts();
    }
  }

  onPageChanged(event: any) {
    if (this.productParams.pageNumber !== event.page){
      this.productParams.pageNumber = event.page;
      this.getProducts();
    }
  }
}
