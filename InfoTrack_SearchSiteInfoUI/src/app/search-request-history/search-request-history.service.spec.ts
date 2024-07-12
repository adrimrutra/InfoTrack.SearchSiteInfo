import { TestBed } from '@angular/core/testing';

import { SearchRequestHistoryService } from './search-request-history.service';
import { createSpyFromClass, Spy } from 'jasmine-auto-spies';
import { HttpClient } from '@angular/common/http';
import { Engine } from '../models/engine';
import { BaseReponse } from '../models/base-response';
import { SearchRequestResponse } from '../models/search-request-response';
import { BehaviorSubject, Observable } from 'rxjs';
import { AppModule } from '../app.module';
import { MockBuilder } from 'ng-mocks';
import { SearchRequest } from '../models/search-request';

describe('SearchRequestHistoryService', () => {
  let service: SearchRequestHistoryService;
  let httpClientSpy: Spy<HttpClient>;

  const response = () => {
    return {
      data: [
        {
          id: 1,
          keywords: 'land registry searches',
          url: 'www.infotrack.co.uk',
          engine: Engine.GOOGLE,
          createdDate: '',
          rank: 10,
        },
        {
          id: 2,
          keywords: 'land registry searches',
          url: 'www.infotrack.co.uk',
          engine: Engine.BING,
          createdDate: '',
          rank: 20,
        },
        {
          id: 3,
          keywords: 'land registry searches',
          url: 'www.infotrack.co.uk',
          engine: Engine.YAHOO,
          createdDate: '',
          rank: 30,
        },
      ],
      errors: [],
      message: 'message',
      succcess: true,
    } as BaseReponse<Array<SearchRequestResponse>>;
  };

  let responseSubject: BehaviorSubject<
    BaseReponse<Array<SearchRequestResponse>>
  > = new BehaviorSubject(response());

  let response$: Observable<BaseReponse<Array<SearchRequestResponse>>> =
    responseSubject.asObservable();

  const configureSpies = () => {
    httpClientSpy = createSpyFromClass(HttpClient);
    httpClientSpy.get.and.returnValue(response$);
  };

  beforeEach(async () => {
    configureSpies();
    await MockBuilder(SearchRequestHistoryService, AppModule).provide({
      provide: HttpClient,
      useValue: httpClientSpy,
    });

    service = TestBed.inject(SearchRequestHistoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should return BaseReponse<SearchRequestResponse> object', (done: DoneFn) => {
    const searchRequest: SearchRequest = {
      keywords: 'land registry searches',
      url: 'www.infotrack.co.uk',
      engine: Engine.GOOGLE,
    };

    const expectedResult = response();

    service.getAllSearchRequestHistory().subscribe((value) => {
      expect(value).toEqual(expectedResult);

      done();
    });
  });
});
