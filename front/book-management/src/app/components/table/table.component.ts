import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-table',
  imports: [CommonModule],
  templateUrl: './table.component.html',
  styleUrl: './table.component.css',
})
export class TableComponent implements OnInit {
  @Input() columns: any[] = [];
  @Input() array: any[] = [];

  @Output() itemToEdit = new EventEmitter<any>();
  @Output() itemToRemove = new EventEmitter<any>();

  ngOnInit() {}

  editItem(item: any) {
    this.itemToEdit.emit(item);
  }

  removeItem(item: any) {
    this.itemToRemove.emit(item);
  }
}
