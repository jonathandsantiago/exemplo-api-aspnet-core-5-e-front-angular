import {Component, Input} from '@angular/core';
import {
  FormGroup,
  FormControl,
  Validators,
} from '@angular/forms';
import {Comanda} from '../../../models/comanda';
import {ActionView, EditViewBase} from '../../../shared/edit-view-base';
import {IService} from '../../../shared/interfaces';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
  selector: 'app-comanda-form',
  templateUrl: './comanda-edit.component.html',
  styleUrls: ['./comanda-edit.component.scss']
})
export class ComandaEditComponent extends EditViewBase<Comanda, number> {

  public form: FormGroup = null;
  @Input() public item: Comanda;

  constructor(
    protected service: IService<Comanda, number>,
    protected router: Router,
    protected actionView: ActionView,
    protected route: ActivatedRoute) {
    super(service, router, actionView, route);
  }

  onInit() {
    this.form = new FormGroup({
      id: new FormControl(''),
      garcom: new FormControl(''),
      pedidos: new FormGroup({
        id: new FormControl(''),
        comanda: new FormControl('', [Validators.required]),
        produto: new FormControl('', [Validators.required]),
        quantidade: new FormControl('', [Validators.required]),
        situacao: new FormControl('', [Validators.required]),
      }),
      totalAPagar: new FormControl(''),
      gorjetaGarcom: new FormControl(''),
    });
  }

  protected AfterSave() {
  }
}
