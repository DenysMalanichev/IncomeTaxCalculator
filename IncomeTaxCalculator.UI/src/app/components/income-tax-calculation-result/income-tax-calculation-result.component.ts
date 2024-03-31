import { Component, Input } from '@angular/core';
import { IncomeTaxCalculationResult } from 'src/app/models/incomeTaxCalculationResult';

@Component({
  selector: 'income-tax-calculation-result',
  templateUrl: './income-tax-calculation-result.component.html',
  styleUrls: ['./income-tax-calculation-result.component.css']
})
export class IncomeTaxCalculationResultComponent {
  @Input()
  calculationResult?: IncomeTaxCalculationResult;
}
