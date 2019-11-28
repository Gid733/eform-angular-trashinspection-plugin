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
  it('should create eForm nr. 2', function () {
    myEformsPage.createNewEform('Number 2');
  });
  it('should check if activated', function () {
    browser.pause(4000);
    myEformsPage.Navbar.advancedDropdown();
    myEformsPage.Navbar.clickonSubMenuItem('Plugins');
    // browser.pause(8000);
    browser.waitForVisible('#plugin-id', 20000);
    const plugin = pluginsPage.getFirstPluginRowObj();
    expect(plugin.id).equal(1);
    expect(plugin.name).equal('Microting Trash Inspection Plugin');
    expect(plugin.version).equal('1.0.0.0');
  });
  it('should check if menupoint is there', function () {
    expect(fractionsPage.trashInspectionDropdownName.getText()).equal('Affaldsinspektion');
    fractionsPage.trashInspectionDropDown();
    browser.pause(4000);
    // browser.waitForVisible('#trash-inspection-pn-fractions', 10000);
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
  });
  it('should edit Fraction', function () {
    fractionsPage.goToFractionsPage();
    const newName = Guid.create().toString();
    const newDescription = Guid.create().toString();
    fractionsPage.editFraction(newName, newDescription);
    const fraction = fractionsPage.getFirstRowObject();
    expect(fraction.name).equal(newName);
    expect(fraction.description).equal(newDescription);
    expect(fraction.eForm).equal('Number 2');
  });
  it('should not edit fraction', function () {
    fractionsPage.goToFractionsPage();
    const fraction = fractionsPage.getFirstRowObject();
    const newName = Guid.create().toString();
    const newDescription = Guid.create().toString();
    const oldName = fraction.name;
    const oldDescription = fraction.description;
    fractionsPage.cancelEditFraction(newName, newDescription);
    const fractionAfterCancelEdit = fractionsPage.getFirstRowObject();
    expect(fractionAfterCancelEdit.name).equal(oldName);
    expect(fractionAfterCancelEdit.description).equal(oldDescription);
    expect(fractionAfterCancelEdit.eForm).equal('Number 2');
  });
  it('should clean up', function () {
    browser.refresh();
    const fraction = fractionsPage.getFirstRowObject();
    fraction.deleteBtn.click();
    browser.waitForVisible('#fractionDeleteDeleteBtn', 20000);
    fractionsPage.fractionDeleteDeleteBtn.click();
    browser.refresh();
    expect(fractionsPage.rowNum).equal(0);
  });
});
