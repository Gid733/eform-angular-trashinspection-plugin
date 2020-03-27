import {Component, OnInit, ViewChild} from '@angular/core';
import {PageSettingsModel} from 'src/app/common/models/settings';
import {
  TrashInspectionsPnRequestModel,
  TrashInspectionsPnModel,
  TrashInspectionPnModel
} from '../../../models';
import {TrashInspectionPnSettingsService, TrashInspectionPnTrashInspectionsService} from '../../../services';
import {SharedPnService} from '../../../../shared/services';
import {AuthService} from '../../../../../../common/services/auth';

@Component({
  selector: 'app-trash-inspection-pn-trash-inspection-page',
  templateUrl: './trash-inspections-page.component.html',
  styleUrls: ['./trash-inspections-page.component.scss']
})
export class TrashInspectionsPageComponent implements OnInit {
  @ViewChild('createTrashInspectionModal', {static: false}) createTrashInspectionModal;
  @ViewChild('editTrashInspectionModal', {static: false}) editTrashInspectionModal;
  @ViewChild('deleteTrashInspectionModal', {static: false}) deleteTrashInspectionModal;
  @ViewChild('versionViewModal', {static: false}) versionViewModal;
  localPageSettings: PageSettingsModel = new PageSettingsModel();
  trashInspectionsModel: TrashInspectionsPnModel = new TrashInspectionsPnModel();
  trashInspectionsRequestModel: TrashInspectionsPnRequestModel = new TrashInspectionsPnRequestModel();
  // settingsModel: TrashInspectionBaseSettingsModel = new TrashInspectionBaseSettingsModel();
  spinnerStatus = false;

  constructor(private sharedPnService: SharedPnService,
              private trashInspectionPnSettingsService: TrashInspectionPnSettingsService,
              private trashInspectionPnTrashInspectionsService: TrashInspectionPnTrashInspectionsService,
              private authService: AuthService) { }
  get currentRole(): string {
    return this.authService.currentRole;
  }

  ngOnInit() {
    this.getLocalPageSettings();
  }

  getLocalPageSettings() {
    this.localPageSettings = this.sharedPnService.getLocalPageSettings
    ('trashInspectionsPnSettings', 'TrashInspections').settings;
    this.getAllInitialData();
  }

  updateLocalPageSettings() {
    this.sharedPnService.updateLocalPageSettings
    ('trashInspectionsPnSettings', this.localPageSettings, 'TrashInspections');
    this.getLocalPageSettings();
  }

  getAllInitialData() {
    this.getAllTrashInspections();
  }

  getAllTrashInspections() {
    this.spinnerStatus = true;
    this.trashInspectionsRequestModel.pageSize = this.localPageSettings.pageSize;
    this.trashInspectionsRequestModel.sort = this.localPageSettings.sort;
    this.trashInspectionsRequestModel.isSortDsc = this.localPageSettings.isSortDsc;
    this.trashInspectionPnTrashInspectionsService.getAllTrashInspections(this.trashInspectionsRequestModel).subscribe((data) => {
      if (data && data.success) {
        this.trashInspectionsModel = data.model;
      } this.spinnerStatus = false;
    });
  }

  showCreateTrashInspection() {
    this.createTrashInspectionModal.show();
  }

  showDeleteTrashInspectionModal(trashInspection: TrashInspectionPnModel) {
    this.deleteTrashInspectionModal.show(trashInspection);
  }

  showVersionViewModal(trashInspectionId: number) {
    this.versionViewModal.show(trashInspectionId);
  }
  downloadPDF(trashInspection: any) {
    window.open('/api/trash-inspection-pn/inspection-results/' +
      trashInspection.weighingNumber + '?token=' + trashInspection.token + '&fileType=pdf', '_blank');
  }

  downloadDocx(trashInspection: any) {
    window.open('/api/trash-inspection-pn/inspection-results/' +
      trashInspection.weighingNumber + '?token=' + trashInspection.token + '&fileType=docx', '_blank');
  }



  sortTable(sort: string) {
    if (this.localPageSettings.sort === sort) {
      this.localPageSettings.isSortDsc = !this.localPageSettings.isSortDsc;
    } else {
      this.localPageSettings.isSortDsc = false;
      this.localPageSettings.sort = sort;
    }
    this.updateLocalPageSettings();
  }

  changePage(e: any) {
    if (e || e === 0) {
      this.trashInspectionsRequestModel.offset = e;
      if (e === 0) {
        this.trashInspectionsRequestModel.pageIndex = 0;
      } else {
        this.trashInspectionsRequestModel.pageIndex
          = Math.floor(e / this.trashInspectionsRequestModel.pageSize);
      }
      this.getAllTrashInspections();
    }
  }
}
