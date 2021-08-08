import {Component, Injector, OnInit} from '@angular/core';
import {FormBuilder, FormControl, Validators} from '@angular/forms';
import {ToastrService} from 'ngx-toastr';
import {Router} from '@angular/router';
import {EditBaseComponent} from '../../../components/common/edit-base.component';
import {ProdutoService} from '../../../services/produto.service';
import {Location} from '@angular/common';

@Component({
  selector: 'app-produto-crud',
  templateUrl: './produto-crud.component.html',
  styleUrls: ['./produto-crud.component.scss'],
})
export class ProdutoCrudComponent extends EditBaseComponent implements OnInit {

  constructor(injector: Injector,
              protected formBuilder: FormBuilder,
              protected toastrService: ToastrService,
              protected router: Router,
              protected location: Location,
              protected produtoService: ProdutoService) {
    super(injector);
  }

  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      id: new FormControl(null),
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
      if (params.id == null || params.id === '') {
        return;
      }

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

    const action = this.isEdicao ?
      this.produtoService.editar(produto, {id: this.id}) :
      this.produtoService.cadastrar(produto, {id: this.id});

    this.subscription.add(action.subscribe(response => {
      this.toastrService.success(`Produto ${this.id > 0 ? 'atualizado' : 'registrado'} com sucesso.`, null, {
        positionClass: 'toast-bottom-right',
        disableTimeOut: false,
        progressBar: true
      });

      this.location.back();
    }, error => this.error = error));
  }
}
