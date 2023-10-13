import { Component, Output, Input, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.css']
})
export class PagerComponent {
@Output() PageChanged? = new EventEmitter<number>();
@Input() totalCount? : number;
@Input() pageSize? : number;

onPagerChanged(event: any){
  this.PageChanged?.emit(event);
}
}
