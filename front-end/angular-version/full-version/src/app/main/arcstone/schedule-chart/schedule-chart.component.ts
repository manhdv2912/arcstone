import { Component, OnInit, ViewEncapsulation, ViewChild } from '@angular/core';
import { BackendProxy } from 'app/services/backend.service';
import { forkJoin, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ArcstoneService } from '../arcstone.service';

@Component({
    selector: 'schedule-chart',
    templateUrl: './schedule-chart.component.html',
    styleUrls: ['./schedule-chart.component.scss'],
    // encapsulation: ViewEncapsulation.None
})
export class ScheduleChartComponent implements OnInit {
    stackedData: any;
    lstDay = [];
    stackedOptions: any;
    listColor = ['#42A5F5','#66BB6A','#FFA726']
    private _unsubscribe = new Subject();


    constructor(private _backendService: BackendProxy,
        private _arcstoneService: ArcstoneService) {

    }
    ngOnInit(): void {
        this.getListDay();
    }
    getListDay() {
        forkJoin([
            this._backendService.getAllProject().pipe(takeUntil(this._unsubscribe)),
            this._backendService.getChartSummary().pipe(takeUntil(this._unsubscribe))
          ]).subscribe(([allProject, weeklySummary]) => {
            this.lstDay = weeklySummary.map(w => w.dayStr);
            let datasets = [];
            allProject.forEach(project => {
                datasets.push({
                    type: 'bar',
                    label: project.projectName,
                    backgroundColor: this.listColor[allProject.findIndex(w => w.id == project.id)],
                    projectId: project.id,
                    data:[]
                })
            });
            for(let i = 0; i < this.lstDay.length; i++){
                let lstTotalSummary = weeklySummary.map(w => w.listProject)[i];
                allProject.forEach(project => {
                    let summary = lstTotalSummary.filter(w => w.projectId == project.id);
                    let total = (summary && summary[0]) ? summary[0].totalHours : 0;
                    datasets.filter(w => w.projectId == project.id)[0].data.push(total);
                })
            }

            debugger;
            console.log(datasets);
            this.stackedData = {
                labels: this.lstDay,
                datasets: datasets
            }
            this.stackedOptions = {
                tooltips: {
                    mode: 'index',
                    intersect: false
                },
                responsive: true,
                scales: {
                    xAxes: [{
                        stacked: true,
                    }],
                    yAxes: [{
                        stacked: true
                    }]
                }
            };
          });
    }


}