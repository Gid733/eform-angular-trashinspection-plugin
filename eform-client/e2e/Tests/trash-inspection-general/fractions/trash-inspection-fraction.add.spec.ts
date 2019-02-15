import {expect} from 'chai';
import loginPage from '../../../Page objects/Login.page';
import {Guid} from 'guid-typescript';
import fractionsPage from '../../../Page objects/trash-inspection/TrashInspection-Fraction.page';
import Nyan = Mocha.reporters.Nyan;

describe('Trash Inspection Plugin - Fraction', function () {
  before(function () {
    loginPage.open('/auth');
    loginPage.login();
  });
  it('Should create Fraction', function () {
    const name = Guid.create().toString();
    const description = Guid.create().toString();
    const newEformLabel = 'Number 1';
    fractionsPage.createNewEform(newEformLabel);
    fractionsPage.goToFractionsPage();
    fractionsPage.getBtnTxt('Ny Fraktion');
    fractionsPage.createFraction(name, description, 'Ny Fraktion');
    const fraction = fractionsPage.getFirstRowObject();
    expect(fraction.name).equal(name);
    expect(fraction.description).equal(description);
  });
});
