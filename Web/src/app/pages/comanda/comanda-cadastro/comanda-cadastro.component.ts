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

@Component({
  selector: 'app-comanda-cadastro',
  templateUrl: './comanda-cadastro.component.html',
  styleUrls: ['./comanda-cadastro.component.scss'],
})
export class ComandaCadastroComponent extends CrudComponent implements OnInit {
  @Input() modalRef: BsModalRef;
  pedidosForm = new FormArray([]);
  produtos: Produto[];

  constructor(injector: Injector,
              protected formBuilder: FormBuilder,
              protected toastrService: ToastrService,
              protected produtoService: ProdutoService,
              protected router: Router,
              protected comandaService: ComandaService) {
    super(injector);
  }

  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      garcomId: new FormControl(null, Validators.required),
      totalAPagar: new FormControl(null),
      gorjetaGarcom: new FormControl(null),
      situacao: new FormControl(ComandaSituacao.Aberta, Validators.required)
    });

    this.subscription.add(this.produtoService.obterTodos().subscribe((produtos: any) => this.produtos = produtos));
    this.adicionarPedido();
  }

  onSubmit() {
    this.submitted = true;
    const comanda = this.formGroup.value;

    if (this.formGroup.invalid && this.pedidosForm.invalid) {
      return;
    }

    this.subscription.add(this.comandaService.cadastrar(comanda).subscribe(response => {
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
