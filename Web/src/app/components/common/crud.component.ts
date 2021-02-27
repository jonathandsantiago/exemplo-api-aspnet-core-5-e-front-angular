import {FormGroup} from '@angular/forms';
import {merge, Subscription} from 'rxjs';
import {Location} from '@angular/common';
import {filter, map} from 'rxjs/operators';
import {ActivatedRoute, NavigationEnd, Router} from '@angular/router';
import {Injector, Input, OnDestroy} from '@angular/core';
import {BsLocaleService} from 'ngx-bootstrap/datepicker';

export abstract class CrudComponent implements OnDestroy {
  @Input() isVisualizacao: boolean;
  @Input() isEdicao: boolean;

  formGroup: FormGroup;
  submitted = false;
  error = '';
  id = null;
  nagivate$ = null;

  subscription: Subscription = new Subscription();

  get formControl() {
    return this.formGroup.controls;
  }

  protected router: Router;
  protected activatedRoute: ActivatedRoute;
  protected location: Location;
  protected localeService: BsLocaleService;

  constructor(injector: Injector) {
    this.router = injector.get(Router);
    this.activatedRoute = injector.get(ActivatedRoute);
    this.location = injector.get(Location);
    this.localeService = injector.get(BsLocaleService);

    this.localeService.use('pt-br');
    this.isEdicao = this.router.url.includes('/editar');
    this.isVisualizacao = this.router.url.includes('/visualizar');

    const nagivate = this.router.events.pipe(filter(event => event instanceof NavigationEnd));
    const paramsRoute = this.activatedRoute.params;
    this.nagivate$ = merge(nagivate, paramsRoute).pipe(map(() => {
      let child = this.activatedRoute.firstChild;
      while (child) {
        if (child.firstChild) {
          child = child.firstChild;
        } else if (child.snapshot.data) {
          return child.snapshot.data;
        } else {
          return null;
        }
      }
      return null;
    }));
  }

  abstract onSubmit();

  voltar() {
    this.location.back();
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
