import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'CarUI';
  //products: Product[] = [];
  constructor(){

  }
  ngOnInit(): void {
  //  this.http.get<Pagination<Product[]>>('https://localhost:7016/api/Product?pageSize=50').subscribe({
  //  //this.http.get('https/localhoss:7016/api/product').subscribe({
  //   next: (response: any) => this.products = response.data,
  //   error: error => console.log(error),
  //   complete: () => console.log("request completed")
  //  })
  }
}
