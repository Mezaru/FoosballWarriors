import { Component, OnInit } from '@angular/core';
import { DataService } from '../shared/service/data.service';
import { Player } from '../shared/models/Player.model';
import { SignalRService } from '../shared/service/signal-r.service';

@Component({
  selector: 'players',
  templateUrl: './players.component.html',
  styleUrls: ['./players.component.scss']
})
export class PlayersComponent implements OnInit {

  public player: Player = new Player();
  public image: File
  private baseUrl: string = 'https://localhost:44364/';
  public imagetxt: string | ArrayBuffer = this.baseUrl + 'images/user-icon.png';
  public players: Player[];
  public height: number;
  public saveButton: string = "Save";

  constructor(private dataService: DataService, private hubService: SignalRService) { }

  ngOnInit(): void {

    this.hubService.PlayerList.subscribe((players: Player[]) => {
      this.players = players;
    });
    this.dataService.GetPlayers().then(resp => {
      this.players = resp;
    });
  }

  public SavePlayer(): void {
    if (this.player.id > 0) {
      const prevPlayer = this.players.find(x => x.id == this.player.id);
      let promises = [];
      if (prevPlayer.name !== this.player.name)
        promises.push(this.dataService.UpdatePlayerName(this.player));
      if (this.baseUrl + prevPlayer.imageUrl !== this.imagetxt)
        promises.push(this.dataService.SavePlayerImage(this.player.id, this.image));

      Promise.all(promises).then(() => {
        this.Clear();
      });
    } else {
      this.dataService.SavePlayer(this.player).then((playerId: number) => {
        if (this.image)
          this.dataService.SavePlayerImage(playerId, this.image).then(() => {
            this.Clear();
          });
        else
          this.Clear();
      });
    }
  }

  public SetImage(files: FileList): void {
    this.image = files.item(0);
    this.readURL(this.image);
  }

  readURL(file: File): void {
    const reader = new FileReader();
    reader.onload = e => {
      if (reader.result !== null) {
        this.imagetxt = reader.result;
      }
      else {
        this.imagetxt = this.baseUrl + 'images/user-icon.png';
      }
    }
    reader.readAsDataURL(file);
  }

  DeletePlayer() {
    if (window.confirm(`Are you sure you want to delete ${this.player.name}`))
      this.dataService.DeletePlayer(this.player.id).then(() => {
        this.Clear();
      });
  }

  Clear() {
    this.player = new Player();
    this.imagetxt = this.baseUrl + 'images/user-icon.png';
    this.saveButton = "Save";
  }

  SelectPlayer(player: Player) {
    Object.assign(this.player, player);
    this.imagetxt = this.baseUrl + player.imageUrl;
    this.saveButton = "Uppdate";
  }
}
