import { Component, OnInit, Input } from '@angular/core';
import { SelectPlayer } from '../shared/models/SelectPlayer.model';
import { DataService } from '../shared/service/data.service';
import { SignalRService } from '../shared/service/signal-r.service';
import { Player } from '../shared/models/Player.model';

@Component({
  selector: 'app-add-match',
  templateUrl: './add-match.component.html',
  styleUrls: ['./add-match.component.scss']
})
export class AddMatchComponent implements OnInit {

  teamOneValue: number | string;
  teamTwoValue: number | string;
  attack1: SelectPlayer;
  defence1: SelectPlayer;
  attack2: SelectPlayer;
  defence2: SelectPlayer;
  
  playerList: SelectPlayer[] = [];

  constructor(private dateService: DataService, private hubService: SignalRService) { }

  ngOnInit(): void {
    this.teamOneValue = 1;
    this.teamTwoValue = 1;
    this.hubService.PlayerList.subscribe((players: Player[])=> {
      this.convertPlayers(players);
    });
     this.dateService.GetPlayers().then((players: Player[]) => {
      this.convertPlayers(players);
    });
  }

  convertPlayers(players: Player[]){
    players.forEach(player => {
      const existingPlayer = this.playerList.find(x => x.player.id == player.id);
      if(existingPlayer){
        existingPlayer.player = player;
      } else {
        const selectPlayer = new SelectPlayer();
        selectPlayer.isSelected = false,
        selectPlayer.player = player;
        this.playerList.push(selectPlayer);
      }
    });
    if(this.playerList.length > players.length){
      this.playerList.forEach(player => {
        if(!players.find(x => x.id == player.player.id)){
          const index = this.playerList.findIndex(x => x == player);
          this.playerList.splice(index, 1);
        }
      });
    }
  }

  SaveMatch() {
    console.log(this.teamOneValue);
    console.log(this.teamTwoValue);
    console.log(this.attack1.player);
    console.log(this.defence1.player);
    console.log(this.attack2.player);
    console.log(this.defence2.player);
  }
}
