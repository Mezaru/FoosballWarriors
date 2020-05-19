import { Component, OnInit } from '@angular/core';
import { DataService } from '../shared/service/data.service';
import { Player } from '../shared/models/Player.model';

@Component({
  selector: 'players',
  templateUrl: './players.component.html',
  styleUrls: ['./players.component.scss']
})
export class PlayersComponent implements OnInit {

  public baseUrl: string = "https://localhost:44364/";
  public newPlayer: Player = new Player();
  public image: File
  public imagetxt: string | ArrayBuffer = 'https://localhost:44364/images/user-icon.png';
  public players: Player[];

  constructor(private dataService : DataService) { }

  ngOnInit(): void {
    this.dataService.GetPlayers().then(resp => {
      this.players = resp;
    });
  }

  public SaveNewPlayer(): void {
    console.log(this.newPlayer);
    this.dataService.SavePlayer(this.newPlayer).then((playerId: number) => {
      if(this.image)
        this.dataService.SavePlayerImage(playerId, this.image);
    });
  }  

  public SetImage(files: FileList): void{
    this.image = files.item(0);
    this.readURL(this.image);
  }

  readURL(file: File): void {
        const reader = new FileReader();
        reader.onload = e => this.imagetxt = reader.result;
        reader.readAsDataURL(file);
  }
}
