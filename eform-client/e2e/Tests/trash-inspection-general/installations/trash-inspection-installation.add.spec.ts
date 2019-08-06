import {expect} from 'chai';
import loginPage from '../../../Page objects/Login.page';
import installationPage from '../../../Page objects/trash-inspection/TrashInspection-Installation.page';
import {Guid} from 'guid-typescript';

describe('Trash Inspection Plugin - Installation', function () {
  before(function () {
    loginPage.open('/auth');
    loginPage.login();
  });
  it('Should create installation without site.', function () {
    const name = Guid.create().toString();
    installationPage.goToInstallationsPage();
    installationPage.createInstallation_DoesntAddSite(name);
    const installation = installationPage.getFirstRowObject();
    expect(installation.name).equal(name);
    installationPage.deleteInstallation_Deletes();
  });
  it('should not create installation', function () {
    const name = Guid.create().toString();
    // installationPage.goToInstallationsPage();
    browser.pause(8000);
    installationPage.createInstallation_DoesntAddSite_Cancels(name);
    const installation = installationPage.getFirstRowObject();
    expect(installationPage.rowNum).equal(0);
  });
});
