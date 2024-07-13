import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { BaseReponse } from '../models/base-response';
import { SearchRequestResponse } from '../models/search-request-response';

@Injectable({
  providedIn: 'root',
})
export class SearchRequestHistoryService {
  constructor(private httpClient: HttpClient) {}

  getAllSearchRequestHistory(): Observable<
    BaseReponse<Array<SearchRequestResponse>>
  > {
    return this.httpClient.get(`${environment.apiUrl}/api/History/getAll`);
  }
}
