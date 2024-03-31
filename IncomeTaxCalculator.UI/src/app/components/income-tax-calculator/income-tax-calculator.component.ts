import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { IncomeTaxCalculationResult } from 'src/app/models/incomeTaxCalculationResult';
import { IncomeTaxCalculatorService } from 'src/app/services/income-tax-calculator-service/income-tax-calculator.service';

@Component({
  selector: 'app-income-tax-calculator',
  templateUrl: './income-tax-calculator.component.html',
  styleUrls: ['./income-tax-calculator.component.css']
})
export class IncomeTaxCalculatorComponent {
  calculationResult?: IncomeTaxCalculationResult;
  salaryFormControl = new FormControl<number | null>(0);

  constructor(private taxCalculatorService: IncomeTaxCalculatorService) {}

  calculateTax(): void {
    const salary: number = this.salaryFormControl?.value ?? 0;

    this.taxCalculatorService
      .calculateTax(salary)
      .subscribe(x => this.calculationResult = { ...x});

      console.log(this.calculationResult);
  }
}
