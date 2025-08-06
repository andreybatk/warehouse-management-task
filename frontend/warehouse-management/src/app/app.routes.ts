import { Routes } from '@angular/router';
import { MainPageLayout } from './common-ui/main-page-layout/main-page-layout';
import { ReceiptDocuments } from './pages/receipt-documents/receipt-documents';
import { Units } from './pages/units/units';
import { Resources } from './pages/resources/resources';
import { ResourceDetails } from './pages/resource-details/resource-details';
import { UnitDetails } from './pages/unit-details/unit-details';
import { ResourceCreate } from './pages/resource-create/resource-create';
import { UnitCreate } from './pages/unit-create/unit-create';
import { ReceiptDocumentCreate } from './pages/receipt-document-create/receipt-document-create';
import { ReceiptDocumentDetails } from './pages/receipt-document-details/receipt-document-details';

export const routes: Routes = [
    {path: '', component: MainPageLayout, children: [
        { path: 'receipt-documents', component: ReceiptDocuments },
        { path: 'receipt-documents/create', component: ReceiptDocumentCreate },
        { path: 'receipt-documents/:id', component: ReceiptDocumentDetails },
        { path: 'resources', component: Resources},
        { path: 'resources/create', component: ResourceCreate },
        { path: 'resources/:id', component: ResourceDetails },
        { path: 'units', component: Units },
        { path: 'units/create', component: UnitCreate },
        { path: 'units/:id', component: UnitDetails },
    ]}
];