import {Component, OnInit} from '@angular/core';
import {User} from './model/User';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'SPATestTaskClient';
  public selectedUser: User = null;

  constructor(
  ) {
  }
  ngOnInit(): void {
    this.onSelectUser(null); // показать все задачи

  }


  // изменение категории
  public onSelectUser(user: User) {

    this.selectedUser = user;

  }
}
