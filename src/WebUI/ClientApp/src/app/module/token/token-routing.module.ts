import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TokenComponent } from './page/token.component';

export const routes: Routes = [
  {
    path: '',
    component: TokenComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TokenRoutingModule {}