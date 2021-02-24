import {Component, Injector, OnInit} from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {UsuarioService} from '../../../services/usuario.service';
import {UsuarioPerfil} from '../../../models/usuario';
import {ToastrService} from 'ngx-toastr';
import {Router} from '@angular/router';
import {Location} from '@angular/common';
import {convertToInt} from '../../../shared/util';
import {CrudComponent} from '../../../components/common/crud.component';

@Component({
  selector: 'app-usuario-edicao',
  templateUrl: './usuario-edicao.component.html',
  styleUrls: ['./usuario-edicao.component.scss']
})
export class UsuarioEdicaoComponent extends CrudComponent implements OnInit {
  usuarioLogadoIsAdmin = false;
  convertToInt = convertToInt;
  id = null;
  perfil = UsuarioPerfil.Garcon;

  constructor(injector: Injector,
              protected formBuilder: FormBuilder,
              protected toastrService: ToastrService,
              protected usuarioService: UsuarioService,
              protected router: Router,
              protected location: Location) {
    super(injector);
    this.usuarioService.usuarioLogado$.subscribe(usuario => {
      this.usuarioLogadoIsAdmin = usuario && usuario.perfil === 2;
    });
  }

  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      id: [null],
      nome: ['', Validators.required],
      login: ['', Validators.required],
      perfil: [String(UsuarioPerfil.Garcon), Validators.required],
      ativo: [true, Validators.required],
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
      this.usuarioService.obterPorId(this.id).subscribe((usuario) => {
        usuario.perfil = usuario.perfil === 0 ? null : usuario.perfil;
        this.formGroup.reset(usuario);
      });
    }));
  }

  onSubmit() {
    this.submitted = true;

    if (this.formGroup.invalid) {
      return;
    }

    const usuario = this.formGroup.value;

    this.subscription.add(this.usuarioService.editar(usuario, {id: this.id}).subscribe(
      usuarioEditado => {
        this.usuarioService.atualizarUsuario(usuarioEditado);
        this.toastrService.success(`Usuário atualizado com sucesso.`, null, {
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
