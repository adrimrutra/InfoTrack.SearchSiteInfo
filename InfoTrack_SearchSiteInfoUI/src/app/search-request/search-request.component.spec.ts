import { ComponentFixture } from '@angular/core/testing';
import { MockBuilder, MockRender, ngMocks } from 'ng-mocks';
import { Spy, createSpyFromClass } from 'jasmine-auto-spies';

import { SearchRequestComponent } from './search-request.component';
import { AppModule } from '../app.module';
import { SearchRequestService } from './search-request.service';
import { BaseReponse } from '../models/base-response';
import { SearchRequestResponse } from '../models/search-request-response';
import { BehaviorSubject, Observable } from 'rxjs';
import { Engine } from '../models/engine';
import { FormBuilder, Validators } from '@angular/forms';
import { REG_EX } from '../constants/constants';

describe('SearchRequestComponent', () => {
  let searchRequestServiceSpy: Spy<SearchRequestService>;
  let formBuilderSpy: Spy<FormBuilder>;
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
    searchRequestServiceSpy = createSpyFromClass(SearchRequestService);
    formBuilderSpy = createSpyFromClass(FormBuilder);
    searchRequestServiceSpy.postSearchRequest.and.returnValue(response$);
    formBuilderSpy.group.and.returnValue(
      new FormBuilder().group({
        keywords: ['', [Validators.required, Validators.maxLength(200)]],
        url: ['', [Validators.required, Validators.pattern(REG_EX)]],
        engines: [Engine.GOOGLE, Validators.required],
      })
    );
  };
  beforeEach(async () => {
    configureSpies();
    await MockBuilder(SearchRequestComponent, AppModule)
      .provide({
        provide: SearchRequestService,
        useValue: searchRequestServiceSpy,
      })
      .provide({
        provide: FormBuilder,
        useValue: formBuilderSpy,
      });
  });
  const getFixture = (payload: any = {}) =>
    MockRender(SearchRequestComponent, {
      ...payload,
    });
  it('should create', () => {
    const fixture = getFixture();
    expect(fixture.componentInstance).toBeDefined();
  });

  it('renders app-search-request inside of the SearchRequestComponent', () => {
    const fixture = getFixture();
    const ganttPopover = ngMocks.find(SearchRequestComponent);
    expect(ganttPopover.componentInstance).toBeDefined();
  });
});
