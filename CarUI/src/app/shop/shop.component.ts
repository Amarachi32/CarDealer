import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Product } from '../shared/models/product';
import { ShopService } from './shop.service';
import { Brand } from '../shared/models/brand';
import { Type } from '../shared/models/type';
import { ShopParams } from '../shared/models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css']
})
export class ShopComponent implements OnInit {
  @ViewChild('search') searchTerm? : ElementRef;
  products: Product[] = [];
  brands: Brand[] = [];
  types: Type[] = [];
  ShopParams = new ShopParams();
  sortOptions = [
    {name: 'Alphabetical',value: 'name'},
    {name: 'Price: Low to High',value: 'priceAsc'},
    {name: 'Price: High to low',value: 'priceDes'}
  ];
  totalCount = 0;
  constructor(private shopService: ShopService){
  }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  //  getProducts(){
  //   // console.log('ngOnInit called');
  //   this.shopService.getProducts(this.brandIdSelected, this.typeIdSelected).subscribe(
  //     (response) => {
  //       console.log('API Response:', response);
  //       // Check if 'response.data' is an array before assigning to 'this.products'
  //       if (Array.isArray(response.data)) {
  //         this.products = response.data;
  //         console.log('Products:', this.products);
  //       } else {
  //         console.error('Response data is not an array:', response.data);
  //       }
  //     },
  //     (error) => {
  //       console.error('Error fetching products:', error);
  //     },
  //     () => {
  //       console.log('Request completed');
  //     }
  //   );
  //  }
  
  getProducts(){
    this.shopService.getProducts(this.ShopParams).subscribe({
      next: response => {
        this.products = response.data;
        this.ShopParams.pageNumber = response.pageIndex;
        this.ShopParams.pageSize = response.pageSize;
        this.totalCount = response.count;
      },
        error: error => console.log(error)
    })
  }
   getBrands() {
    this.shopService.getBrands().subscribe({
      next: (response: any) => {
        this.brands = [{id:0, name: 'All'}, ...response]; // Ensure 'data' is the correct property
        console.log('Brands:', this.brands);
      },
      error: error => {
        console.error('Error fetching brands:', error);
      },
      complete: () => {
        console.log('Request completed');
      }
    });
  }
  

  getTypes() {
    this.shopService.getTypes().subscribe({
      next: response => this.types = [{id: 0, name: 'All'}, ...response],

      // next: (response: any) => this.types = [{id: 0, name: 'All'}, ...response],
       error: error => console.log(error),
       complete: () => console.log("request completed")
    });
  }

  onBrandSelected(brandId:number){
    this.ShopParams.brandId = brandId;
    this.ShopParams.pageNumber = 1;
    console.log('Selected Brand ID:', this.ShopParams.brandId);
    this.getBrands();
  }

  onTypeSelected(typeId:number){
    this.ShopParams.typeId = typeId;
    this.ShopParams.pageNumber = 1;
    this.getProducts();
  }
    
  onSortSelected(event:any){
    this.ShopParams.sort = event.target.value;
    this.getProducts();
  }

  onPageChanged(event:any){
    if(this.ShopParams.pageNumber !== event){
      this.ShopParams.pageNumber = event;
      this.getProducts();
    }
  }

  onSearch(){
    this.ShopParams.search = this.searchTerm?.nativeElement.value;
    this.ShopParams.pageNumber = 1;
    this.getProducts();
  }

  onReset(){
    if(this.searchTerm) this.searchTerm.nativeElement.value = '';
    this.ShopParams = new ShopParams();
    this.getProducts();
  }
}
