import { TestBed } from '@angular/core/testing';

import { RelatorioPorAutorService } from './relatorio-por-autor.service';

describe('RelatorioPorAutorService', () => {
  let service: RelatorioPorAutorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RelatorioPorAutorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
