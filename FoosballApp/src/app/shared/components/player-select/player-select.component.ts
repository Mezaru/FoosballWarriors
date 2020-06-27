import { Component, OnInit, Input, Output, EventEmitter, ViewChild, ComponentFactoryResolver, ViewContainerRef, ComponentRef } from '@angular/core';
import { ChoosePlayerComponent } from '../choose-player/choose-player.component';
import { SelectPlayer } from '../../models/SelectPlayer.model';

@Component({
  selector: 'player-select',
  templateUrl: './player-select.component.html',
  styleUrls: ['./player-select.component.scss']
})
export class PlayerSelectComponent implements OnInit {

  @Input() players: SelectPlayer[];
  @Input() selectedPlayer: SelectPlayer;
  @Output() playersChange = new EventEmitter();
  @Output() selectedPlayerChange = new EventEmitter();

  @ViewChild("choosePlayer", {read: ViewContainerRef}) choosePlayerRef: ViewContainerRef
  private ref: ComponentRef<ChoosePlayerComponent>;
  public selectedPlayerName: string = "Select player";

  constructor(private componentFactoryResolver: ComponentFactoryResolver) { }

  ngOnInit(): void {

  }

  SelectPlayer(player: SelectPlayer){
    if(this.selectedPlayer)
      this.selectedPlayer.isSelected = false;

    var selectPlayer = this.players.find(x => player.player.id == x.player.id);
    selectPlayer.isSelected = true;

    this.selectedPlayer = selectPlayer;
    this.selectedPlayerName = selectPlayer.player.name;

    this.selectedPlayerChange.emit(this.selectedPlayer);
    this.playersChange.emit(this.players);
  }

  ChoosePlayer(){
    this.choosePlayerRef.clear()

    const componentFactory = this.componentFactoryResolver.resolveComponentFactory(ChoosePlayerComponent);

    this.ref = this.choosePlayerRef.createComponent(componentFactory);
    this.ref.instance.players = this.players;
    this.ref.instance.close.subscribe(() => {
      setTimeout(() => this.ref.destroy(), 0);
    });
    this.ref.instance.selectedPlayerChange.subscribe((player: SelectPlayer) => {
      this.SelectPlayer(player);
      setTimeout(() => this.ref.destroy(), 0);
    });
  }

  RemovePlayer(){
    if(this.selectedPlayer){
      this.selectedPlayer.isSelected = false;
      this.selectedPlayer = null;
      this.selectedPlayerName = "Select player"
      this.selectedPlayerChange.emit(null);
      this.playersChange.emit(this.players);
    }
  }
}
