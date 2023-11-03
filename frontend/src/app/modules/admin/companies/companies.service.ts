import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment as config } from 'environments/environment';
import { CompanyFilter, Company, CompanyViewModel, CompanyPagination, SortOrderEnum } from './models';

@Injectable({
    providedIn: 'root'
})
export class CompaniesService {
    private _companies: BehaviorSubject<CompanyViewModel[] | null> = new BehaviorSubject(null);
    private _pagination: BehaviorSubject<CompanyPagination | null> = new BehaviorSubject(null);
    private _pageSize: number = 5;
    private _pageIndex: number = 0;

    private _sortBy: string = 'CompanyName';
    private _sortOrder: SortOrderEnum = SortOrderEnum.Ascending;

    /**
     * Constructor
     */
    constructor(private _httpClient: HttpClient) {
    }

    setPageSize(pageSize: number) {
        this._pageSize = pageSize;
    }

    setPageIndex(pageIndex: number) {
        this._pageIndex = pageIndex;
    }

    setSortBy(sortBy: string) {
        this._sortBy = sortBy;
    }


    setSortOrder(sortOrder: SortOrderEnum) {
        this._sortOrder = sortOrder;
    }

    // Get one company
    get(id) {
        return this._httpClient.get(`${config.api.host}${config.api.companyGetOnePath}${id}`);
    }

    // create or Update compnay
    createOrUpdate(company: Company) {
        return this._httpClient.post(`${config.api.host}${config.api.createOrUpdatePath}`, company);
    }

    get pagination(): Observable<CompanyPagination> {
        return this._pagination.asObservable();
    }

    /**
  * Getter for comanies
  */
    get companies(): Observable<CompanyViewModel[]> {
        return this._companies.asObservable();
    }

    getCompanies(filter: CompanyFilter):
        Observable<{ pagination: CompanyPagination; companies: CompanyViewModel[] }> {
        return this._httpClient.post<{ pagination: CompanyPagination; companies: CompanyViewModel[] }>(`${config.api.host}${config.api.compniesFilterListPath}`,
            {
                ...filter,
                sortBy: this._sortBy,
                sortOrder: this._sortOrder,
                pageIndex: this._pageIndex,
                pageSize: this._pageSize
            }).pipe(
                tap((response) => {
                    this._pagination.next(response.pagination);
                    this._companies.next(response.companies);
                })
            );
    }

    // Companines listing for the Dropdown binding
    getAllCompanies() {
        return this._httpClient.get(`${config.api.host}${config.api.compniesAllListPath}`);
    }
}
