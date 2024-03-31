import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { IncomeTaxCalculatorComponent } from './components/income-tax-calculator/income-tax-calculator.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { IncomeTaxCalculationResultComponent } from './components/income-tax-calculation-result/income-tax-calculation-result.component';

@NgModule({
  declarations: [
    AppComponent,
    IncomeTaxCalculatorComponent,
    PageNotFoundComponent,
    IncomeTaxCalculationResultComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
