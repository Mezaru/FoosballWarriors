import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-add-match',
  templateUrl: './add-match.component.html',
  styleUrls: ['./add-match.component.scss']
})
export class AddMatchComponent implements OnInit {

  teamOneValue: number | string;
  teamTwoValue: number | string;
  attack1;
  defence1;
  attack2;
  defence2;
  
  playerList = [];

  constructor() { }

  ngOnInit(): void {
    this.teamOneValue = 1;
    this.teamTwoValue = 1;
    this.playerList = [ 
      {name: "test1", value: "testId1"},
      {name: "test2", value: "testId2"},
      {name: "test3", value: "testId3"},
      {name: "test4", value: "testId4"}
    ]
  }

  SaveMatch() {
    console.log(this.teamOneValue);
    console.log(this.teamTwoValue);
    console.log(this.attack1);
    console.log(this.defence1);
    console.log(this.attack2);
    console.log(this.defence2);

  }
}
