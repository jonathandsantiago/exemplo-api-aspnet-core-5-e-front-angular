// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  apiUrl: 'https://localhost:44300',
  mensageriaConfig: {
    brokerURL: 'ws://127.0.0.1:15674/ws',
    connectHeaders: {
      login: 'guest',
      passcode: 'guest'
    },
    heartbeatIncoming: 0,
    heartbeatOutgoing: 20000,
    reconnectDelay: 200
  },
};
