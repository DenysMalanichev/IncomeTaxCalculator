import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IncomeTaxCalculationResultComponent } from './income-tax-calculation-result.component';
import { IncomeTaxCalculationResult } from 'src/app/models/incomeTaxCalculationResult';
import { By } from '@angular/platform-browser';

describe('IncomeTaxCalculationResultComponent', () => {
  let component: IncomeTaxCalculationResultComponent;
  let fixture: ComponentFixture<IncomeTaxCalculationResultComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IncomeTaxCalculationResultComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IncomeTaxCalculationResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display tax calculation result when provided', () => {
    const testResult: IncomeTaxCalculationResult = {    
      grossAnnualSalary: 40000,
      grossMonthlySalary: 3333.33,
      netAnnualSalary: 29000,
      netMonthlySalary: 2416.67,
      annualTaxPaid: 11000.00,
      monthlyTaxPaid:  916.67,
    };
    component.calculationResult = testResult;
    fixture.detectChanges();

    const resultElement = fixture.debugElement.query(By.css('.result-value'));
  });
});
