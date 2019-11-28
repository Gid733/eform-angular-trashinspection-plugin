import {expect} from 'chai';
import loginPage from '../../../Page objects/Login.page';
import {Guid} from 'guid-typescript';
import fractionsPage from '../../../Page objects/trash-inspection/TrashInspection-Fraction.page';
import pluginsPage from '../../trash-inspections-settings/application-settings.plugins.page';
import myEformsPage from '../../../Page objects/MyEforms.page';

describe('Trash Inspection Plugin - Fraction', function () {
  before(function () {
    loginPage.open('/auth');
    loginPage.login();
  });
  it('should check if activated', function () {
    myEformsPage.Navbar.advancedDropdown();
    myEformsPage.Navbar.clickonSubMenuItem('Plugins');
    // browser.pause(8000);
    browser.waitForVisible('#plugin-id', 10000);
    const plugin = pluginsPage.getFirstPluginRowObj();
    expect(plugin.id).equal(1);
    expect(plugin.name).equal('Microting Trash Inspection Plugin');
    expect(plugin.version).equal('1.0.0.0');
  });
  it('should check if menupoint is there', function () {
    expect(fractionsPage.trashInspectionDropdownName.getText()).equal('Affaldsinspektion');
    fractionsPage.trashInspectionDropDown();
    // browser.pause(4000);
    browser.pause(2000);
    browser.waitForVisible('#trash-inspection-pn-fractions', 10000);
    expect(fractionsPage.fractionBtn.getText()).equal('Fraktioner');
    browser.pause(4000);
    fractionsPage.trashInspectionDropDown();
    browser.refresh();
  });
  it('should get btn text', function () {
    // browser.waitForVisible('#plugin-id', 10000);
    browser.pause(10000);
    fractionsPage.goToFractionsPage();
    fractionsPage.getBtnTxt('Ny Fraktion');
  });
  it('should create fraction', function () {
    const name = Guid.create().toString();
    const description = Guid.create().toString();
    fractionsPage.createFraction(name, description);
    const fraction = fractionsPage.getFirstRowObject();
    expect(fraction.name).equal(name);
    expect(fraction.description).equal(description);
    expect(fraction.eForm).equal('Number 1');
  });
  it('should not delete fraction', function () {
    fractionsPage.goToFractionsPage();
    const fraction = fractionsPage.getFirstRowObject();
    const oldName = fraction.name;
    const oldDescription = fraction.description;
    fractionsPage.cancelDeleteFraction();
    browser.pause(8000);
    const fractionAfterCancelDelete = fractionsPage.getFirstRowObject();
    expect(fractionAfterCancelDelete.name).equal(oldName);
    expect(fractionAfterCancelDelete.description).equal(oldDescription);
  });
  it('should delete Fraction', function () {
    fractionsPage.goToFractionsPage();
    fractionsPage.deleteFraction();
    browser.pause(8000);
    const fraction = fractionsPage.getFirstRowObject();
    expect(fractionsPage.rowNum).equal(0);
  });

});
