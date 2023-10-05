import { Component, Input } from '@angular/core';
import { Product } from 'src/app/shared/models/product';
@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.css']
})
export class ProductItemComponent {
  @Input() product: Product = { name: '', description: '', price: 0, imageFile: null, productType: '', productBrand: '' };

}
