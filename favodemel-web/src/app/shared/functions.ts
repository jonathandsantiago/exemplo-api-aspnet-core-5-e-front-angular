import * as moment from 'moment';
import {HttpResponse} from '@angular/common/http';

export function formatarNumero(valor:any) {
  if (`${valor}`.length === 1) {
    return `${valor}`.replace(/([0-9]{1})$/g, '0$1');
  }
  return valor;
}

export function somarHr(dias, horas) {
  return formatarNumero((dias * 24) + horas);
}

export function downloadCSV(data, columns, filename = 'data') {
  const csvData = convertToCSV(data, columns);
  const blob = new Blob(['\ufeff' + csvData], {type: 'text/csv;charset=utf-8;'});
  const dwldLink = document.createElement('a');
  const url = URL.createObjectURL(blob);
  const isSafariBrowser = navigator.userAgent.indexOf('Safari') !== -1 && navigator.userAgent.indexOf('Chrome') === -1;

  if (isSafariBrowser) {
    dwldLink.setAttribute('target', '_blank');
  }

  dwldLink.setAttribute('href', url);
  dwldLink.setAttribute('download', filename + '.csv');
  dwldLink.style.visibility = 'hidden';
  document.body.appendChild(dwldLink);
  dwldLink.click();
  document.body.removeChild(dwldLink);
}

export function convertToCSV(objArray, headerList) {
  const array = typeof objArray !== 'object' ? JSON.parse(objArray) : objArray;
  let str = '';
  let row = '';

  // tslint:disable-next-line:forin
  for (const index in headerList) {
    row += headerList[index] + ',';
  }

  row = row.slice(0, -1);
  str += row + '\r\n';
  // tslint:disable-next-line:prefer-for-of
  for (let i = 0; i < array.length; i++) {
    let line = '';

    // tslint:disable-next-line:forin
    for (const index in headerList) {
      const head = headerList[index];
      line += (Number(index) === 0 ? '' : ',') + array[i][head];
    }

    str += line + '\r\n';
  }

  return str;
}

export function download(content, nome) {
  const a = document.createElement('a');
  document.body.appendChild(a);
  const fileURL = window.URL.createObjectURL(content);
  a.href = fileURL;
  a.download = nome;
  a.click();
  document.body.removeChild(a);
}

export function convertDateTimeString(data, zerarSegundos = false): string {
  if (!data) {
    return null;
  }

  if (typeof data === 'string') {
    return data;
  }

  if (zerarSegundos) {
    return data ? moment(data).format('YYYY-MM-DDTHH:mm:00.000') : '';
  }

  return data ? moment(data).format('YYYY-MM-DDTHH:mm:ss.000') : '';
}

export function formatarDataHora(data, horas) {
  if (!data || !horas) {
    return null;
  }
  const dataHoraFormatada = `${moment(data).format('YYYY-MM-DD')}T${moment(horas).format('HH:mm:ss.000')}`;
  return moment(dataHoraFormatada).toDate();
}

export function convertDateString(data): string {
  if (!data) {
    return null;
  }

  if (typeof data === 'string') {
    return data;
  }

  return data ? moment(data).format('YYYY-MM-DDT00:00:00.000') : '';
}

export function convertToBoolean(valor): boolean {
  if (typeof valor === 'string') {
    return valor === 'true' ? true : false;
  }

  return Boolean(valor);
}

export function convertToDecimal(valor) {
  if (!valor) {
    return null;
  }

  return parseFloat(valor);
}

export function convertToInt(valor, valuedefault = null): number {
  if (!valor) {
    return valuedefault;
  }

  return Number(valor);
}

export function convertTimeSpan(horas) {
  if (!horas) {
    return '00:00:00';
  }
  return moment(horas).format('HH:mm:ss');
}

export function obterPrimeiroDiaMes(mes: any = null) {
  if (mes) {
    return new Date(moment().year(), mes, 1);
  }
  return new Date(moment().year(), moment().month(), 1);
}

export function obterUltimoDiaMes(mes: any = null) {
  if (mes) {
    return new Date(moment().year(), mes, 0);
  }
  return new Date(moment().year(), moment().month() + 1, 0);
}

export function getFileNameBlob(response: HttpResponse<Blob>, name) {
  let filename: string;
  try {
    const contentDisposition: string = response.headers.get('content-disposition');
    const filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
    filename = filenameRegex.exec(contentDisposition)[1];
  } catch (e) {
    filename = name;
  }
  return filename;
}

export function formatCnpj(cnpj) {
  if (!cnpj) {
    return '';
  }

  const x = cnpj.replace(/\D/g, '').match(/(\d{0,2})(\d{0,3})(\d{0,3})(\d{0,4})(\d{0,2})/);
  return !x[2] ? x[1] : x[1] + '.' + x[2] + '.' + x[3] + '/' + x[4] + (x[5] ? '-' + x[5] : '');
}

export function formatCpf(cpf) {
  if (!cpf) {
    return '';
  }

  cpf = cpf.replace(/[^\d]/g, '');
  return cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');
}

export function formatDecimal(value: any) {
  let result = `${value}`;

  if (!result || result === '0' || result === '0,00') {
    return '0,00';
  }

  result = result.replace('.', '').replace(',', '');

  while (result.startsWith('0') || result.startsWith('.')) {
    result = result.substring(1, result.length);
  }

  if (result.length === 1) {
    return result.replace(/([0-9]{1})$/g, '0,0$1');
  } else if (result.length === 2) {
    return result.replace(/([0-9]{2})$/g, '0,$1');
  } else if (result.length > 6) {
    return result.replace(/([0-9]{3})([0-9]{2}$)/g, '.$1,$2');
  }

  return result.replace(/([0-9]{2})$/g, ',$1');
}
