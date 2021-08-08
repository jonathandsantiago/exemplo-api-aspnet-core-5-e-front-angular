import {BehaviorSubject} from "rxjs";
import {Injectable} from "@angular/core";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class WebSocketService {
  private socket: WebSocket;
  squares$: BehaviorSubject<any[]> = new BehaviorSubject<any[]>([]);
  announcement$: BehaviorSubject<string> = new BehaviorSubject<string>('');
  name$: BehaviorSubject<string> = new BehaviorSubject<string>('');
  private name: string;

  constructor() {
  }

  startSocket() {
    this.socket = new WebSocket(`wss://${environment.webSocket}`);
    this.socket.addEventListener("open", (ev => {
      console.log('opened')
    }));
    this.socket.addEventListener("message", (ev => {
      console.log(ev);
      const messageBox = JSON.parse(ev.data);
      console.log('message object', messageBox);
      switch (messageBox.MessageType) {
        case "name":
          this.name = messageBox.Payload;
          this.name$.next(this.name);
          break;
        case "announce":
          this.announcement$.next(messageBox.Payload);
          break;
        case "squares":
          this.squares$.next(messageBox.Payload);
          break;
        default:
          break;
      }
    }));
  }

  sendSquareChangeRequest(req: any) {
    req.Name = this.name;
    const requestAsJson = JSON.stringify(req);
    this.socket.send(requestAsJson);
  }
}
