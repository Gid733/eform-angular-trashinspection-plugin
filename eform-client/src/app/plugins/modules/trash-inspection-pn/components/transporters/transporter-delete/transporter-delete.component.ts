import {Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {TransporterPnModel} from '../../../models/transporter';
import {TrashInspectionPnTransporterService} from '../../../services';
import {ProducerPnModel} from '../../../models/producer';

@Component({
  selector: 'app-trash-inspection-pn-transporter-delete',
  templateUrl: './transporter-delete.component.html',
  styleUrls: ['./transporter-delete.component.scss']
})
export class TransporterDeleteComponent implements OnInit {
  @ViewChild('frame', {static: false}) frame;
  @Output() onTransporterDeleted: EventEmitter<void> = new EventEmitter<void>();
  spinnerStatus = false;
  selectedTransporter: TransporterPnModel = new TransporterPnModel();
  constructor(private trashInspectionPnTransporterService: TrashInspectionPnTransporterService) { }

  ngOnInit() {
  }
  show(transporterModel: TransporterPnModel) {
    this.selectedTransporter = transporterModel;
    this.frame.show();
  }

  deleteTransporter() {
    this.spinnerStatus = true;
    this.trashInspectionPnTransporterService.deleteTransporter(this.selectedTransporter.id).subscribe((data) => {
      if (data && data.success) {
        this.onTransporterDeleted.emit();
        this.frame.hide();
      } this.spinnerStatus = false;
    });  }
}
