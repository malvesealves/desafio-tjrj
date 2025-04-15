import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-table',
  imports: [CommonModule],
  templateUrl: './table.component.html',
  styleUrl: './table.component.css'
})
export class TableComponent implements OnInit{
  @Input() columns: string[] = [];
  @Input() array: any[] = [];

  ngOnInit() {
    console.log(this.array);
    console.log(this.columns);
  }
}
