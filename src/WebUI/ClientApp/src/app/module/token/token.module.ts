import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { TokenComponent } from './page/token.component';
import { TokenRoutingModule } from './token-routing.module';

@NgModule({
  declarations: [
    TokenComponent
  ],
  imports: [CommonModule,TokenRoutingModule],
  exports: [],
  providers: []
})
export class TokenModule {}