import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar.component';
import { RouterModule, RouterOutlet } from '@angular/router';

@NgModule({
  declarations: [NavBarComponent],
  imports: [CommonModule, RouterOutlet, RouterModule],
  exports: [NavBarComponent],
})
export class NavBarModule {}
