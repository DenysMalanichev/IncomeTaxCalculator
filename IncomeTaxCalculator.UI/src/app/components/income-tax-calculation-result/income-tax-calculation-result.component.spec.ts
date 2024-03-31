import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IncomeTaxCalculationResultComponent } from './income-tax-calculation-result.component';

describe('IncomeTaxCalculationResultComponent', () => {
  let component: IncomeTaxCalculationResultComponent;
  let fixture: ComponentFixture<IncomeTaxCalculationResultComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [IncomeTaxCalculationResultComponent]
    });
    fixture = TestBed.createComponent(IncomeTaxCalculationResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
