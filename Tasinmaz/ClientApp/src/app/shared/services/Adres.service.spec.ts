/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { AdresService } from './Adres.service';

describe('Service: Adres', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AdresService]
    });
  });

  it('should ...', inject([AdresService], (service: AdresService) => {
    expect(service).toBeTruthy();
  }));
});
