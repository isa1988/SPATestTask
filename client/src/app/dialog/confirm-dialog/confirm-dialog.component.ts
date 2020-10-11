import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';

@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
  styleUrls: ['./confirm-dialog.component.scss']
})

// диалоговое окно подтверждения действия
export class ConfirmDialogComponent implements OnInit {
  public dialogTitle: string;
  public message: string;

  constructor(
    private dialogRef: MatDialogRef<ConfirmDialogComponent>, // для работы с текущим диалог. окном
    @Inject(MAT_DIALOG_DATA) private data: { dialogTitle: string, message: string } // данные, которые передали в диалоговое окно
  ) {

  }

  ngOnInit() {
    this.dialogTitle = this.data[0]; // заголовок
    this.message = this.data[1]; // сообщение
  }

  // нажали ОК
  public onConfirm(): void {
    this.dialogRef.close(true);
  }

  // нажали отмену
  public onCancel(): void {
    this.dialogRef.close(false);
  }
}

