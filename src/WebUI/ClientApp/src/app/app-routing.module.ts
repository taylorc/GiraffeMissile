import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthorizeGuard } from './core/guard/authorize.guard';
import { CounterComponent } from './module/counter/counter.component';

export const routes: Routes = [

  { path: 'counter', component: CounterComponent },
  { path: '', loadChildren: ()=>import('./module/home/home.module').then(x=>x.HomeModule) },
  { path: 'todo', loadChildren: ()=>import('./module/todo/todo.module').then(x=>x.TodoModule), canActivate: [AuthorizeGuard] },
  { path: 'token', loadChildren: ()=>import('./module/token/token.module').then(x=>x.TokenModule), canActivate: [AuthorizeGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
