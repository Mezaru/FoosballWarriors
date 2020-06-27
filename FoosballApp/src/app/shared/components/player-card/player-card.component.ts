import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Player } from '../../models/Player.model';

@Component({
  selector: 'player-card',
  templateUrl: './player-card.component.html',
  styleUrls: ['./player-card.component.scss']
})
export class PlayerCardComponent implements OnInit {

  @Input() player: Player;
  @Output() cardClick: EventEmitter<Player> = new EventEmitter<Player>();
  public height: number;
  public width: number;

  public baseUrl: string = "https://localhost:44364/";
  constructor() { }

  ngOnInit(): void {
    
    var image = document.getElementById('image-container');

    this.height = image.clientWidth;
    this.width = image.clientWidth;
  }

  IsClicked(){
    this.cardClick.emit(this.player);
  }
}
