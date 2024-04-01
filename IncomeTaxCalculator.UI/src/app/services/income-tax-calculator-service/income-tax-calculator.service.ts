import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { IncomeTaxCalculationResult } from 'src/app/models/incomeTaxCalculationResult';
import { Observable, map } from 'rxjs';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class IncomeTaxCalculatorService {

  private apiUrl = environment.apiBaseUrl + environment.endpoints.taxCalculator;

  constructor(private http: HttpClient) {}

  calculateTax(salary: number): Observable<IncomeTaxCalculationResult> {
    const requestBody = { salary: salary };
    return this.http.post<IncomeTaxCalculationResult>(this.apiUrl, requestBody);
  }
}
