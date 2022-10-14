import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-modals',
  templateUrl: './modal-confirm.component.html',
  styleUrls: ['./modal-confirm.component.scss']
})
export class ModalConfirmComponent {

  description: string;
  title: string;

  constructor(
    private dialogRef: MatDialogRef<ModalConfirmComponent>,
    @Inject(MAT_DIALOG_DATA) data) {
    this.description = data.description;
    this.title = data.title;
  }

  confirm() {
    this.dialogRef.close(true);
  }

  deny() {
    this.dialogRef.close(false);
  }
}
