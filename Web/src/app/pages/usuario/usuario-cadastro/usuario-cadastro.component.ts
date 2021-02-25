import {Component, Injector, OnInit} from '@angular/core';
import {FormBuilder, FormControl, Validators} from '@angular/forms';
import {ToastrService} from 'ngx-toastr';
import {Router} from '@angular/router';
import {CrudComponent} from '../../../components/common/crud.component';
import {UsuarioService} from '../../../services/usuario.service';
import {UsuarioPerfil} from '../../../models/usuario';

@Component({
  selector: 'app-feriado-cadastro',
  templateUrl: './usuario-cadastro.component.html',
  styleUrls: ['./usuario-cadastro.component.scss'],
})
export class UsuarioCadastroComponent extends CrudComponent implements OnInit {

  constructor(injector: Injector,
              protected formBuilder: FormBuilder,
              protected toastrService: ToastrService,
              protected router: Router,
              protected usuarioService: UsuarioService) {
    super(injector);
  }

  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      nome: new FormControl(null, Validators.required),
      login: new FormControl(null, Validators.required),
      password: new FormControl(null, Validators.required),
      perfil: new FormControl(UsuarioPerfil.Garcon, Validators.required),
      Ativo: new FormControl(true, Validators.required),
    });
  }

  onSubmit() {
    this.submitted = true;
    const usuario = this.formGroup.value;

    if (this.formGroup.invalid) {
      return;
    }

    this.subscription.add(this.usuarioService.cadastrar(usuario).subscribe(response => {
      this.toastrService.success(`Usuário registrado com sucesso.`, null, {
        positionClass: 'toast-bottom-right',
        disableTimeOut: false,
        progressBar: true
      });
    }, error => this.error = error));
  }
}
