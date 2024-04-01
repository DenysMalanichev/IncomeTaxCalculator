import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { IncomeTaxCalculationResult } from 'src/app/models/incomeTaxCalculationResult';
import { IncomeTaxCalculatorService } from 'src/app/services/income-tax-calculator-service/income-tax-calculator.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-income-tax-calculator',
  templateUrl: './income-tax-calculator.component.html',
  styleUrls: ['./income-tax-calculator.component.css']
})
export class IncomeTaxCalculatorComponent {
  calculationResult?: IncomeTaxCalculationResult;
  salaryFormControl = new FormControl<number | null>(0, [
    Validators.required,
    Validators.min(0.00)
  ]);

  constructor(
    private taxCalculatorService: IncomeTaxCalculatorService,
    private snackBar: MatSnackBar
    ) {}

  calculateTax(): void {
    if(this.salaryFormControl.valid) {
      const salary: number = this.salaryFormControl?.value ?? 0;

      this.taxCalculatorService
        .calculateTax(salary)
        .subscribe(x => this.calculationResult = { ...x});
    }
    else {
      this.showError('Gross Annual Salary can not be a negative number');
    } 
  }

  showError(msg: string) {
    this.snackBar.open(msg, 'OK', {
      duration: 3000,
      panelClass: ['error-snackbar'],
    });
  }
}
