import {Component, Injector, OnInit} from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {ToastrService} from 'ngx-toastr';
import {Router} from '@angular/router';
import {Location} from '@angular/common';
import {CrudComponent} from '../../../components/common/crud.component';
import {ComandaSituacao} from '../../../models/comanda';
import {ComandaService} from '../../../services/comanda.service';

@Component({
  selector: 'app-comanda-edicao',
  templateUrl: './comanda-edicao.component.html',
  styleUrls: ['./comanda-edicao.component.scss']
})
export class ComandaEdicaoComponent extends CrudComponent implements OnInit {
  id = null;

  constructor(injector: Injector,
              protected formBuilder: FormBuilder,
              protected toastrService: ToastrService,
              protected comandaService: ComandaService,
              protected router: Router,
              protected location: Location) {
    super(injector);
  }

  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      id: [null],
      garcomId: ['', Validators.required],
      totalAPagar: [null],
      gorjetaGarcom: [null],
      situacao: [ComandaSituacao.Aberta],
    });

    this.subscription.add(this.nagivate$.subscribe(customData => {
      let route = this.router.routerState.root.snapshot;
      while (route.firstChild != null) {
        route = route.firstChild;
      }

      const params = route.params;
      const data = route.data;

      if (params.id == null || params.id === '') {
        return;
      }

      this.isVisualizacao = data.isVisualizacao;
      this.id = params.id;
      this.comandaService.obterPorId(this.id).subscribe((comanda) => {
        this.formGroup.reset(comanda);
      });
    }));
  }

  onSubmit() {
    this.submitted = true;

    if (this.formGroup.invalid) {
      return;
    }

    const comanda = this.formGroup.value;

    this.subscription.add(this.comandaService.editar(comanda, {id: this.id}).subscribe(
      comandaEditado => {
        this.toastrService.success(`Comanda atualizado com sucesso.`, null, {
          positionClass: 'toast-bottom-right',
          disableTimeOut: false,
          progressBar: true
        });

        this.router.navigate(['/']).then();
      },
      error => {
        this.error = error;
        this.toastrService.error(error, `Ocorreu o seguinte erro ao salvar.`, {
          positionClass: 'toast-bottom-right',
          disableTimeOut: false,
          enableHtml: true,
        });
      }));
  }

  voltar() {
    this.location.back();
  }
}
