#!/bin/bash
perl -pi -e '$_.="  },\n" if /INSERT ROUTES HERE/' src/app/plugins/plugins.routing.ts
perl -pi -e '$_.="      .then(m => m.TrashInspectionPnModule)\n" if /INSERT ROUTES HERE/' src/app/plugins/plugins.routing.ts
perl -pi -e '$_.="    loadChildren: () => import('\''./modules/trash-inspection-pn/trash-inspection-pn.module'\'')\n" if /INSERT ROUTES HERE/' src/app/plugins/plugins.routing.ts
perl -pi -e '$_.="    path: '\''trash-inspection-pn'\'',\n" if /INSERT ROUTES HERE/' src/app/plugins/plugins.routing.ts
perl -pi -e '$_.="  {\n" if /INSERT ROUTES HERE/' src/app/plugins/plugins.routing.ts
