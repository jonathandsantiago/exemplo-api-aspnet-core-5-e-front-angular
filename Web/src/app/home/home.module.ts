import {NgModule} from '@angular/core';
import {HomeComponent} from './home.component';
import {ComandaGarcomComponent} from './comanda/comanda-garcom/comanda-garcom.component';
import {ComandaCozinhaComponent} from './comanda/comanda-cozinha/comanda-cozinha.component';
import {CommonModule} from '@angular/common';
import {HomeRoutingModule} from './home-routing.moduel';
import {MatTableModule} from '@angular/material/table';
import {MatBadgeModule} from '@angular/material/badge';
import {ReactiveFormsModule} from '@angular/forms';
import {MatIconModule} from '@angular/material/icon';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatSortModule} from '@angular/material/sort';
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatMenuModule} from '@angular/material/menu';
import {MatCardModule} from '@angular/material/card';
import {MatListModule} from '@angular/material/list';
import {MatChipsModule} from '@angular/material/chips';
import {MatOptionModule} from '@angular/material/core';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {MatSelectModule} from '@angular/material/select';
import {MatStepperModule} from '@angular/material/stepper';
import {MatTabsModule} from '@angular/material/tabs';
import {ComandaEditComponent} from './comanda/comanda-edit/comanda-edit.component';

@NgModule({
  declarations: [
    HomeComponent,
    ComandaGarcomComponent,
    ComandaCozinhaComponent,
    ComandaEditComponent,
    ComandaEditComponent
  ],
  imports: [
    CommonModule,
    HomeRoutingModule,
    MatTableModule,
    MatBadgeModule,
    ReactiveFormsModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCheckboxModule,
    MatSortModule,
    MatPaginatorModule,
    MatMenuModule,
    MatCardModule,
    MatListModule,
    MatChipsModule,
    MatProgressSpinnerModule,
    MatOptionModule,
    MatSelectModule,
    MatStepperModule,
    MatTabsModule
  ],
  exports: [HomeComponent]
})
export class HomeModule {
}
