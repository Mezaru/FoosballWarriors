import { Player } from 'src/app/shared/models/Player.model'
import { Team } from 'src/app/shared/models/Team.model'

export class Match {
    
    public OffencePlayer1: Player = null;
    public OffencePlayer2: Team = null;
    public DefencePlayer1: Player = null;
    public DefencePlayer2: Player = null;
    public ScoreTeam1: number = 0;
    public ScoreTeam2: number = 0;

}