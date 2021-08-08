import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-paginacao',
  templateUrl: './paginacao.component.html',
  styleUrls: ['./paginacao.component.scss']
})
export class PaginacaoComponent implements OnInit {
  @Input() totalItensPagina: number;
  @Input() totalItens: number;
  @Input() totalPorPagina: number;
  @Input() display: string;
  @Input() classDisplay: string;
  @Output() pageChanged = new EventEmitter();

  constructor() {
  }

  ngOnInit(): void {
  }

  mudarPagina(event) {
    this.pageChanged.emit(event);
  }
}
