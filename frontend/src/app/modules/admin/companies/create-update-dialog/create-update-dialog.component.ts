import { Component, OnInit, Inject, ViewEncapsulation, OnDestroy } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { Subject, takeUntil } from 'rxjs';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { fuseAnimations } from '@fuse/animations';
import { Company, CompanyViewModel, IndustryTypeEnum } from 'app/modules/admin/companies/models';
import { CompaniesService } from 'app/modules/admin/companies/companies.service';

@Component({
    selector: 'app-create-update-company-dialog',
    templateUrl: './create-update-dialog.component.html',
    styleUrls: ['./create-update-dialog.component.scss'],
    animations: fuseAnimations,
    encapsulation: ViewEncapsulation.None
})
export class CreateUpdateDialogComponent implements OnInit, OnDestroy {
    dialogTitle: string;
    companiesDropdownData: any;
    companyForUpdate: any;
    companyForm: FormGroup;
    errorMessage: string;
    industryTypes = Object.keys(IndustryTypeEnum).map(key => {
        return {
            value: key,
            label: <string>IndustryTypeEnum[key]
        };
    });

    private _unsubscribeAll: Subject<any> = new Subject<any>();

    /**
     * Constructor
     */
    constructor(
        @Inject(MAT_DIALOG_DATA) public data: any,
        private _matDialogRef: MatDialogRef<CreateUpdateDialogComponent>,
        private _formBuilder: FormBuilder,
        private _companiesService: CompaniesService) {
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void {
        if (this.data) {
            this.dialogTitle = this.data.dialogTitle;
            this.companiesDropdownData = this.data.companiesDropdownData;
            this.companyForUpdate = this.data.companyForUpdate;
        }

        if (this.companyForUpdate) {
            this.companyForm = this.editCompanyForm(this.companyForUpdate);
        }
        else {
            this.companyForm = this.createCompanyForm();
        }
    }

    /**
     * After view init
     */
    ngAfterViewInit(): void {

    }

    /**
     * On destroy
     */
    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    createOrUpdateCompany(): void {
        this.errorMessage = null;
        const newCompany: Company = {
            id: this.companyForm.value?.id!,
            companyNo: this.companyForm.value.companyNo,
            companyName: this.companyForm.value.companyName,
            industry: this.companyForm.value.industry,
            numberOfEmployees: this.companyForm.value.numberOfEmployees,
            city: this.companyForm.value.city,
            parentCompanyId: this.companyForm.value.parentCompanyId
        };

        this._companiesService
            .createOrUpdate(newCompany)
            .pipe(takeUntil(this._unsubscribeAll))
            .toPromise()
            .then((response) => {
                this._matDialogRef.close({ dataSaved: true, result: response });
            })
            .catch((error) => {
                this.errorMessage = "Occurred in saving the comapany data.";
            });
    }

    createCompanyForm(): FormGroup {
        return this._formBuilder.group({
            companyNo: new FormControl(''),
            companyName: new FormControl('', [Validators.required, Validators.pattern(/^[A-Za-z\s]+$/)]),
            industry: new FormControl('', [Validators.required]),
            numberOfEmployees: new FormControl('', [Validators.required, Validators.min(1), Validators.max(1000000)]),
            city: new FormControl('', [Validators.required, Validators.maxLength(50), Validators.pattern(/^[A-Za-z\-\s]+$/)]),
            parentCompanyId: new FormControl()
        });
    }


    editCompanyForm(company: CompanyViewModel): FormGroup {
        return this._formBuilder.group({
            id: new FormControl(company.id),
            companyNo: new FormControl(company.companyNo),
            companyName: new FormControl(company.companyName, [Validators.required, Validators.pattern(/^[A-Za-z\s]+$/)]),
            industry: new FormControl(company.industry, [Validators.required]),
            numberOfEmployees: new FormControl(company.numberOfEmployees, [Validators.required, Validators.min(1), Validators.max(1000000)]),
            city: new FormControl(company.city, [Validators.required, Validators.maxLength(50), Validators.pattern(/^[A-Za-z\-\s]+$/)]),
            parentCompanyId: new FormControl(company.parentCompanyId)
        });
    }

    closeDialog() {
        this._matDialogRef.close({ dataSaved: false });
    }
}
