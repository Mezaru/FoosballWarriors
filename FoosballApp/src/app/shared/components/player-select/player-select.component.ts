import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'player-select',
  templateUrl: './player-select.component.html',
  styleUrls: ['./player-select.component.scss']
})
export class PlayerSelectComponent implements OnInit {

  @Input() values;
  @Input() selectedValue;
  @Output() valuesChange = new EventEmitter();
  @Output() selectedValueChange = new EventEmitter();

  currentValue;

  constructor() { }

  ngOnInit(): void {
  }

  SelectPlayer(player){

    let tmpList = Object.create(this.values);

    if(player !== undefined){
      var index = tmpList.findIndex(x => x.value === player.value)
      tmpList.splice(index, 1);
    }

    if(this.currentValue !== undefined) {
      tmpList.push(this.currentValue);
    }
    this.currentValue = player;   

    this.values = tmpList.sort(function(a, b) {
      var nameA = a.name.toUpperCase();
      var nameB = b.name.toUpperCase();

      if(nameA < nameB)
        return -1;
      if(nameA > nameB)
        return 1;

      return 0;
    });

    this.selectedValueChange.emit(this.selectedValue);
    this.valuesChange.emit(this.values);
  }
}
