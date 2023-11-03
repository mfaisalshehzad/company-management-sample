import { Route } from '@angular/router';
import { CompaniesComponent } from 'app/modules/admin/companies/companies.component';
import { CompaniesResolver } from 'app/modules/admin/companies/companies.resolvers';
export const CompaniesRoutes: Route[] = [
    {
        path: '',
        component: CompaniesComponent,
        resolve: {
            companies: CompaniesResolver
        }
    }
];
