import { Player } from 'src/app/shared/models/Player.model'

export class Team {
    
    public id: number = 0;
    public name: string = null;
    public offensPlayer: Player = null;
    public defensePlayer: Player = null;
    public rating: number = 0;

}