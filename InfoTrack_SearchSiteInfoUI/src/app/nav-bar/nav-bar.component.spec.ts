import { ComponentFixture } from '@angular/core/testing';
import { MockBuilder, MockRender, ngMocks } from 'ng-mocks';

import { NavBarComponent } from './nav-bar.component';
import { AppModule } from '../app.module';

describe('NavBarComponent', () => {
  let component: NavBarComponent;
  let fixture: ComponentFixture<NavBarComponent>;

  const configureSpies = () => {};

  beforeEach(async () => {
    configureSpies();
    await MockBuilder(NavBarComponent, AppModule);
  });

  const getFixture = (payload: any = {}) =>
    MockRender(NavBarComponent, {
      ...payload,
    });

    it('should create', () => {
      const fixture = getFixture();
      expect(fixture.componentInstance).toBeDefined();
    });

    it('renders app-nav-bar element inside of the NavBarComponent', () => {
      const fixture = getFixture();
      const ganttPopover = ngMocks.find(NavBarComponent);
      expect(ganttPopover.componentInstance).toBeDefined();
    });
});

