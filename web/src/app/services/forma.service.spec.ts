import { TestBed } from '@angular/core/testing';

import { FormaService } from './forma.service';

describe('FormaService', () => {
  let service: FormaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FormaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
