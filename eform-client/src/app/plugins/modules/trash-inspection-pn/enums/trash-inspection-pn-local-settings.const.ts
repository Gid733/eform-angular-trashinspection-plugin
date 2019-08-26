import {
  ApplicationPageModel,
  PageSettingsModel
} from 'src/app/common/models/settings';

export const TrashInspectionPnLocalSettings = [
  new ApplicationPageModel({
      name: 'Installations',
      settings: new PageSettingsModel({
        pageSize: 10,
        sort: 'Id',
        isSortDsc: false
      })
    }
  ),
  new ApplicationPageModel({
      name: 'Fractions',
      settings: new PageSettingsModel({
        pageSize: 10,
        sort: '',
        isSortDsc: false
      })
    }
  ),
  new ApplicationPageModel({
      name: 'Segments',
      settings: new PageSettingsModel({
        pageSize: 10,
        sort: 'Id',
        isSortDsc: false
      })
    }
  ),
  new ApplicationPageModel({
      name: 'TrashInspections',
      settings: new PageSettingsModel({
        pageSize: 10,
        sort: 'Id',
        isSortDsc: false
      })
    }
  ),
  new ApplicationPageModel({
      name: 'Transporters',
      settings: new PageSettingsModel({
        pageSize: 10,
        sort: 'Id',
        isSortDsc: false
      })
    }
  ),
  new ApplicationPageModel({
      name: 'Producers',
      settings: new PageSettingsModel({
        pageSize: 10,
        sort: 'Id',
        isSortDsc: false
      })
    }
  ),
  new ApplicationPageModel({
      name: 'ProducersByYear',
      settings: new PageSettingsModel({
        pageSize: 10,
        sort: 'Name',
        isSortDsc: false
      })
    }
  ),
  new ApplicationPageModel({
      name: 'TransportersByYear',
      settings: new PageSettingsModel({
        pageSize: 10,
        sort: 'Transporter',
        isSortDsc: false
      })
    }
  ),
  new ApplicationPageModel({
      name: 'FractionsByYear',
      settings: new PageSettingsModel({
        pageSize: 10,
        sort: 'Name',
        isSortDsc: false
      })
    }
  )
];

