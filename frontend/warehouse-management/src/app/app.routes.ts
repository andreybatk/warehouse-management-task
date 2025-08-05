import { Routes } from '@angular/router';
import { MainPageLayout } from './common-ui/main-page-layout/main-page-layout';
import { ReceiptDocuments } from './pages/receipt-documents/receipt-documents';
import { Units } from './pages/units/units';
import { Resources } from './pages/resources/resources';
import { ResourceDetails } from './pages/resource-details/resource-details';
import { UnitDetails } from './pages/unit-details/unit-details';

export const routes: Routes = [
    {path: '', component: MainPageLayout, children: [
        { path: 'receipt-documents', component: ReceiptDocuments },
        { path: 'resources', component: Resources},
        { path: 'resources/:id', component: ResourceDetails },
        { path: 'units', component: Units },
        { path: 'units/:id', component: UnitDetails },
    ]}
];