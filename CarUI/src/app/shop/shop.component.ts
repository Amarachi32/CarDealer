import { Component, OnInit } from '@angular/core';
import { Product } from '../shared/models/product';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css']
})
export class ShopComponent implements OnInit {
  products: Product[] = [];
  constructor(private shopService: ShopService){
  }
  ngOnInit(): void {
    this.shopService.getProducts().subscribe({
      next: (response: any) => this.products = response.data,
      error: error => console.log(error),
      complete: () => console.log("request completed")
     })
  }
}
