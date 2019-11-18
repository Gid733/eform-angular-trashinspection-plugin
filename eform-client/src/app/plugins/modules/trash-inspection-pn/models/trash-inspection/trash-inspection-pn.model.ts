import {Time} from '@angular/common';

export class TrashInspectionsPnModel {
  total: number;
  trashInspectionList: Array<TrashInspectionPnModel> = [];
  token: string;
  numOfElements: number;
  pageNum: number;
}

export class TrashInspectionPnModel {
  id: number;
  name: string;
  weighingNumber: string;
  date: Date;
  time: Date;
  registrationNumber: string;
  trashFraction: number;
  eakCode: number;
  producer: string;
  transporter: string;
  segment: string;
  installationName: string;
  mustBeInspected: boolean;
  token: string;
  relatedAreasIds: Array<number> = [];
  status: number;
  extendedInspection: boolean;
  isApproved: boolean;
  comment: string;
  workflowState: string;
  inspectionDone: boolean;
  sdkCaseId: string;
  sdkeFormId: string;
}
