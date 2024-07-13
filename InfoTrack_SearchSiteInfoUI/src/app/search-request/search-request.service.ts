import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment.development';
import { SearchRequest } from '../models/search-request';
import { Observable } from 'rxjs';
import { BaseReponse } from '../models/base-response';
import { SearchRequestResponse } from '../models/search-request-response';

@Injectable({
  providedIn: 'root',
})
export class SearchRequestService {
  constructor(private httpClient: HttpClient) {}

  postSearchRequest(
    searchRequest: SearchRequest
  ): Observable<BaseReponse<SearchRequestResponse>> {
    return this.httpClient.post(
      `${environment.apiUrl}/api/Search`,
      searchRequest
    );
  }
}
