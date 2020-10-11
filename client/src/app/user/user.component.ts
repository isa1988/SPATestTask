import {Component, Input, OnInit, Output} from '@angular/core';
import {User} from '../model/User';
import {EventEmitter} from 'events';
import {IUser, UserService} from './user.service';
import {MatDialog} from '@angular/material/dialog';
import {UserDialogComponent} from '../dialog/user-dialog/user-dialog.component';
import {HttpParams} from '@angular/common/http';
import {ConfirmDialogComponent} from "../dialog/confirm-dialog/confirm-dialog.component";

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {

  private displayedColumns: string[] = ['id', 'userName'];

  @Input()
  selectedUser: User;

  @Output()
  updateUser = new EventEmitter<User>();
  users: IUser[] = [];
  // для отображения иконки редактирования при наведении на категорию
  private indexMouseMove: number;
  constructor(
    private service: UserService,
    private dialog: MatDialog // работа с диалоговым окном
              ) { }

  ngOnInit(): void {
    this.showAllUser();
  }


  private showAllUser(){
    this.service.getAll()
      .subscribe(users =>{
        console.log('Response', users)
        this.users = users;
      }, error => console.log(error.message));
  }

  // сохраняет индекс записи пользователя, над который в данный момент проходит мышка (и там отображается иконка редактирования)
  private showEditIcon(index: number) {
    this.indexMouseMove = index;
  }


  // диалоговое окно для создания пользователя
  public openCreateDialog() : void {
    this.editCreate(new User( null, ''), true);
  }

  // диалоговое окно для создания пользователя
  public openEditDialog(user: User) : void{
    this.editCreate(user, false);
  }

  private editCreate(user: User, isNew: boolean) : void{
    // открытие диалогового окна
    let titleForm : string = (isNew) ? 'Создание пользователя' :  'Редактирование пользователя';
    const dialogRef = this.dialog.open(UserDialogComponent, {
      data: [user, titleForm],
      width: '400px',
      panelClass: 'mystyle-dialog',
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result as User) { // если нажали ОК и есть результат
        user.userName = result.userName;
        if (isNew)
        {
          this.service.createUser(user)
            .subscribe(userN =>{
              this.showAllUser();
            }, error => console.log(error.message));
        }
        else {
          this.service.editUser(user)
            .subscribe(userN =>{
            }, error => console.log(error.message));
        }

        return;
      }

    });
    console.log(user.userName);
  }

  private openDeleteDialog(user: User, ) : void{
    // открытие диалогового окна

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: ["Удаление пользователя", `ФИО ${user.userName}`],
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result as User) { // если нажали ОК и есть результат
          this.service.deleteUser(user.id)
            .subscribe(userN =>{
              this.showAllUser();
            }, error => console.log(error.message));
        return;
      }

    });
    console.log(user.userName);
  }

}
