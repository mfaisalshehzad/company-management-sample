import { AfterViewInit, ChangeDetectionStrategy, Component, OnDestroy, OnInit, ViewChild, ChangeDetectorRef, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup, FormBuilder } from '@angular/forms';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { debounceTime, map, merge, Observable, Subject, switchMap, takeUntil, forkJoin } from 'rxjs';
import { fuseAnimations } from '@fuse/animations';
import { CompaniesService } from 'app/modules/admin/companies/companies.service';
import { CreateUpdateDialogComponent } from 'app/modules/admin/companies/create-update-dialog/create-update-dialog.component';
import { CompanyFilter, IndustryTypeEnum, SortOrderEnum, CompanyViewModel, CompanyPagination } from 'app/modules/admin/companies/models';

@Component({
    selector: 'companies',
    templateUrl: './companies.component.html',
    styleUrls: ['./companies.component.scss'],
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: fuseAnimations
})
export class CompaniesComponent implements OnInit, AfterViewInit, OnDestroy {
    @ViewChild(MatPaginator) private _paginator: MatPaginator;
    @ViewChild(MatSort) private _sort: MatSort;
    searchForm: FormGroup;
    totalRecords: number;
    pageIndex: number;
    pageSize: number;
    companiesDropdownData: any[];
    isLoading: boolean = false;
    filter: CompanyFilter;
    companies: Observable<CompanyViewModel[]>;
    pagination: CompanyPagination;
    private _unsubscribeAll: Subject<any> = new Subject<any>();
    industryTypes = Object.keys(IndustryTypeEnum).map(key => {
        return {
            value: key,
            label: <string>IndustryTypeEnum[key]
        };
    });
    /**
     * Constructor
     */
    constructor(private _changeDetectorRef: ChangeDetectorRef,
        private _companiesService: CompaniesService,
        private _formBuilder: FormBuilder,
        private _matDialog: MatDialog,
    ) {
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void {
        // Get the pagination
        this._companiesService.pagination
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((pagination: CompanyPagination) => {

                // Update the pagination
                this.pagination = pagination;
                // Mark for check
                this._changeDetectorRef.markForCheck();
            });

        // get the companies data
        this.companies = this._companiesService.companies;
        this.loadCompaniesDropdownData();
        this.searchForm = this._formBuilder.group({
            companyNo: new FormControl(''),
            companyName: new FormControl(''),
            industry: new FormControl(''),
            city: new FormControl(''),
            parentCompany: new FormControl('')
        });
    }

    /**
     * After view init
     */
    ngAfterViewInit(): void {
        if (this._sort && this._paginator) {
            // Set the initial sort
            this._sort.sort({
                id: 'CompanyNo',
                start: 'asc',
                disableClear: true
            });

            // Mark for check
            this._changeDetectorRef.markForCheck();

            // If the user changes the sort order...
            this._sort.sortChange
                .pipe(takeUntil(this._unsubscribeAll))
                .subscribe(() => {
                    // Reset back to the first page
                    this._paginator.pageIndex = 0;
                });

            // Get companies if sort or page changes
            merge(this._sort.sortChange, this._paginator.page).pipe(
                switchMap(() => {
                    this.isLoading = true;
                    this._companiesService.setPageSize(this._paginator.pageSize);
                    this._companiesService.setPageIndex(this._paginator.pageIndex);
                    this._companiesService.setSortBy(this._sort.active ?? "CompanyName");
                    this._companiesService.setSortOrder(this._sort.direction === "desc" ? SortOrderEnum.Descending : SortOrderEnum.Ascending);
                    return this._companiesService.getCompanies({ ...this.filter });
                }),
                map(() => {
                    this.isLoading = false;
                })
            ).subscribe();
        }
    }

    /**
     * On destroy
     */
    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    loadCompaniesDropdownData() {
        this._companiesService.getAllCompanies()
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((data: any) => {
                this.companiesDropdownData = data.map(record => ({
                    value: record.id,
                    label: `${record.companyNo}-${record.companyName}`
                }));
            });
    }

    /**
     * Track by function for ngFor loops
     *
     * @param index
     * @param item
     */
    trackByFn(index: number, item: any): any {
        return item.id || index;
    }

    search(): void {
        const searchKeys = Object.keys(this.searchForm.value);
        let searchValues = this.searchForm.value;
        searchKeys.forEach(key => {
            if (!searchValues[key])
                delete searchValues[key];
        });
        this.isLoading = true;
        this._companiesService.setPageIndex(0);
        this._companiesService.setSortBy(this._sort.active ?? "CompanyName");
        this._companiesService.getCompanies({ ...searchValues })
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((r) => this.isLoading = false);
    }

    resetSearch() {
        Object.keys(this.searchForm.controls).forEach(controlName => {
            this.searchForm.get(controlName).reset(null);
            this.isLoading = true;
            this._companiesService.getCompanies({})
                .pipe(takeUntil(this._unsubscribeAll))
                .subscribe((r) => this.isLoading = false);
        });
    }

    createCompany(): void {
        const ref = this._matDialog
            .open(CreateUpdateDialogComponent, {
                panelClass: 'create-update-company-dialog',
                width: '800px',
                data: { dialogTitle: 'Create New Company', companiesDropdownData: this.companiesDropdownData }
            });
        ref.afterClosed()
            .subscribe((response: any) => {
                if (response) {
                    this.loadCompaniesDropdownData();
                    this._companiesService.getCompanies({})
                        .pipe(takeUntil(this._unsubscribeAll))
                        .subscribe();
                }
            });
    }

    editCompany(id: string) {
        this._companiesService
            .get(id)
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((company: any) => {
                const ref = this._matDialog
                    .open(CreateUpdateDialogComponent, {
                        panelClass: 'create-update-company-dialog',
                        width: '800px',
                        data: { dialogTitle: 'Edit Company', companyForUpdate: company, companiesDropdownData: this.companiesDropdownData.filter(c => c.value !== id) }
                    });
                ref.afterClosed()
                    .subscribe((response: any) => {
                        if (response && response.dataSaved) {
                            this.loadCompaniesDropdownData();
                            this._companiesService.getCompanies({});

                        }
                    });
            });
    }
}
