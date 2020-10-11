import {Component, Inject, OnInit} from '@angular/core';
import {User} from '../../model/User';
import {UserService} from '../../user/user.service';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";

@Component({
  selector: 'app-user-dialog',
  templateUrl: './user-dialog.component.html',
  styleUrls: ['./user-dialog.component.scss']
})

export class UserDialogComponent implements OnInit {

  public dialogTitle: string; // заголовок окна
  private user: User; // пользователь для редактирования/создания

  // сохраняем все значения в отдельные переменные
  // чтобы изменения не сказывались на самом пользователе и можно было отменить изменения
  public userName: string;

  constructor(
    private dialogRef: MatDialogRef<UserDialogComponent>, // // для возможности работы с текущим диалог. окном
    @Inject(MAT_DIALOG_DATA) private data: [User, string], // данные, которые передали в диалоговое окно
    private dataHandler: UserService // ссылка на сервис для работы с данными
  ) {
  }

  ngOnInit() {
    this.user = this.data[0]; // задача для редактирования/создания
    this.dialogTitle = this.data[1]; // текст для диалогового окна


    // инициализация начальных значений (записывам в отдельные переменные
    // чтобы можно было отменить изменения, а то будут сразу записываться в пользователя)
    this.userName = this.user.userName;
  }

  // нажали ОК
  public onConfirm(): void {

    // считываем все значения для сохранения в поля пользователя
    this.user.userName = this.userName;


    // передаем добавленную/измененную задачу в обработчик
    // что с ним будут делать - уже на задача этого компонента
    this.dialogRef.close(this.user);

  }

  // нажали отмену (ничего не сохраняем и закрываем окно)
  public onCancel(): void {
    this.dialogRef.close(null);
  }
}
