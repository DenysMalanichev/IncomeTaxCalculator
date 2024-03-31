import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IncomeTaxCalculatorComponent } from './components/income-tax-calculator/income-tax-calculator.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: '/income-tax-calculator',
    pathMatch: 'full'
  },
  {
    path: 'income-tax-calculator',
    component: IncomeTaxCalculatorComponent
  },
  {
    path: '**',
    component: PageNotFoundComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
