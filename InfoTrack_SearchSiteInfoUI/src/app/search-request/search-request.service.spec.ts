import { TestBed } from '@angular/core/testing';
import { SearchRequestService } from './search-request.service';
import { MockBuilder } from 'ng-mocks';
import { AppModule } from '../app.module';
import { HttpClient } from '@angular/common/http';
import { createSpyFromClass, Spy } from 'jasmine-auto-spies';
import { BehaviorSubject, Observable } from 'rxjs';
import { BaseReponse } from '../models/base-response';
import { SearchRequestResponse } from '../models/search-request-response';
import { SearchRequest } from '../models/search-request';
import { Engine } from '../models/engine';

describe('SearchRequestService', () => {
  let service: SearchRequestService;
  let httpClientSpy: Spy<HttpClient>;

  const response = () => {
    return {
      data: {
        id: 1,
        keywords: 'land registry searches',
        url: 'www.infotrack.co.uk',
        engine: Engine.GOOGLE,
        createdDate: '',
        rank: 10,
      },
      errors: [],
      message: 'message',
      succcess: true,
    } as BaseReponse<SearchRequestResponse>;
  };

  let responseSubject: BehaviorSubject<BaseReponse<SearchRequestResponse>> =
    new BehaviorSubject(response());

  let response$: Observable<BaseReponse<SearchRequestResponse>> =
    responseSubject.asObservable();

  const configureSpies = () => {
    httpClientSpy = createSpyFromClass(HttpClient);
    httpClientSpy.post.and.returnValue(response$);
  };

  beforeEach(async () => {
    configureSpies();
    await MockBuilder(SearchRequestService, AppModule).provide({
      provide: HttpClient,
      useValue: httpClientSpy,
    });

    service = TestBed.inject(SearchRequestService);
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

    service.postSearchRequest(searchRequest).subscribe((value) => {
      expect(value).toEqual(expectedResult);

      done();
    });
  });
});
