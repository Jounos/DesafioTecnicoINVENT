import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SalvarPage } from './salvar-page';

describe('SalvarPage', () => {
  let component: SalvarPage;
  let fixture: ComponentFixture<SalvarPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SalvarPage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SalvarPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
