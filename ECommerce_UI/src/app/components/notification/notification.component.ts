import { Component, Inject } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import {
  MAT_DIALOG_DATA,
  MatDialogModule,
  MatDialogRef,
} from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css'],
  standalone: true,
  imports: [MatCardModule, MatDialogModule, MatButtonModule],
})
export class NotificationComponent {
  protected mensaje!: string;

  constructor(
    private dialogRef: MatDialogRef<NotificationComponent>,
    @Inject(MAT_DIALOG_DATA) private data: any
  ) {
    if (data && data.mensaje) {
      this.mensaje = data.mensaje;
    }
  }

  public showExitoso(mensaje: string): void {
    this.mensaje = mensaje;
  }

  public closeNotification(): void {
    this.dialogRef.close();
  }

  public showFallido(mensaje: string): void {}
}
