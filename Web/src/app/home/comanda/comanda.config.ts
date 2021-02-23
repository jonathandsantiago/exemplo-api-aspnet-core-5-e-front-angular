import {InjectionToken} from '@angular/core';
import {environment} from '../../../environments/environment';

export interface ComandaConfig {
    api: string;
  }

export const COMANDA_CONFIG = new InjectionToken<ComandaConfig>('COMANDA_CONFIG',
  {
    providedIn: 'root',
    factory: () => {
        return {
            api: `${environment.apiUrl}/comanda`
        };
    }
  }
);
