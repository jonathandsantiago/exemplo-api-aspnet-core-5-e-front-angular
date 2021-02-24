import {Component, Injector, OnInit} from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {ToastrService} from 'ngx-toastr';
import {Router} from '@angular/router';
import {Location} from '@angular/common';
import {CrudComponent} from '../../../components/common/crud.component';
import {ProdutoService} from '../../../services/produto.service';

@Component({
  selector: 'app-produto-edicao',
  templateUrl: './produto-edicao.component.html',
  styleUrls: ['./produto-edicao.component.scss']
})
export class ProdutoEdicaoComponent extends CrudComponent implements OnInit {
  id = null;

  constructor(injector: Injector,
              protected formBuilder: FormBuilder,
              protected toastrService: ToastrService,
              protected produtoService: ProdutoService,
              protected router: Router,
              protected location: Location) {
    super(injector);
  }

  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      id: [null],
      nome: ['', Validators.required],
      preco: [null],
      ulrImage: [null],
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
      this.produtoService.obterPorId(this.id).subscribe((produto) => {
        this.formGroup.reset(produto);
      });
    }));
  }

  onSubmit() {
    this.submitted = true;

    if (this.formGroup.invalid) {
      return;
    }

    const produto = this.formGroup.value;

    this.subscription.add(this.produtoService.editar(produto, {id: this.id}).subscribe(
      produtoEditado => {
        this.toastrService.success(`Produto atualizado com sucesso.`, null, {
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
