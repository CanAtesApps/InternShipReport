import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TasinmazEkleComponent } from './tasinmaz-ekle.component';

describe('TasinmazEkleComponent', () => {
  let component: TasinmazEkleComponent;
  let fixture: ComponentFixture<TasinmazEkleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TasinmazEkleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TasinmazEkleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
