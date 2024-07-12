import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SearchRequestComponent } from './search-request/search-request.component';
import { SearchRequestHistoryComponent } from './search-request-history/search-request-history.component';

const routes: Routes = [
  { path: '', component: SearchRequestComponent },
  { path: 'history', component: SearchRequestHistoryComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
