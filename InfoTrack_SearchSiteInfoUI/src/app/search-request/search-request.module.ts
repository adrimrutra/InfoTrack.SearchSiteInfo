import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SearchRequestComponent } from './search-request.component';
import { SearchRequestService } from './search-request.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [SearchRequestComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports: [SearchRequestComponent],
  providers: [SearchRequestService],
})
export class SearchRequestModule {}
