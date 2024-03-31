import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { IncomeTaxCalculationResult } from 'src/app/models/incomeTaxCalculationResult';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class IncomeTaxCalculatorService {
  url='https://localhost:44340/api/TaxCalculator';

  constructor(private http: HttpClient) {}

  calculateTax(salary: number): Observable<IncomeTaxCalculationResult> {
    const requestBody = { salary: salary };
    return this.http.post<IncomeTaxCalculationResult>(this.url, requestBody);
  }
}
