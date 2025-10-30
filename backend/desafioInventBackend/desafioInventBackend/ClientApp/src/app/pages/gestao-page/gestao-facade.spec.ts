import { TestBed } from '@angular/core/testing';

import { GestaoFacade } from './gestao-facade';

describe('GestaoFacade', () => {
  let service: GestaoFacade;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GestaoFacade);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
