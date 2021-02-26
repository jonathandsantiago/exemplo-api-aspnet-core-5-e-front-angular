import {Component, Injector, Input, OnInit} from '@angular/core';
import {FormArray, FormBuilder, FormControl, Validators} from '@angular/forms';
import {ToastrService} from 'ngx-toastr';
import {Router} from '@angular/router';
import {CrudComponent} from '../../../components/common/crud.component';
import {ComandaPedidoSituacao, ComandaSituacao} from '../../../models/comanda';
import {ComandaService} from '../../../services/comanda.service';
import {BsModalRef} from 'ngx-bootstrap/modal';
import {Produto} from '../../../models/produto';
import {ProdutoService} from '../../../services/produto.service';
import {Usuario, UsuarioPerfil} from '../../../models/usuario';
import {UsuarioService} from '../../../services/usuario.service';

@Component({
  selector: 'app-comanda-cadastro',
  templateUrl: './comanda-cadastro.component.html',
  styleUrls: ['./comanda-cadastro.component.scss'],
})
export class ComandaCadastroComponent extends CrudComponent implements OnInit {
  @Input() modalRef: BsModalRef;
  id = null;
  pedidosForm = new FormArray([]);
  produtos: Produto[];
  garcons: Usuario[];

  constructor(injector: Injector,
              protected formBuilder: FormBuilder,
              protected toastrService: ToastrService,
              protected produtoService: ProdutoService,
              protected usuarioService: UsuarioService,
              protected router: Router,
              protected comandaService: ComandaService) {
    super(injector);
  }

  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      id: new FormControl(this.id),
      garcomId: new FormControl(null, Validators.required),
      totalAPagar: new FormControl(null),
      gorjetaGarcom: new FormControl(null),
      situacao: new FormControl(ComandaSituacao.Aberta, Validators.required)
    });

    this.subscription.add(this.produtoService.obterTodos().subscribe((produtos: any) => this.produtos = produtos));
    this.subscription.add(this.usuarioService.obterTodosPorPerfil(UsuarioPerfil.Garcon)
      .subscribe((garcons: any) => this.garcons = garcons));
    this.adicionarPedido();
  }

  onSubmit() {
    this.submitted = true;
    const comanda = this.formGroup.value;

    if (this.formGroup.invalid && this.pedidosForm.invalid) {
      return;
    }

    this.subscription.add(this.comandaService.inserirOuEditar(comanda, {id: this.id}).subscribe(response => {
      this.toastrService.success(`Comanda registrado com sucesso.`, null, {
        positionClass: 'toast-bottom-right',
        disableTimeOut: false,
        progressBar: true
      });
    }, error => this.error = error));
  }

  adicionarPedido() {
    this.pedidosForm.push(this.formBuilder.group({
      comandaId: new FormControl(null),
      produtoId: new FormControl(null),
      quantidade: new FormControl(0),
      situacao: new FormControl(ComandaPedidoSituacao.Pedido),
    }));
  }

  removerPedido(index: number) {
    this.pedidosForm.removeAt(index);
  }
}
