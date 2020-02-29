import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectPointsComponent } from './select-points.component';

describe('SelectPointsComponent', () => {
  let component: SelectPointsComponent;
  let fixture: ComponentFixture<SelectPointsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SelectPointsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SelectPointsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
