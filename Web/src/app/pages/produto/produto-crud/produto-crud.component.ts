import {Component, Injector, OnInit} from '@angular/core';
import {FormBuilder, FormControl, Validators} from '@angular/forms';
import {ToastrService} from 'ngx-toastr';
import {Router} from '@angular/router';
import {CrudComponent} from '../../../components/common/crud.component';
import {ProdutoService} from '../../../services/produto.service';

@Component({
  selector: 'app-produto-cadastro',
  templateUrl: './produto-cadastro.component.html',
  styleUrls: ['./produto-cadastro.component.scss'],
})
export class ProdutoCadastroComponent extends CrudComponent implements OnInit {
  id = null;

  constructor(injector: Injector,
              protected formBuilder: FormBuilder,
              protected toastrService: ToastrService,
              protected router: Router,
              protected produtoService: ProdutoService) {
    super(injector);
  }

  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      nome: new FormControl(null, Validators.required),
      preco: new FormControl(null, Validators.required),
      ulrImage: new FormControl(null),
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
    const produto = this.formGroup.value;

    if (this.formGroup.invalid) {
      return;
    }

    this.subscription.add(this.produtoService.cadastrar(produto, {id: this.id}).subscribe(response => {
      this.toastrService.success(`Produto registrado com sucesso.`, null, {
        positionClass: 'toast-bottom-right',
        disableTimeOut: false,
        progressBar: true
      });
    }, error => this.error = error));
  }
}
