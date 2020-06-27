import { Player } from 'src/app/shared/models/Player.model'

export class Match {
    
    public id: number = 0;
    public offencePlayer1: Player = null;
    public offencePlayer2: Player = null;
    public defencePlayer1: Player = null;
    public defencePlayer2: Player = null;
    public scoreTeam1: number = 0;
    public scoreTeam2: number = 0;

}