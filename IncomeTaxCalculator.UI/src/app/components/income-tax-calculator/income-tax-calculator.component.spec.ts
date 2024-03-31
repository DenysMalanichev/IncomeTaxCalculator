import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MatSnackBar } from '@angular/material/snack-bar';

import { IncomeTaxCalculatorComponent } from './income-tax-calculator.component';
import { IncomeTaxCalculatorService } from 'src/app/services/income-tax-calculator-service/income-tax-calculator.service';
import { FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { of } from 'rxjs';
import { IncomeTaxCalculationResult } from 'src/app/models/incomeTaxCalculationResult';

describe('IncomeTaxCalculatorComponent', () => {
  let component: IncomeTaxCalculatorComponent;
  let fixture: ComponentFixture<IncomeTaxCalculatorComponent>;
  let mockTaxCalculatorService: jasmine.SpyObj<IncomeTaxCalculatorService>;
  let mockSnackBar: jasmine.SpyObj<MatSnackBar>;

  beforeEach(async () => {
    mockTaxCalculatorService = jasmine.createSpyObj('IncomeTaxCalculatorService', ['calculateTax']);
    mockSnackBar = jasmine.createSpyObj('MatSnackBar', ['open']);

    await TestBed.configureTestingModule({
      declarations: [ IncomeTaxCalculatorComponent ],
      imports: [ ReactiveFormsModule, FormsModule ],
      providers: [
        { provide: IncomeTaxCalculatorService, useValue: mockTaxCalculatorService },
        { provide: MatSnackBar, useValue: mockSnackBar }
      ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IncomeTaxCalculatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should not call calculateTax if salary is invalid', () => {
    component.salaryFormControl = new FormControl(-1, [
      Validators.required,
      Validators.min(0.00)
    ]);
    component.calculateTax();
    expect(mockTaxCalculatorService.calculateTax).not.toHaveBeenCalled();
    expect(mockSnackBar.open).toHaveBeenCalledWith('Gross Annual Salary can not be a negative number', 'OK', {
      duration: 3000,
      panelClass: ['error-snackbar'],
    });
  });

  it('should call calculateTax and update calculationResult for valid salary', () => {
    const mockResponse: IncomeTaxCalculationResult = {    
      grossAnnualSalary: 40000,
      grossMonthlySalary: 3333.33,
      netAnnualSalary: 29000,
      netMonthlySalary: 2416.67,
      annualTaxPaid: 11000.00,
      monthlyTaxPaid:  916.67,
    };
    mockTaxCalculatorService.calculateTax.and.returnValue(of(mockResponse));

    component.salaryFormControl.setValue(60000);
    component.calculateTax();

    expect(mockTaxCalculatorService.calculateTax).toHaveBeenCalledWith(60000);
    expect(component.calculationResult).toEqual(mockResponse);
  });
});
