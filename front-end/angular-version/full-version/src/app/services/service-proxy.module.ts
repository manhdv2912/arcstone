import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BackendProxy } from './backend.service';
@NgModule({
    imports: [
        BackendProxy
    ],
    providers: [
    //   { provide: HTTP_INTERCEPTORS, useClass: CustomHttpInterceptor, multi: true }
    ]
  })
  export class ServiceProxyModule { }