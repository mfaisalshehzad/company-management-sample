import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { CompaniesService } from 'app/modules/admin/companies/companies.service';
import { CompanyPagination,CompanyViewModel } from 'app/modules/admin/companies/models';

@Injectable({
    providedIn: 'root'
})
export class CompaniesResolver implements Resolve<any>
{
    /**
     * Constructor
     */
    constructor(private _companiesService: CompaniesService)
    {
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Resolver
     *
     * @param route
     * @param state
     */
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<{ pagination: CompanyPagination; companies: CompanyViewModel[] }>
    {
        return this._companiesService.getCompanies({});
    }
}
