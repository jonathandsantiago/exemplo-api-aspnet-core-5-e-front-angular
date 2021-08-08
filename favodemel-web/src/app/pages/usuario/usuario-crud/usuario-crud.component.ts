import {Component, Injector, OnInit} from '@angular/core';
import {FormBuilder, FormControl, Validators} from '@angular/forms';
import {ToastrService} from 'ngx-toastr';
import {Router} from '@angular/router';
import {EditBaseComponent} from '../../../components/common/edit-base.component';
import {UsuarioService} from '../../../services/usuario.service';
import {UsuarioPerfil} from '../../../models/usuario';
import {Location} from '@angular/common';

@Component({
  selector: 'app-usuario-crud',
  templateUrl: './usuario-crud.component.html',
  styleUrls: ['./usuario-crud.component.scss'],
})
export class UsuarioCrudComponent extends EditBaseComponent implements OnInit {

  constructor(injector: Injector,
              protected formBuilder: FormBuilder,
              protected toastrService: ToastrService,
              protected router: Router,
              protected location: Location,
              protected usuarioService: UsuarioService) {
    super(injector);
  }

  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      id: new FormControl(null),
      nome: new FormControl(null, Validators.required),
      login: new FormControl(null, Validators.required),
      password: new FormControl(null),
      perfil: new FormControl(UsuarioPerfil.Garcon, Validators.required),
      ativo: new FormControl(true, Validators.required),
      comissao: new FormControl(null),
    });

    this.subscription.add(this.nagivate$.subscribe(customData => {
      let route = this.router.routerState.root.snapshot;
      while (route.firstChild != null) {
        route = route.firstChild;
      }

      const params = route.params;

      if (params.id == null || params.id === '') {
        this.formGroup.get('password').setValidators(Validators.required);
        return;
      }

      this.id = params.id;
      this.usuarioService.obterPorId(this.id).subscribe((usuario) => {
        this.formGroup.reset(usuario);
      });
    }));
  }

  onSubmit() {
    this.submitted = true;
    const usuario = this.formGroup.value;

    if (this.formGroup.invalid) {
      return;
    }

    const action = this.isEdicao ?
      this.usuarioService.editar(usuario, {id: this.id}) :
      this.usuarioService.cadastrar(usuario, {id: this.id});

    this.subscription.add(action.subscribe(response => {
      this.toastrService.success(`Usuário ${this.id > 0 ? 'atualizado' : 'registrada'} com sucesso.`, null, {
        positionClass: 'toast-bottom-right',
        disableTimeOut: false,
        progressBar: true
      });
      this.location.back();
    }, error => this.error = error));
  }
}
