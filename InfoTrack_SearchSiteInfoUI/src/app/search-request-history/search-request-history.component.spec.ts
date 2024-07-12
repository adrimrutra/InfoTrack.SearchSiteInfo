import { MockBuilder, MockRender, ngMocks } from 'ng-mocks';
import { Spy, createSpyFromClass } from 'jasmine-auto-spies';

import { SearchRequestHistoryComponent } from './search-request-history.component';
import { AppModule } from '../app.module';
import { SearchRequestHistoryService } from './search-request-history.service';
import { Engine } from '../models/engine';
import { BaseReponse } from '../models/base-response';
import { SearchRequestResponse } from '../models/search-request-response';
import { BehaviorSubject, Observable } from 'rxjs';

describe('SearchRequestHistoryComponent', () => {
  let searchServiceSpy: Spy<SearchRequestHistoryService>;

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
    searchServiceSpy = createSpyFromClass(SearchRequestHistoryService);
    searchServiceSpy.getAllSearchRequestHistory.and.returnValue(response$);
  };

  beforeEach(async () => {
    configureSpies();
    await MockBuilder(SearchRequestHistoryComponent, AppModule).provide({
      provide: SearchRequestHistoryService,
      useValue: searchServiceSpy,
    });
  });

  const getFixture = (payload: any = {}) =>
    MockRender(SearchRequestHistoryComponent, {
      ...payload,
    });

  it('should create', () => {
    const fixture = getFixture();
    expect(fixture.componentInstance).toBeDefined();
  });

  it('renders app-search-request-history element inside of the SearchRequestHistoryComponent', () => {
    const fixture = getFixture();
    const ganttPopover = ngMocks.find(SearchRequestHistoryComponent);
    expect(ganttPopover.componentInstance).toBeDefined();
  });
});
