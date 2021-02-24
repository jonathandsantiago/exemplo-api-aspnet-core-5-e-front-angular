import {Component, Injector, OnInit} from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {ToastrService} from 'ngx-toastr';
import {Router} from '@angular/router';
import {CrudComponent} from '../../../components/common/crud.component';
import {UsuarioService} from '../../../services/usuario.service';

@Component({
  selector: 'app-produto-cadastro',
  templateUrl: './produto-cadastro.component.html',
  styleUrls: ['./produto-cadastro.component.scss'],
})
export class ProdutoCadastroComponent extends CrudComponent implements OnInit {

  constructor(injector: Injector,
              protected formBuilder: FormBuilder,
              protected toastrService: ToastrService,
              protected router: Router,
              protected usuarioService: UsuarioService) {
    super(injector);
  }

  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      nome: ['', Validators.required],
      preco: [null],
      ulrImage: [null],
    });
  }

  onSubmit() {
    this.submitted = true;
    const produto = this.formGroup.value;

    if (this.formGroup.invalid) {
      return;
    }

    this.subscription.add(this.usuarioService.cadastrar(produto).subscribe(response => {
      this.toastrService.success(`Produto registrado com sucesso.`, null, {
        positionClass: 'toast-bottom-right',
        disableTimeOut: false,
        progressBar: true
      });
    }, error => this.error = error));
  }
}
