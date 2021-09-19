import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';
import { BaseResponse, CreateScheduleInput, Project, ScheduleInWeekDto, SummaryByDayInWeek, WeeklySummaryDto } from 'app/models/project.model';
import { Observable, of as _observableOf, throwError as _observableThrow } from 'rxjs';
import { catchError as _observableCatch, mergeMap as _observableMergeMap } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class BackendProxy {
    private REST_API_SERVER = "http://localhost:29121";
    jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;
    constructor(private http: HttpClient) {
    }

    public getAllProject(): Observable<Project[]> {
        let url_ = `${this.REST_API_SERVER}/api/Project`;
        return this.defaultGetList<Project[]>(url_, Project);
    }

    public createSchedule(input: CreateScheduleInput): Observable<BaseResponse> {
        let url_ = `${this.REST_API_SERVER}/api/Schedule/Create`;
        return this.defaultPost<BaseResponse>(url_, input, BaseResponse);
    }

    public updateSchedule(input: CreateScheduleInput): Observable<BaseResponse> {
        let url_ = `${this.REST_API_SERVER}/api/Schedule/Update`;
        return this.defaultPut<BaseResponse>(url_, input, BaseResponse);
    }

    public getAllScheduleInWeek(): Observable<ScheduleInWeekDto[]> {
        let url_ = `${this.REST_API_SERVER}/api/Schedule/GetAllScheduleInWeek`;
        return this.defaultGetList<ScheduleInWeekDto[]>(url_, ScheduleInWeekDto);
    }

    public findCurrentWeeklySummary(): Observable<WeeklySummaryDto> {
        let url_ = `${this.REST_API_SERVER}/api/WeeklySummary/FindCurrentWeeklySummary`;
        return this.defaultGet<WeeklySummaryDto>(url_, WeeklySummaryDto);
    }

    public deleteScheduleById(id: number): Observable<BaseResponse> {
        let url_ = `${this.REST_API_SERVER}/api/Schedule/DeleteScheduleById?id=` + id;
        return this.defaultDelete<BaseResponse>(url_, BaseResponse);
    }

    public getChartSummary(): Observable<SummaryByDayInWeek[]> {
        let url_ = `${this.REST_API_SERVER}/api/Chart/GetChartSummary`;
        return this.defaultGetList<SummaryByDayInWeek[]>(url_, SummaryByDayInWeek);
    }

    throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): Observable<any> {
        if (result !== null && result !== undefined) {
            return _observableThrow(result);
        } else {
            return _observableThrow(new ApiException(message, status, response, headers, null));
        }
    }

    blobToText(blob: any): Observable<string> {
        return new Observable<string>((observer: any) => {
            if (!blob) {
                observer.next('');
                observer.complete();
            } else {
                const reader = new FileReader();
                reader.onload = event => {
                    observer.next((<any>event.target).result);
                    observer.complete();
                };
                reader.readAsText(blob);
            }
        });
    }

    defaultGet<T>(apiUrl: string, itemType: any): Observable<T> {
        apiUrl = apiUrl.replace(/[?&]$/, '');
        const options_: any = {
            observe: 'response',
            responseType: 'blob',
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            })
        };
        return this.http.request('get', apiUrl, options_).pipe(_observableMergeMap((response_: any) => {
            return this.onProcessGet<T>(<any>response_, itemType);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.onProcessGet<T>(<any>response_, itemType);
                } catch (e) {
                    return <Observable<T>><any>_observableThrow(e);
                }
            } else
                return <Observable<T>><any>_observableThrow(response_);
        }));
    }

    onProcessGet<T>(response: HttpResponseBase, itemType): Observable<any> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
                (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        const _headers: any = {};
        if (response.headers) {
            for (const key of response.headers.keys()) {
                _headers[key] = response.headers.get(key);
            }
        }
        if (status === 200) {
            return this.blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
                let result200: any = null;
                const resultData200 = _responseText === '' ? null : JSON.parse(_responseText, this.jsonParseReviver);
                result200 = new ModelMapper(itemType).map(resultData200);
                return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return this.blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
                return this.throwException('An unexpected server error occurred.', status, _responseText, _headers);
            }));
        }
        return _observableOf<T>(<any>null);
    }

    defaultGetList<T>(apiUrl: string, itemType: any): Observable<T> {
        apiUrl = apiUrl.replace(/[?&]$/, '');
        const options_: any = {
            observe: 'response',
            responseType: 'blob',
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            })
        };
        return this.http.request('get', apiUrl, options_).pipe(_observableMergeMap((response_: any) => {
            return this.onProcessGetList<T>(<any>response_, itemType);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.onProcessGetList<T>(<any>response_, itemType);
                } catch (e) {
                    return <Observable<T>><any>_observableThrow(e);
                }
            } else
                return <Observable<T>><any>_observableThrow(response_);
        }));
    }

    onProcessGetList<T>(response: HttpResponseBase, itemType): Observable<T> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
                (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        const _headers: any = {};
        if (response.headers) {
            for (const key of response.headers.keys()) {
                _headers[key] = response.headers.get(key);
            }
        }
        if (status === 200) {
            return this.blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
                let result200: any = null;
                const resultData200 = _responseText === '' ? null : JSON.parse(_responseText, this.jsonParseReviver);
                if (resultData200 && resultData200.constructor === Array) {
                    result200 = [];
                    for (const item of resultData200) {
                        let itemData = new ModelMapper(itemType).map(item);
                        // console.log(itemData);
                        result200.push(itemData);
                    }

                    // result200.push(ContactHistoryDto.fromJS(item));
                }
                return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return this.blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
                return this.throwException('An unexpected server error occurred.', status, _responseText, _headers);
            }));
        }
        return _observableOf<T>(<any>null);
    }

    defaultPost<T>(apiUrl, data: any, itemType: any): Observable<T> {
        apiUrl = apiUrl.replace(/[?&]$/, '');
        const content_ = JSON.stringify(data);
        const options_: any = {
            body: content_,
            observe: 'response',
            responseType: 'blob',
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            })
        };
        return this.http.request('post', apiUrl, options_).pipe(_observableMergeMap((response_: any) => {
            return this.onProcessPost<T>(<any>response_, itemType);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.onProcessPost<T>(<any>response_, itemType);
                } catch (e) {
                    return <Observable<T>><any>_observableThrow(e);
                }
            } else
                return <Observable<T>><any>_observableThrow(response_);
        }));
    }

    onProcessPost<T>(response: HttpResponseBase, itemType: any): Observable<T> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
                (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        const _headers: any = {};
        if (response.headers) {
            for (const key of response.headers.keys()) {
                _headers[key] = response.headers.get(key);
            }
        }
        if (status === 200) {
            return this.blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
                let result200: any = null;
                const resultData200 = _responseText === '' ? null : JSON.parse(_responseText, this.jsonParseReviver);
                result200 = resultData200 !== undefined ? new ModelMapper(itemType).map(resultData200) : <any>null;
                return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return this.blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
                return this.throwException('An unexpected server error occurred.', status, _responseText, _headers);
            }));
        }
        return _observableOf<T>(<any>null);
    }

    defaultDelete<T>(apiUrl: string, itemType: any): Observable<T> {
        apiUrl = apiUrl.replace(/[?&]$/, '');
        let options_: any = {
            observe: 'response',
            responseType: 'blob',
            headers: new HttpHeaders({})
        };
        return this.http.request('delete', apiUrl, options_).pipe(_observableMergeMap((response_: any) => {
            return this.onProcessDelete<T>(<any>response_, itemType);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.onProcessDelete<T>(<any>response_, itemType);
                } catch (e) {
                    return <Observable<T>><any>_observableThrow(e);
                }
            } else
                return <Observable<T>><any>_observableThrow(response_);
        }));
    }

    onProcessDelete<T>(response: HttpResponseBase, itemType: any): Observable<T> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
                (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        const _headers: any = {};
        if (response.headers) {
            for (const key of response.headers.keys()) {
                _headers[key] = response.headers.get(key);
            }
        }
        if (status === 200) {
            return this.blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
                let result200: any = null;
                const resultData200 = _responseText === '' ? null : JSON.parse(_responseText, this.jsonParseReviver);
                result200 = resultData200 !== undefined ? new ModelMapper(itemType).map(resultData200) : <any>null;
                return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return this.blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
                return this.throwException('An unexpected server error occurred.', status, _responseText, _headers);
            }));
        }
        return _observableOf<T>(<any>null);
    }

    defaultPut<T>(apiUrl: string, data: any, itemType: any): Observable<T> {
        apiUrl = apiUrl.replace(/[?&]$/, '');
        const content_ = JSON.stringify(data);
        const options_: any = {
            body: content_,
            observe: 'response',
            responseType: 'blob',
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
                'Accept': 'text/plain'
            })
        };
        return this.http.request('put', apiUrl, options_).pipe(_observableMergeMap((response_: any) => {
            return this.onProcessPut<T>(response_, itemType);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.onProcessPut<T>(<any>response_, itemType);
                } catch (e) {
                    return <Observable<T>><any>_observableThrow(e);
                }
            } else
                return <Observable<T>><any>_observableThrow(response_);
        }));
    }

    onProcessPut<T>(response: HttpResponseBase, itemType: any): Observable<T> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
                (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        const _headers: any = {};
        if (response.headers) {
            for (const key of response.headers.keys()) {
                _headers[key] = response.headers.get(key);
            }
        }
        if (status === 200) {
            return this.blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
                let result200: any = null;
                const resultData200 = _responseText === '' ? null : JSON.parse(_responseText, this.jsonParseReviver);
                result200 = resultData200 !== undefined ? new ModelMapper(itemType).map(resultData200) : <any>null;
                return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return this.blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
                return this.throwException('An unexpected server error occurred.', status, _responseText, _headers);
            }));
        }
        return _observableOf<T>(<any>null);
    }
}

export class ApiException extends Error {

    message: string;
    status: number;
    response: string;
    headers: { [key: string]: any; };
    result: any;
    protected isApiException = true;

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        super();
        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    static isApiException(obj: any): obj is ApiException {
        return obj.isApiException === true;
    }
}

export class ModelMapper<T> {
    _propertyMapping: any;
    _target: any;
    constructor(type: new () => T) {
        this._target = new type();
        this._propertyMapping = this._target.constructor._propertyMap;
    }

    map(source) {
        Object.keys(this._target).forEach((key) => {
            const mappedKey = this._propertyMapping[key];
            if (mappedKey) {
                this._target[key] = source[mappedKey];
            } else {
                this._target[key] = source[key];
            }
        });

        Object.keys(source).forEach((key) => {
            const targetKeys = Object.keys(this._target);
            if (targetKeys.indexOf(key) === -1) {
                this._target[key] = source[key];
            }
        });
        return this._target;
    }
}