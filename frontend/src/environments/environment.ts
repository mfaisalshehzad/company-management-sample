// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
    production: false,
    api: {
        host: 'https://localhost:7086/',
        loginPath: 'api/auth/login',
        getUserPath: 'api/auth/get-user',
        changePasswordPath: 'api/auth/change-password',
        compniesAllListPath: 'api/company/get-all',
        companyGetOnePath: 'api/company/',
        compniesFilterListPath: 'api/company/get-list',        
        createOrUpdatePath: 'api/company/create-or-update'
    }
};

