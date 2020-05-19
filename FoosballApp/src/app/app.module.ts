import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgSelectModule } from '@ng-select/ng-select';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MenyComponent } from './meny/meny.component';
import { GridComponent } from './grid/grid.component';
import { AddMatchComponent } from './add-match/add-match.component';
import { RouterModule } from '@angular/router';
import { HistoryComponent } from './history/history.component';
import { GenerateTeamsComponent } from './generate-teams/generate-teams.component';
import { SelectPointsComponent } from './shared/components/select-points/select-points.component';
import { PlayerSelectComponent } from './shared/components/player-select/player-select.component';
import { PlayersComponent } from './players/players.component';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    MenyComponent,
    GridComponent,
    AddMatchComponent,
    SelectPointsComponent,
    HistoryComponent,
    GenerateTeamsComponent,
    PlayerSelectComponent,
    PlayersComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    NgxChartsModule,
    BrowserAnimationsModule,
    NgSelectModule,
    HttpClientModule,
    RouterModule.forRoot([
      { path: 'AddMatch', component: AddMatchComponent},
      { path: 'GenerateTeams', component: GenerateTeamsComponent },
      { path: 'History', component: HistoryComponent },
      { path: 'Players', component: PlayersComponent },
      { path: '', component: GridComponent },
      { path: '**', component: GridComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
