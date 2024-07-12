import { Component, OnInit } from '@angular/core';
import { SearchRequestHistoryService } from './search-request-history.service';
import { SearchRequestResponse } from '../models/search-request-response';
import moment from 'moment';

@Component({
  selector: 'app-search-request-history',
  templateUrl: './search-request-history.component.html',
  styleUrl: './search-request-history.component.scss',
})
export class SearchRequestHistoryComponent implements OnInit {
  public searchRequests: Array<SearchRequestResponse> | undefined;

  constructor(
    private searchRequestHistoryService: SearchRequestHistoryService
  ) {}

  ngOnInit(): void {
    this.searchRequestHistoryService.getAllSearchRequestHistory().subscribe({
      next: (data) => {
        this.searchRequests = data.data;
      },
      error: (error) => {
        console.error('Error getting post:', error);
      },
    });
  }
}
