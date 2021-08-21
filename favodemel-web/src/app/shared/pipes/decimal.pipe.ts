import {Injectable, Pipe, PipeTransform} from '@angular/core';

@Pipe({
  name: 'DECIMAL',
  pure: false
})
@Injectable()
export class DecimalPipe implements PipeTransform {
  transform(value: any, currency: boolean = false, ...args: any[]): any {

    if (value) {
      return currency ? value.toLocaleString('pt-br', {style: 'currency', currency: 'BRL'}) :
        value.toLocaleString('pt-br', {minimumFractionDigits: 2});
    }

    return 0;
  }
}
