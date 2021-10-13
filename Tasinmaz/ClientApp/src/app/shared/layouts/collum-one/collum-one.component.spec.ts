import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CollumOneComponent } from './collum-one.component';

describe('CollumOneComponent', () => {
  let component: CollumOneComponent;
  let fixture: ComponentFixture<CollumOneComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CollumOneComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CollumOneComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
