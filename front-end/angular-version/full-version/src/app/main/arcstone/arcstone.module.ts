import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { TranslateModule } from '@ngx-translate/core';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgApexchartsModule } from 'ng-apexcharts';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { CoreCommonModule } from '@core/common.module';

import { InvoiceModule } from 'app/main/apps/invoice/invoice.module';

import { ScheduleComponent } from './schedule/my-schedule.component';
import { CoreCardModule } from '@core/components/core-card/core-card.module';
import { NgSelectModule } from '@ng-select/ng-select';
import { AuthGuard } from 'app/auth/helpers';
import { FormElementsModule } from 'app/main/forms/form-elements/form-elements.module';
import { TimePickerI18nModule } from 'app/main/forms/form-elements/date-time-picker/time-picker-i18n/time-picker-i18n.module';
import { DatePickerI18nModule } from 'app/main/forms/form-elements/date-time-picker/date-picker-i18n/date-picker-i18n.module';
import { ButtonsComponent } from 'app/main/components/buttons/buttons.component';
import { ScheduleManageComponent } from './schedule-manage/schedule-manage.component';
import { ScheduleChartComponent } from './schedule-chart/schedule-chart.component';
import { ChartModule } from 'primeng/chart';
import {DropdownModule} from 'primeng/dropdown';
import { ArcstoneService } from './arcstone.service';
import {ToastModule} from 'primeng/toast';
import { MessageService } from 'primeng/api';



const routes = [
  {
    path: 'schedule',
    component: ScheduleComponent,
    resolve: {
    }
  },
  {
    path: 'schedule-manager',
    component: ScheduleManageComponent,
    resolve: {
    }
  },
  {
    path: 'schedule-chart',
    component: ScheduleChartComponent,
    resolve: {
    }
  }
];

@NgModule({
  declarations: [ScheduleComponent, ScheduleManageComponent, ScheduleChartComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    TranslateModule,
    NgbModule,
    PerfectScrollbarModule,
    CoreCommonModule,
    NgApexchartsModule,
    InvoiceModule,
    CoreCardModule,
    NgSelectModule,
    FormElementsModule,
    TimePickerI18nModule,
    DatePickerI18nModule,
    ChartModule,
    DropdownModule,
    ToastModule
  ],
  providers: [ArcstoneService, MessageService],
  exports: []
})
export class ArcstoneModule {}
