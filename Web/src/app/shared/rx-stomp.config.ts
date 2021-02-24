import {InjectableRxStompConfig} from '@stomp/ng2-stompjs';

export const rxStompConfig: InjectableRxStompConfig = {
  brokerURL: 'ws://127.0.0.1:15674/ws',
  connectHeaders: {
    login: 'guest',
    passcode: 'guest'
  },
  heartbeatIncoming: 0,
  heartbeatOutgoing: 20000,
  reconnectDelay: 200,
  debug: (msg: string): void => {
    console.log(new Date(), msg);
  }
};
