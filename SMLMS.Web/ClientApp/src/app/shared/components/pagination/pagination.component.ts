import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
export class Page {
  page: number;
  itemsPerPage: number;
}
@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css']
})
export class PaginationComponent implements OnInit {

  // @Input() page: number;
  // @Input() count: number;
  // @Input() perPage: number;
  // @Input() loading: boolean;
  @Input() pagesToShow: number;

  @Input() pageSize: number;
  @Input() pageNumber: number;
  @Input() totalRecord: number;
  currentPage= 1;
 

  @Output() goPrev = new EventEmitter<boolean>();
  @Output() goNext = new EventEmitter<boolean>();
  @Output() goPage = new EventEmitter<number>();

  constructor() { }
  ngOnInit() {

  }
  getMin(): number {
    return ((this.pageSize * this.pageNumber) - this.pageSize) + 1;
  }

  getMax(): number {
    let max = this.pageSize * this.pageNumber;
    if (max > this.totalRecord) {
      max = this.totalRecord;
    }
    return max;
  }

  onPage(n: number): void {
    this.goPage.emit(n);
  }

  onPrev(): void {
    this.goPrev.emit(true);
  }

  onNext(next: boolean): void {
    this.goNext.emit(next);
  }

  totalPages(): number {
    let _pages =Math.ceil(this.totalRecord / this.pageSize) || 0;
    if(this.totalRecord % this.pageSize != 0)
    _pages++;
    return _pages;
  }

  lastPage(): boolean {
    return this.pageSize * this.pageNumber > this.totalRecord;
  }

  getPages(): number[] {
    const c = Math.ceil(this.totalRecord / this.pageSize);
    const p = this.pageNumber || 1;
    const pagesToShow = this.pagesToShow || 10;
    const pages: number[] = [];
    pages.push(p);
    const times = pagesToShow - 1;
    for (let i = 0; i < times; i++) {
      if (pages.length < pagesToShow) {
        if (Math.min.apply(null, pages) > 1) {
          pages.push(Math.min.apply(null, pages) - 1);
        }
      }
      if (pages.length < pagesToShow) {
        if (Math.max.apply(null, pages) < c) {
          pages.push(Math.max.apply(null, pages) + 1);
        }
      }
    }
    pages.sort((a, b) => a - b);
    return pages;
  }
}
