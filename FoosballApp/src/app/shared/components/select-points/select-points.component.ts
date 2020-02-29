import { Component, OnInit, Output, Input, EventEmitter } from '@angular/core';

@Component({
  selector: 'select-points',
  templateUrl: './select-points.component.html',
  styleUrls: ['./select-points.component.scss']
})
export class SelectPointsComponent implements OnInit {
  
  numbers = [1, 2, 3, 4, 5, 6 ,7 ,8 ,9 ,10]
  @Input() value: number;
  @Output() valueChange = new EventEmitter();
  constructor() {
  }
  
  ngOnInit(): void {
  }

  ChangeValue() {
    this.valueChange.emit(this.value.toString());
  }
}
