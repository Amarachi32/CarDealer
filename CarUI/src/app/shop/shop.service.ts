import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Injectable, } from '@angular/core';
import { Pagination } from '../shared/models/pagination';
import { Product } from '../shared/models/product';
import { Brand } from '../shared/models/brand';
import { Type } from '../shared/models/type';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
baseUrl = "https://localhost:7016/api/"
  constructor(private http: HttpClient) { }
  getProducts(){
    return this.http.get<Pagination<Product[]>>(this.baseUrl + 'Product?pageSize=50');
  }

  getBrands(){
    return this.http.get<Brand[]>(this.baseUrl + 'product/brand')
  }

  getTypes(){
    return this.http.get<Type[]>(this.baseUrl + 'product/type')
  }
}
