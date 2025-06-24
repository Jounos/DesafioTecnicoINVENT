import { TestBed } from '@angular/core/testing';

import { EquipamentoEletronicoService } from './equipamento-eletronico-service';

describe('EquipamentoEletronicoService', () => {
	let service: EquipamentoEletronicoService;

	beforeEach(() => {
		TestBed.configureTestingModule({});
		service = TestBed.inject(EquipamentoEletronicoService);
	});

	it('should be created', () => {
		expect(service).toBeTruthy();
	});
});
