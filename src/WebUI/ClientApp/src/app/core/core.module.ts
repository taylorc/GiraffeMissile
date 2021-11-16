import { NgModule, Optional, SkipSelf } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthorizeGuard } from './guard/authorize.guard';
import { AuthorizeInterceptor } from './interceptor/authorize.interceptor';
import { throwIfAlreadyLoaded } from './guard/module-import.guard';

@NgModule({
  imports: [HttpClientModule],
  providers: [
    AuthorizeGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthorizeInterceptor,
      multi: true
    }
  ]
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    throwIfAlreadyLoaded(parentModule, 'CoreModule');
  }
}