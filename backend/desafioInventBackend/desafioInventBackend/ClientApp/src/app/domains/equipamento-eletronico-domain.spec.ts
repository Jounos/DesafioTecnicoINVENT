import { TestBed } from '@angular/core/testing';

import { EquipamentoEletronicoDomain } from './equipamento-eletronico-domain';

describe('EquipamentoEletronicoDomain', () => {
  let service: EquipamentoEletronicoDomain;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EquipamentoEletronicoDomain);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
