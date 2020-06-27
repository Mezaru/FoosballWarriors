import { HttpClient, HttpHeaders, HttpEventType } from '@angular/common/http';
import { Player } from '../models/Player.model'
import { Injectable } from '@angular/core'
import { stringify } from 'querystring';

@Injectable({
    providedIn: 'root'
})
export class DataService {
    private url: string = "https://localhost:44364/";
    private headers: HttpHeaders;
    private progress: number;

    constructor(private client: HttpClient) {
        this.headers = new HttpHeaders().append('accept', 'application/json');
    }

    public GetPlayers(): Promise<Player[]> {
        return new Promise((resolve, reject) => {
            this.client.get<Player[]>(this.url + 'Players/get', { headers: this.headers }).subscribe(next => {
                resolve(next);
            }, error => {
                console.log(error);
                reject(error);
            })
        });
    }

    public SavePlayer(player: Player): Promise<number> {

        return new Promise((resolve, reject) => {
            this.client.post(this.url + 'Players/post', player, { headers: this.headers }).subscribe((playerId: number) => {
                resolve(playerId);
            }, error => {
                console.log(error);
                reject(error);
            });
        });
    }

    public SavePlayerImage(playerId: number, file: File): Promise<void> {
        let formData = new FormData();
        formData.append('file', file);

        return new Promise((resolve, reject) => {
            this.client.post(this.url + 'PlayersImage/post/' + playerId, formData, { headers: this.headers, reportProgress: true }).subscribe(resp => {
                resolve();
            }, error => {
                console.log(error);
                reject();
            });
        });
    }

    public UpdatePlayerName(player: Player){
        return new Promise((resolve, reject) => {
            this.client.put(this.url + 'Players/put/', player, { headers: this.headers, reportProgress: true }).subscribe(resp => {
                resolve();
            }, error => {
                console.log(error);
                reject();
            });
        });
    }

    public DeletePlayer(playerId: number){
        return new Promise((resolve, reject) => {
            this.client.delete(this.url + `Players/Delete/${playerId}`, { headers: this.headers }).subscribe(resp => {
                resolve(resp);
                
            }, error => {
                console.log(error);
                reject(false);
            });
        });
    }

}