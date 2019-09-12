import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {ToastrService} from 'ngx-toastr';

import { Observable} from 'rxjs';
import {Router} from '@angular/router';
import {OperationDataResult, OperationResult} from 'src/app/common/models/operation.models';
import {BaseService} from 'src/app/common/services/base.service';

import {
  TransporterPnImportModel,
  TransporterPnModel,
  TransporterPnRequestModel,
  TransporterPnUpdateModel,
  TransportersPnModel, TransporterYearPnRequestModel,
} from '../models/transporter';
import {TrashInspectionYearModelPnModel} from '../models/trash-inspection/trash-inspectionYearModel-pn.model';
import {TransporterYearPnModel} from '../models/transporter/transporterYearPnModel';
import {StatByMonthPnModel} from '../models/transporter/statByMonthPnModel';

export let TrashInspectionPnTransporterMethods = {
  Transporter: 'api/trash-inspection-pn/transporters'
};

@Injectable({
  providedIn: 'root'
})
export class TrashInspectionPnTransporterService extends BaseService {
  constructor(private _http: HttpClient, router: Router, toastrService: ToastrService) {
    super(_http, router, toastrService);
  }
  getAllTransporters(model: TransporterPnRequestModel): Observable<OperationDataResult<TransportersPnModel>> {
    return this.get(TrashInspectionPnTransporterMethods.Transporter, model);
  }

  getSingleTransporter(transporterId: number): Observable<OperationDataResult<TransporterPnModel>> {
    return this.get(TrashInspectionPnTransporterMethods.Transporter + '/' + transporterId);
  }

  getSingleTransporterByMonth(transporterId: number, year: number): Observable<OperationDataResult<StatByMonthPnModel>> {
    return this.get(TrashInspectionPnTransporterMethods.Transporter  + '/' + transporterId + '/' + year);
  }

  updateTransporter(model: TransporterPnUpdateModel): Observable<OperationResult> {
    return this.put(TrashInspectionPnTransporterMethods.Transporter, model);
  }

  createTransporter(model: TransporterPnModel): Observable<OperationResult> {
    return this.post(TrashInspectionPnTransporterMethods.Transporter, model);
  }

  deleteTransporter(transporterId: number): Observable<OperationResult> {
    return this.delete(TrashInspectionPnTransporterMethods.Transporter + '/' + transporterId);
  }
  importTransporter(model: TransporterPnImportModel): Observable<OperationResult> {
    return this.post(TrashInspectionPnTransporterMethods.Transporter + '/import', model);
  }
  getAllTransportersByYear(model: TransporterYearPnRequestModel): Observable<OperationDataResult<TrashInspectionYearModelPnModel>> {
    return this.get(TrashInspectionPnTransporterMethods.Transporter + '/year/' + model.year, model);
  }
}
