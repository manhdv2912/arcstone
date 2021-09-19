export class Project {
    projectName: string;
    id: number;

    constructor(data?: Project) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.projectName = data['projectName'];
            this.id = data['id'];
        }
    }
    static fromJS(data: any): Project {
        data = typeof data === 'object' ? data : {};
        const result = new Project();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data['projectName'] = this.projectName;
        data['id'] = this.id;
        return data;
    }
}

export class CreateScheduleInput{
    id?: number;
    scheduleDate: string;
    startHour: number;
    startMinute: number;
    endHour: number;
    endMinute: number;
    projectId: number;
    description: string;
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data['scheduleDate'] = this.scheduleDate;
        data['startHour'] = this.startHour;
        data['startMinute'] = this.startMinute;
        data['endHour'] = this.endHour;
        data['endMinute'] = this.endMinute;
        data['projectId'] = this.projectId;
        data['description'] = this.description;
        data['id'] = this.id;
        return data;
    }
}

export class BaseResponse {
    message: string;
    status: boolean;

    constructor(data?: BaseResponse) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.message = data['message'];
            this.status = data['status'];
        }
    }
    static fromJS(data: any): BaseResponse {
        data = typeof data === 'object' ? data : {};
        const result = new BaseResponse();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data['message'] = this.message;
        data['status'] = this.status;
        return data;
    }
}

export class ScheduleInWeekDto {
    listSchedules: ScheduleInDayDto[];
    dayInWeek: string;
    totalHourStr: string;
}

export class ScheduleInDayDto {
    id: number;
    description: string;
    projectName: string;
    startTimeStr: string;
    endTimeStr: string;
    totalHourStr: string;
    projectId: number;
    startHour: number;
    endHour: number;
    startMinute: number;
    endMinute: number;
    year: number;
    month: number;
    day: number;

}

export class WeeklySummaryDto {
    id: number;
    weekIndex: number;
    totalHours: number;
    totalHoursStr: string;
}

export class SummaryByProjectInDay {
    totalHours: number;
    projectId: number;
}

export class SummaryByDayInWeek {
    listProject: SummaryByProjectInDay[];
    dayStr: string;
}