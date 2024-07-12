import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SearchRequestService } from './search-request.service';
import { SearchRequest } from '../models/search-request';
import { Engine } from '../models/engine';
import { REG_EX } from '../constants/constants';
import { SearchRequestResponse } from '../models/search-request-response';

@Component({
  selector: 'app-search-request',
  templateUrl: './search-request.component.html',
  styleUrl: './search-request.component.scss',
})
export class SearchRequestComponent implements OnInit {
  public searchRequestForm!: FormGroup;
  public engines = [Engine.GOOGLE, Engine.BING, Engine.YAHOO];
  public result: SearchRequestResponse | undefined;
  public loading: boolean = false;
  public loadingValue = 'Loading...';

  constructor(
    private searchRequestService: SearchRequestService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.searchRequestForm = this.formBuilder.group({
      keywords: ['', [Validators.required, Validators.maxLength(200)]],
      url: ['', [Validators.required, Validators.pattern(REG_EX)]],
      engines: [Engine.GOOGLE, Validators.required],
    });
  }

  onSubmit() {
    this.loadingValue = 'Loading...';
    this.loading = true;

    const searchRequest: SearchRequest = {
      keywords: this.searchRequestForm.value.keywords,
      url: this.searchRequestForm.value.url,
      engine: this.searchRequestForm.value.engines,
    };

    this.searchRequestService.postSearchRequest(searchRequest).subscribe({
      next: (data) => {
        this.result = data.data;
        this.loading = false;
      },
      error: (error) => {
        this.loadingValue = `Error getting post: ${error}`;
        this.loading = true;
      },
    });
  }
}
