import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetalhesModal } from './detalhes-modal';

describe('DetalhesModal', () => {
  let component: DetalhesModal;
  let fixture: ComponentFixture<DetalhesModal>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DetalhesModal]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DetalhesModal);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
