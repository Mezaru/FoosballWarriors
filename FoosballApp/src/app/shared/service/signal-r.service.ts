import { Injectable, EventEmitter } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { Player } from '../models/Player.model';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  private hubConnection: signalR.HubConnection;
  public PlayerList: EventEmitter<Player[]> = new EventEmitter<Player[]>();

  constructor() {
    this.buildConnection();
    this.startConnection();
   }

  public buildConnection() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:44364/playerHub')
      .build();
  }

  public startConnection() {
    this.hubConnection.start()
    .then(() => {
      console.log('SignalR is connected');
      this.registerEvents();
    })
    .catch(() => {
      console.log('Error when connecting to SignalR');
      setTimeout(this.startConnection, 5000);
    })
  }

  private registerEvents() {
    this.hubConnection.on("PlayerList", (players: Player[]) => 
    {
      this.PlayerList.emit(players);
    });
  }

}
