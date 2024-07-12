import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SearchRequestHistoryComponent } from './search-request-history.component';
import { SearchRequestHistoryService } from './search-request-history.service';
import { PipesModule } from '../pipes/pipes.module';

@NgModule({
  declarations: [SearchRequestHistoryComponent],
  imports: [CommonModule, PipesModule],
  exports: [SearchRequestHistoryComponent],
  providers: [SearchRequestHistoryService],
})
export class SearchRequestHistoryModule {}
