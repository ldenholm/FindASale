export class Result {
    success: boolean;
    assignedSalesPerson: SalesPerson;
    errorMessage: string;
}

export class SalesPerson {
    name: string;
    groups: string[];
    isAvailable: boolean;
}

export class ResetResult {
    resetMessage: string;
}