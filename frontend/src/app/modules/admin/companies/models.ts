export interface Company {
    id?: string | null;
    companyNo?: number | null;
    companyName: string;
    industry: IndustryTypeEnum;
    numberOfEmployees: number;
    city: string;
    parentCompanyId?: string | null;
}


export interface CompanyViewModel {
    id: string;
    companyNo: number;
    companyName: string;
    industry: IndustryTypeEnum;
    numberOfEmployees: number;
    city: string;
    parentCompanyId?: string;
    parentCompanyName?: string | null;
    companyLevel?: number | null;
}

export interface CompanyFilter {
    companyNo?: number | null;
    companyName?: string | null;
    industry?: IndustryTypeEnum | null;
    city?: string | null;
    parentCompany?: string | null;
}

export interface CompanyPagination {
    totalRecords: number;
    currentPageRecords: number;
    pageSize: number;
    pageIndex: number;
    startIndex: number;
    endIndex: number;
    sortBy: string;
    sortOrder: string;
}

export enum IndustryTypeEnum {
    MeatProcessing = "Meat processing",
    GardeningAndLandscaping = "Gardening and landscaping",
    ITServices = "IT services",
    AerospaceTechnology = "Aerospace technology",
    ConsumerElectronics = "Consumer electronics"
}

export enum SortOrderEnum {
    Ascending = "asc",
    Descending = "desc"
}


