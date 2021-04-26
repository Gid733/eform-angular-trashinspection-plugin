import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import {
  OperationDataResult,
  OperationResult,
} from 'src/app/common/models/operation.models';
import { BaseService } from 'src/app/common/services/base.service';
import {
  FractionPnImportModel,
  FractionPnModel,
  FractionPnRequestModel,
  FractionPnStatsByYearModel,
  FractionPnUpdateModel,
  FractionPnYearRequestModel,
  StatByMonthPnModel,
} from '../models';
import { Paged } from 'src/app/common/models';

export let TrashInspectionPnFractionMethods = {
  Fractions: 'api/trash-inspection-pn/fractions',
  FractionsIndex: 'api/trash-inspection-pn/fractions/index',
  FractionsStatsByYear: 'api/trash-inspection-pn/fractions/stats-by-year',
  FractionsImport: 'api/trash-inspection-pn/fractions/import',
};
@Injectable({
  providedIn: 'root',
})
export class TrashInspectionPnFractionsService extends BaseService {
  constructor(
    private _http: HttpClient,
    router: Router,
    toastrService: ToastrService
  ) {
    super(_http, router, toastrService);
  }

  getAllFractions(
    model: FractionPnRequestModel
  ): Observable<OperationDataResult<Paged<FractionPnModel>>> {
    return this.post(TrashInspectionPnFractionMethods.FractionsIndex, model);
  }

  getSingleFraction(
    fractionId: number
  ): Observable<OperationDataResult<FractionPnModel>> {
    return this.get(
      TrashInspectionPnFractionMethods.Fractions + '/' + fractionId
    );
  }

  getSingleFractionByMonth(
    fractionId: number,
    year: number
  ): Observable<OperationDataResult<StatByMonthPnModel>> {
    return this.get(
      TrashInspectionPnFractionMethods.Fractions + '/' + fractionId + '/' + year
    );
  }

  updateFraction(model: FractionPnUpdateModel): Observable<OperationResult> {
    return this.put(TrashInspectionPnFractionMethods.Fractions, model);
  }

  createFraction(model: FractionPnModel): Observable<OperationResult> {
    return this.post(TrashInspectionPnFractionMethods.Fractions, model);
  }

  deleteFraction(fractionId: number): Observable<OperationResult> {
    return this.delete(
      TrashInspectionPnFractionMethods.Fractions + '/' + fractionId
    );
  }
  importFraction(model: FractionPnImportModel): Observable<OperationResult> {
    return this.post(TrashInspectionPnFractionMethods.FractionsImport, model);
  }

  getAllFractionsStatsByYear(
    model: FractionPnYearRequestModel
  ): Observable<OperationDataResult<Paged<FractionPnStatsByYearModel>>> {
    return this.post(
      TrashInspectionPnFractionMethods.FractionsStatsByYear,
      model
    );
  }
}
