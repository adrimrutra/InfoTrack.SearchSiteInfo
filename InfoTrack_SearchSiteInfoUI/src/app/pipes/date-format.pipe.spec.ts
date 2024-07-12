import { DateFormatPipe } from './date-format.pipe';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { inject } from '@angular/core/testing';

describe('Pipe:DateFormatPipe', () => {
  let pipe: DateFormatPipe;

  beforeEach(inject([], () => {
    pipe = new DateFormatPipe();
  }));

  it('should return Data Format', inject([], () => {
    const value = '01/01/2023 12:00';
    const expectedResult = 'January 1st 2023, 12:00:00 pm';

    const result = pipe.transform(value);

    expect(result).toEqual(expectedResult);
  }));
});
