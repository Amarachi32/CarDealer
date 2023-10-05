import { Component, OnInit } from '@angular/core';
import { Product } from '../shared/models/product';
import { ShopService } from './shop.service';
import { Brand } from '../shared/models/brand';
import { Type } from '../shared/models/type';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css']
})
export class ShopComponent implements OnInit {
  products: Product[] = [];
  brands: Brand[] = [];
  types: Type[] = [];
  constructor(private shopService: ShopService){
  }
  // ngOnInit(): void {
  //   this.shopService.getProducts().subscribe({
  //     next: (response: any) => this.products = response.data,
  //     error: error => console.log(error),
  //     complete: () => console.log("request completed")
  //    })
  // }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes()
  }
   getProducts(){
    console.log('ngOnInit called');
    this.shopService.getProducts().subscribe(
      (response) => {
        console.log('API Response:', response);
        // Check if 'response.data' is an array before assigning to 'this.products'
        if (Array.isArray(response.data)) {
          this.products = response.data;
          console.log('Products:', this.products);
        } else {
          console.error('Response data is not an array:', response.data);
        }
      },
      (error) => {
        console.error('Error fetching products:', error);
      },
      () => {
        console.log('Request completed');
      }
    );
   }
  
   getBrands(){
      this.shopService.getBrands().subscribe({
      next: (response: any) => this.brands = response.data,
      error: error => console.log(error),
      complete: () => console.log("request completed")
     })
   }

   getTypes(){
    this.shopService.getTypes().subscribe({
    next: (response: any) => this.types = response.data,
    error: error => console.log(error),
    complete: () => console.log("request completed")
   })
 }
}
