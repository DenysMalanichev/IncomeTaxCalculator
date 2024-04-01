import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { IncomeTaxCalculatorService } from './income-tax-calculator.service';
import { IncomeTaxCalculationResult } from 'src/app/models/incomeTaxCalculationResult';
import { environment } from 'src/environments/environment.development';

describe('IncomeTaxCalculatorService', () => {
  let service: IncomeTaxCalculatorService;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [IncomeTaxCalculatorService]
    });
    service = TestBed.inject(IncomeTaxCalculatorService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('#calculateTax should return expected tax calculation result (POST request)', () => {
    const expectedData: IncomeTaxCalculationResult = {    
      grossAnnualSalary: 40000,
      grossMonthlySalary: 3333.33,
      netAnnualSalary: 29000,
      netMonthlySalary: 2416.67,
      annualTaxPaid: 11000.00,
      monthlyTaxPaid:  916.67,
    };

    service.calculateTax(40000).subscribe(data =>
      expect(data).toEqual(expectedData),
      fail
    );

    const req = httpTestingController.expectOne(environment.apiBaseUrl + environment.endpoints.taxCalculator);
    expect(req.request.method).toEqual('POST');
    expect(req.request.body).toEqual({ salary: 40000 });
    
    req.flush(expectedData);
  });
});
