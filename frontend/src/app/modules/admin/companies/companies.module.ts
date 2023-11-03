import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTooltipModule } from '@angular/material/tooltip';
import { CompaniesComponent } from 'app/modules/admin/companies/companies.component';
import { CreateUpdateDialogComponent } from 'app/modules/admin/companies/create-update-dialog/create-update-dialog.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatToolbarModule } from '@angular/material/toolbar';
import { FuseAlertModule } from '@fuse/components/alert';
import { SharedModule } from 'app/shared/shared.module';
import { CompaniesRoutes } from 'app/modules/admin/companies/companies.routing';

@NgModule({
    providers: [
        { provide: MatDialogRef, useValue: {} },
        { provide: MAT_DIALOG_DATA, useValue: {} }
    ],
    declarations: [
        CompaniesComponent,
        CreateUpdateDialogComponent
    ],
    imports: [
        RouterModule.forChild(CompaniesRoutes),
        MatButtonModule,
        MatDividerModule,
        MatIconModule,
        MatMenuModule,
        MatFormFieldModule,
        MatInputModule,
        MatSelectModule,
        MatProgressBarModule,
        MatSortModule,
        MatTableModule,
        MatDialogModule,
        MatToolbarModule,
        MatPaginatorModule,
        MatTooltipModule,
        FuseAlertModule,
        SharedModule
    ]
})
export class CompaniesModule {
}
