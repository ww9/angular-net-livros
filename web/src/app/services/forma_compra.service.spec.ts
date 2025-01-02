import { TestBed } from '@angular/core/testing';

import { FormaCompraService } from './forma_compra.service';

describe('FormaCompraService', () => {
  let service: FormaCompraService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FormaCompraService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
