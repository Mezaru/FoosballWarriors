import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { SelectPlayer } from '../../models/SelectPlayer.model';
import { Player } from '../../models/Player.model';

@Component({
  selector: 'app-choose-player',
  templateUrl: './choose-player.component.html',
  styleUrls: ['./choose-player.component.scss']
})
export class ChoosePlayerComponent implements OnInit {

  constructor() { }

  @Input() players: SelectPlayer[];
  @Output() close: EventEmitter<any> = new EventEmitter();
  @Output() selectedPlayerChange: EventEmitter<SelectPlayer> = new EventEmitter<SelectPlayer>();

  ngOnInit(): void {

  }

  ClosePopUp(event){
    if(event.target.className === "popup" || event.target.className == "exitButton"){
      this.close.emit(null);
    }
  }

  SelectPlayer(selectedPlayer: Player){
    var tmpPlayer = this.players.find(x => x.player.id == selectedPlayer.id);
    this.selectedPlayerChange.emit(tmpPlayer);
  }
}
