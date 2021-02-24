import {Component, Injector, OnInit} from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {ToastrService} from 'ngx-toastr';
import {Router} from '@angular/router';
import {CrudComponent} from '../../../components/common/crud.component';
import {ComandaSituacao} from '../../../models/comanda';
import {ComandaService} from '../../../services/comanda.service';

@Component({
  selector: 'app-comanda-cadastro',
  templateUrl: './comanda-cadastro.component.html',
  styleUrls: ['./comanda-cadastro.component.scss'],
})
export class ComandaCadastroComponent extends CrudComponent implements OnInit {

  constructor(injector: Injector,
              protected formBuilder: FormBuilder,
              protected toastrService: ToastrService,
              protected router: Router,
              protected comandaService: ComandaService) {
    super(injector);
  }

  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      garcomId: ['', Validators.required],
      totalAPagar: [null],
      gorjetaGarcom: [null],
      situacao: [ComandaSituacao.Aberta],
    });
  }

  onSubmit() {
    this.submitted = true;
    const comanda = this.formGroup.value;

    if (this.formGroup.invalid) {
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
}
