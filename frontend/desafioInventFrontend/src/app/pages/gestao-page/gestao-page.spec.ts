import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GestaoPage } from './gestao-page';

describe('GestaoPage', () => {
  let component: GestaoPage;
  let fixture: ComponentFixture<GestaoPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GestaoPage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GestaoPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
