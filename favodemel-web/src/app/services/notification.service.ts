import {EventEmitter, Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  notificationMessage = new EventEmitter();

  constructor() {
  }

}
