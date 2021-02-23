import {ActivatedRoute, Router} from '@angular/router';
import {OnInit} from '@angular/core';
import {FormGroup} from '@angular/forms';
import {IService} from './interfaces';
import {IModelBase} from '../models/model-base';

export enum ActionView {
  create,
  edit,
  detail
}

export abstract class EditViewBase<TModel extends IModelBase<TId>, TId> implements OnInit {
  form: FormGroup;

  constructor(
    protected service: IService<TModel, TId>,
    protected router: Router,
    protected actionView: ActionView,
    protected route: ActivatedRoute) {
  }

  public async submit() {
    const result = await this.onSave();

    if (!result.success) {

      if (result.err.status === 400) {
        // this.showMenssageErrors(result.err.error);
      }

      return false;
    }

    this.AfterSave();
  }

  protected abstract AfterSave();

  private async onSave() {
    const model = this.form.value as TModel;
    switch (this.actionView) {
      case ActionView.create: {
        return await this.service.add(model);
      }
      case ActionView.edit: {
        return await this.service.update(model.id, model);
      }
      default: {
        throw new Error('not Implemented');
      }
    }
  }

  ngOnInit(): void {
    this.onInit();
    if (this.actionView === ActionView.detail ||
      this.actionView === ActionView.edit) {
      this.configureEdit();
    }
  }

  onInit() {
  }

  configureEdit() {
    this.route.params.subscribe(async params => {
      if (params.id == null || params.id === '') {
        return;
      }

      const result = await this.service.getById(params.id);
      // this.model = result.data;
    });
  }
}
