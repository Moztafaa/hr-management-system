import { TestBed } from '@angular/core/testing';

import { Attendance } from './attendance';

describe('Attendance', () => {
  let service: Attendance;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Attendance);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
