import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

import { BehaviorSubject, Observable } from 'rxjs';

@Injectable()
export class ArcstoneService {
  
  constructor() {
    // Set the defaults
  }
  
  CalculateWorkingTime(workingTimes: WorkingTime[]){
      let totalMinute = 0;
      workingTimes.forEach(wt => {
          let minuteStartFrom = wt.startHour * 60 + wt.startMinute;
          let minuteEndFrom = wt.endHour * 60 + wt.endMinute;
          if (minuteEndFrom > minuteStartFrom) {
            totalMinute += minuteEndFrom - minuteStartFrom;
          }
      });
      if (totalMinute > 0) {
        let hour = ((totalMinute / 60) < 10 ? "0" : "" ) + (Math.floor(totalMinute / 60));
        let minute = ((totalMinute % 60) < 10 ? "0" : "" ) +  (totalMinute % 60);
        return hour + ":" + minute;
      }
      return "00:00";
  }

  ConvertDatetimeString(year?: number, month?: number, day?: number, hour?: number, minute?: number, second?: number): string{
    let yyyy = !year ? "2021" : year.toString();
    let MM = !month ? "01" : (month > 9 ? "" : "0") + month;
    let dd = !day ? "01" : (day > 9 ? "" : "0") + day;
    let HH = !hour ? "01" : (hour > 9 ? "" : "0") + hour;
    let mm = !minute ? "00" : (minute > 9 ? "" : "0") + minute;
    let ss = !second ? "00" : (second > 9 ? "" : "0") + second;
    return `${yyyy}-${MM}-${dd}T${HH}:${mm}:${ss}.000Z`;
  }
  
}

export class WorkingTime {
    startHour: number | undefined;
    startMinute: number | undefined;
    endHour: number | undefined;
    endMinute: number | undefined;
}
