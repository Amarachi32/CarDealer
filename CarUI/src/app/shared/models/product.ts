import { NonNullableFormBuilder } from "@angular/forms"

export interface Product {
    // id: NonNullableFormBuilder
    id : number
    name: string
    description: string
    price: number
    // imageFile: any
    productType: string
    productBrand: string
    pictureUrl?:any
  }
  