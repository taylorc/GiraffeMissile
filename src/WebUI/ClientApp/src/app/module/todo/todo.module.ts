import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TodoComponent } from './page/todo.component';
import { TodoRoutingModule } from './todo-routing.module';

@NgModule({
  declarations: [
    TodoComponent
  ],
  imports: [CommonModule, TodoRoutingModule, FontAwesomeModule, FormsModule, ModalModule.forRoot()],
  exports: [],
  providers: []
})
export class TodoModule {}