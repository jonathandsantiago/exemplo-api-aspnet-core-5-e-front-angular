import {Component, ElementRef, Inject, inject, InjectionToken, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {combineLatest, Observable, of, Subscription} from 'rxjs';
import {RxStompService} from '@stomp/ng2-stompjs';
import {Message} from '@stomp/stompjs';
import {PaginationResponse, PaginatorPlugin} from '@datorama/akita';
import {ComandaQuery} from '../comanda.query';
import {ComandaService} from '../comanda.service';
import {catchError, distinctUntilChanged, map, share, startWith, switchMap, tap} from 'rxjs/operators';
import {Paginacao} from '../../../helpers/paginacao';
import {MatPaginator} from '@angular/material/paginator';
import {PageEvent} from '@angular/material/paginator/paginator';
import {Comanda} from '../../../models/comanda';
import {MatTableDataSource} from '@angular/material/table';

export const COMANDA_PAGINATOR = new InjectionToken('COMANDA_PAGINATOR', {
  providedIn: 'root',
  factory: () => {
    const query = inject(ComandaQuery);
    return new PaginatorPlugin(query).withControls().withRange();
  }
});

@Component({
  selector: 'app-comanda-garcom',
  templateUrl: './comanda-garcom.component.html',
  styleUrls: ['./comanda-garcom.component.scss']
})
export class ComandaGarcomComponent implements OnInit, OnDestroy {

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  public loading = this.comandaQuery.selectLoading();
  public displayedColumns: string[] = ['id', 'garcom', 'totalAPagar', 'gorjetaGarcom'];
  public dataSource = new MatTableDataSource<Comanda>();

  protected comandaSubscription: Subscription;
  protected itemsPaginated$: Observable<PaginationResponse<Comanda>>;
  protected subscription: Subscription = new Subscription();
  public total = (this.paginatorPlugin.metadata.get('total') || 0);
  public currentPageItems: Comanda[] = [];
  public paginacao = (
    this.paginatorPlugin.metadata.get('paginacao') || {
      pageNumber: 0,
      pageSize: 5
    }) as { pageNumber: number, pageSize: number };

  constructor(@Inject(COMANDA_PAGINATOR) protected paginatorPlugin: PaginatorPlugin<Comanda>,
              private comandaService: ComandaService,
              protected comandaQuery: ComandaQuery,
              private rxStompService: RxStompService) {
  }

  ngOnInit() {
    this.comandaSubscription = this.rxStompService.watch('/queue/teste').subscribe((message: Message) => {
      this.currentPageItems.push(JSON.parse(message.body));
      this.dataSource = new MatTableDataSource<Comanda>(this.currentPageItems);
    });

    const mudancaDePagina$ = this.paginator.page
      .pipe(map(page => page.pageSize),
        startWith(this.paginacao.pageSize),
        distinctUntilChanged());

    this.itemsPaginated$ = combineLatest(this.paginatorPlugin.pageChanges,
      combineLatest(mudancaDePagina$))
      .pipe(switchMap(([pagina, [quantidade]]) => {
          const page = {pageSize: quantidade, pageNumber: pagina} as Paginacao;
          this.paginatorPlugin.metadata.set('paginacao', page);

          return this.comandaService.get(page)
            .pipe(catchError(error => {
              return of({data: [], perPage: 0, total: 0, currentPage: 0} as PaginationResponse<Comanda>);
            }));
        }),
        tap(paginatedResponse => {
          this.paginatorPlugin.metadata.set('total', paginatedResponse.total);
          this.paginacao = this.paginatorPlugin.metadata.get('paginacao');
          this.total = this.paginatorPlugin.metadata.get('total');
        }),
        share(),
      );
    this.changeItensPage();
  }

  changeItensPage() {
    this.subscription.add(this.itemsPaginated$.subscribe(r => {
      this.currentPageItems = r.data;
      this.dataSource = new MatTableDataSource<Comanda>(r.data);
    }));
  }

  changeToPage(pageEvent: PageEvent) {
    this.paginatorPlugin.setPage(pageEvent.pageIndex);
    this.comandaService.setActive([]);
  }

  ngOnDestroy() {
    this.comandaSubscription.unsubscribe();
  }
}
