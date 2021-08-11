import {Component, EventEmitter, Injector, Input, OnInit, Output} from '@angular/core';
import {FormArray, FormBuilder, FormControl, Validators} from '@angular/forms';
import {ToastrService} from 'ngx-toastr';
import {Router} from '@angular/router';
import {EditBaseComponent} from '../../../components/common/edit-base.component';
import {ComandaPedidoSituacao, ComandaSituacao} from '../../../models/comanda';
import {ComandaService} from '../../../services/comanda.service';
import {BsModalRef} from 'ngx-bootstrap/modal';
import {Produto} from '../../../models/produto';
import {ProdutoService} from '../../../services/produto.service';
import {Usuario, UsuarioPerfil} from '../../../models/usuario';
import {UsuarioService} from '../../../services/usuario.service';

@Component({
  selector: 'app-comanda-edit',
  templateUrl: './comanda-edit.component.html',
  styleUrls: ['./comanda-edit.component.scss'],
})
export class ComandaEditComponent extends EditBaseComponent implements OnInit {
  @Input() modalRef: BsModalRef;
  @Output() callbackEdicao: EventEmitter<any> = new EventEmitter<any>();
  @Output() callbackCadastro: EventEmitter<any> = new EventEmitter<any>();
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
      id: new FormControl(null),
      garcomId: new FormControl(null),
      codigo: new FormControl(null),
      totalAPagar: new FormControl(0),
      gorjetaGarcom: new FormControl(0),
      situacao: new FormControl(ComandaSituacao.Aberta, Validators.required)
    });

    if (this.modalRef.content) {
      this.id = this.modalRef.content.id;
      this.isEdicao = this.modalRef.content.isEdicao;
      this.isVisualizacao = this.modalRef.content.isVisualizacao;
    }

    this.subscription.add(this.produtoService.obterTodos().subscribe((produtos: any) => this.produtos = produtos));
    this.subscription.add(this.usuarioService.obterTodosPorPerfil(UsuarioPerfil.Garcon)
      .subscribe((garcons: any) => this.garcons = garcons));

    if (this.id) {
      this.comandaService.obterPorId(this.id).subscribe((comanda) => {
        this.formGroup.reset(comanda);
        comanda.pedidos.map(pedido => {
          const formPedido = this.formControlPedido();
          formPedido.reset(pedido);
          this.pedidosForm.push(formPedido);
        });
      });
    } else {
      this.iniciarPedido();
    }
  }

  onSubmit() {
    this.submitted = true;
    const comanda = this.formGroup.value;

    if (this.formGroup.invalid || this.pedidosForm.invalid) {
      return;
    }

    const pedidos = this.pedidosForm.value;

    const action = this.isEdicao ?
      this.comandaService.editar(comanda, {pedidos, id: this.id}) :
      this.comandaService.cadastrar(comanda, {pedidos, id: this.id});

    this.subscription.add(action.subscribe(response => {
      this.toastrService.success(`Comanda ${this.id > 0 ? 'atualizada' : 'registrada'} com sucesso.`, null, {
        positionClass: 'toast-bottom-right',
        disableTimeOut: false,
        progressBar: true
      });
      this.modalRef.hide();
    }, error => this.error = error));
  }

  iniciarPedido() {
    this.pedidosForm.push(this.formControlPedido());
  }

  formControlPedido() {
    return this.formBuilder.group({
      id: new FormControl(null),
      comandaId: new FormControl(null),
      produtoId: new FormControl(null, Validators.required),
      quantidade: new FormControl(1, [Validators.required, Validators.min(1)]),
      situacao: new FormControl(ComandaPedidoSituacao.Pedido),
    });
  }

  removerPedido(index: number) {
    this.pedidosForm.removeAt(index);
  }
}
