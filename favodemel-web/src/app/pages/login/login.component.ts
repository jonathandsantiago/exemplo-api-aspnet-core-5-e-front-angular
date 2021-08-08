import {Component, OnDestroy, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';

import {UsuarioService} from '../../services/usuario.service';
import {Usuario} from '../../models/usuario';
import {Subscription} from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {
  loginForm: FormGroup;
  submitted = false;
  returnUrl: string;
  error = '';

  subscription: Subscription = new Subscription();

  get formControl() {
    return this.loginForm.controls;
  }

  constructor(protected formBuilder: FormBuilder,
              protected activatedRoute: ActivatedRoute,
              protected router: Router,
              protected usuarioService: UsuarioService) {
    if (this.usuarioService.usuarioLogado) {
      this.router.navigate(['/']).then();
    }
  }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      login: ['', Validators.required],
      password: ['', Validators.required],
    });

    this.returnUrl = this.activatedRoute.snapshot.queryParams[this.returnUrl] || '/';
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  onSubmit() {
    this.submitted = true;

    if (this.loginForm.invalid) {
      return;
    }

    this.subscription.add(this.usuarioService.login(this.loginForm.value as Usuario)
      .subscribe(data => this.router.navigate([this.returnUrl]),
        error => {
          this.error = error;
        }));
  }
}

