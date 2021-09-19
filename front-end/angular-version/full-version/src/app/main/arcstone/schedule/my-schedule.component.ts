import { Component, OnInit, ViewEncapsulation, ViewChild } from '@angular/core';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { ToastService } from 'app/main/components/toasts/toasts.service';
import { CreateScheduleInput, ScheduleInDayDto, ScheduleInWeekDto, WeeklySummaryDto } from 'app/models/project.model';
import { BackendProxy } from 'app/services/backend.service';
import { MessageService } from 'primeng/api';
import { ArcstoneService, WorkingTime } from '../arcstone.service';
import * as moment from 'moment';
import { forkJoin, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
// import { first } from 'rxjs/operators';

// import { CoreConfigService } from '@core/services/config.service';

// import { colors } from 'app/colors.const';
// import { User } from 'app/auth/models';
// import { UserService } from 'app/auth/service';
// import { DashboardService } from 'app/main/dashboard/dashboard.service';

@Component({
    selector: 'my-schedule',
    templateUrl: './my-schedule.component.html',
    styleUrls: ['./my-schedule.component.scss'],
    // encapsulation: ViewEncapsulation.None
})
export class ScheduleComponent implements OnInit {

    private _unsubscribe = new Subject();
    public listProjects: any[] = [];
    public selectBasicLoading = false;
    public startTime = { hour: 12, minute: 30 };
    public endTime = { hour: 13, minute: 30 };
    public scheduleDate: NgbDateStruct;
    public selectedProject: any;
    public description: string;
    public totalTime: string;
    public lstSchedule: ScheduleInWeekDto[] = [];
    public weeklySummary: WeeklySummaryDto;
    public editId: number;

    constructor(
        private _backendService: BackendProxy,
        private _arcstoneService: ArcstoneService,
        private messageService: MessageService
    ) {

    }
    ngOnInit(): void {
        this._backendService.getAllProject().subscribe((data: any[]) => {
            this.listProjects = data;
        });
        this.loadData();
        this.changeTime()
    }

    addOrUpdateSchedule() {
        if (this.totalTime == '00:00') {
            this.messageService.add({severity:'error', summary:'Schedule', detail:'WorkingTime not valid'});
            return;
        }
        else if (!this.scheduleDate || !this.description || !document.getElementById('projectSelected')['value']) {
            this.messageService.add({severity:'error', summary:'Schedule', detail:'NOT VALID'});
            return;
        } else {
            let input = new CreateScheduleInput();
            input.description = this.description;
            // input.scheduleDate = moment().year(this.scheduleDate['year']).month(this.scheduleDate['month']).day(this.scheduleDate['day']);
            input.scheduleDate = this._arcstoneService.ConvertDatetimeString(Number(this.scheduleDate['year']), Number(this.scheduleDate['month']), Number(this.scheduleDate['day']));
            input.projectId = Number(document.getElementById('projectSelected')['value']);
            input.startHour = this.startTime.hour;
            input.startMinute = this.startTime.minute;
            input.endMinute = this.endTime.minute;
            input.endHour = this.endTime.hour;
            if (!this.editId) {
                this._backendService.createSchedule(input).subscribe(rs => {
                    if (rs['status']) {
                        this.messageService.add({severity:'success', summary:'Schedule', detail:'Saved'});
                        this.loadData();
                    } else {
                        this.messageService.add({severity:'error', summary:'Schedule', detail: rs['message']});
                    }
                }, error => {
                    this.messageService.add({severity:'error', summary:'Schedule', detail:'WRONG'});
                })
            } else {
                input.id = this.editId;
                this._backendService.updateSchedule(input).subscribe(rs => {
                    if (rs['status']) {
                        this.messageService.add({severity:'success', summary:'Schedule', detail:'Updated'});
                        this.loadData();
                        this.cancelEdit();
                    } else {
                        this.messageService.add({severity:'error', summary:'Schedule', detail: rs['message']});
                    }
                }, error => {
                    this.messageService.add({severity:'error', summary:'Schedule', detail:'WRONG'});
                })
            }
            
        }
    }

    changeTime() {
        let workingTime = new WorkingTime();
        workingTime.startHour = this.startTime.hour;
        workingTime.startMinute = this.startTime.minute;
        workingTime.endHour = this.endTime.hour;
        workingTime.endMinute = this.endTime.minute;
        this.totalTime = this._arcstoneService.CalculateWorkingTime([workingTime]);
    }

    loadData(){
        forkJoin([
            this._backendService.getAllScheduleInWeek().pipe(takeUntil(this._unsubscribe)),
            this._backendService.findCurrentWeeklySummary().pipe(takeUntil(this._unsubscribe))
          ]).subscribe(([scheduleInWeek, weeklySummary]) => {
            this.lstSchedule = scheduleInWeek;
            this.weeklySummary = weeklySummary;
          });
        
    }

    delete(id: number){
        this._backendService.deleteScheduleById(id).pipe(takeUntil(this._unsubscribe)).subscribe(rs => {
            if (rs['status']) {
                this.messageService.add({severity:'success', summary:'Schedule', detail:'Deleted'});
                this.loadData();
            } else {
                this.messageService.add({severity:'error', summary:'Schedule', detail: rs['message']});
            }
        },
        error => {
            this.messageService.add({severity:'error', summary:'Schedule', detail:'WRONG'});
        })
    }

    edit(schedule: ScheduleInDayDto) {
        this.description = schedule.description;
        document.getElementById('projectSelected')['value'] = schedule.projectId;
        this.startTime = {hour: schedule.startHour, minute: schedule.startMinute};
        this.endTime = {hour: schedule.endHour, minute: schedule.endMinute};
        this.scheduleDate = {year: schedule.year, month: schedule.month, day: schedule.day}
        this.editId = schedule.id;
    }

    cancelEdit(){
        this.editId = undefined;
        this.description = undefined;
        this.startTime = { hour: 12, minute: 30 };
        this.endTime = { hour: 13, minute: 30 };
        this.scheduleDate = undefined
    }
}