export interface ReceiptDocument {
  id: string;
  number: number;
  createdAt: Date;
  receiptResources: ReceiptResource[];
}

export interface ReceiptResource {
  id: string;
  resourceId: string;
  resourceName: string;
  unitId: string;
  unitName: string;
  quantity: number;
}

export interface ReceiptDocumentFilter {
  dateFrom?: Date;
  dateTo?: Date;
  documentNumbers?: number[];
  resourceIds?: string[];
  unitIds?: string[];
}

export interface CreateReceiptDocumentRequest {
  number: number;
  createdAt: Date;
  receiptResources?: CreateReceiptResourceRequest[];
}

export interface CreateReceiptResourceRequest {
  resourceId: string;
  unitId: string;
  quantity: number;
}

//TODO: Нужно переделать под обновление всего документа и ресурсов одним запросом
export interface UpdateReceiptDocumentRequest {
  number: number;
  createdAt: Date;
}
