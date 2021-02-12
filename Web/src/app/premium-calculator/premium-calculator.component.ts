import { Component, OnInit, ViewChild, AfterViewChecked } from '@angular/core';
import { PremiumCalculatorService } from '../Service/premium-calculator.service';
import { Occupation } from '../Class/occupation';
import { NgForm } from '@angular/forms';
import { PremiumParameters } from '../Class/premiumparameters';
import { isNumber } from 'util';
import { CurrencyPipe } from '@angular/common';
import {formatDate} from '@angular/common';

@Component({
  selector: 'app-premium-calculator',
  templateUrl: './premium-calculator.component.html',
  styleUrls: ['./premium-calculator.component.less']
})
export class PremiumCalculatorComponent implements OnInit, AfterViewChecked {
  eMessage = '';
  tomorrow = new String();
  @ViewChild('premiumForm', {static: false}) currentForm: NgForm;
  
  premiumForm: NgForm;
  monthlyPremium = 0.0;
  isError: boolean = false;
  allOccupations: Occupation[];
  premiumParamModel:PremiumParameters; 

  constructor(private premiumCalculatorService: PremiumCalculatorService, 
    private currencyPipe: CurrencyPipe) {
      this.tomorrow = formatDate(new Date(), 'yyyy-MM-dd', 'en');
     }

  ngOnInit() {
    this.premiumParamModel =  new PremiumParameters();
    this.loadAllOccupations();
  }

  ngAfterViewChecked() {
    this.formChanged();
  }

  



  formChanged() {
    if(this.currentForm == this.premiumForm){
      return;
    }
    this.premiumForm = this.currentForm;

    if(this.premiumForm) {
      this.premiumForm.valueChanges.subscribe(
        data => this.onValueChanged(data)
      );
    }
  }

  onValueChanged(data? : any){
    if(!this.premiumForm) { return; }
    const form = this.premiumForm.form;
    for(const field in this.formErrors){
      const control = form.get(field);
      this.formErrors[field] = '';
      if(control && control.dirty &&  !control.valid){
        for(const key in control.errors){
          this.formErrors[field]+= this.validationMessages[field][key];
        }
      }
    }
  }


  formErrors = {
    'Name': '',
    'DOB': '',
    'Age': '',
    'DeathInsured':''
  }

  validationMessages = {
    'Name': {
      'required': 'Name is required.',
      'pattern': 'Only Alphabets are allowed.'
    },
    'DOB':  {
      'required': 'DOB is required.',
      'pattern': 'Enter a valid date'
    },
    'Age':  {
      'required': 'Age is required.',
      'pattern': 'Age should not be negative | should not be greater than 150'
    },
    'DeathInsured': {
      'required': 'Death Sum Insured is required.',
    },
  }

  loadAllOccupations() {
    this.premiumCalculatorService.getOccupations().subscribe(result => {
      this.allOccupations = result;
    }, error => {
        // we can catch different exceptions according to error codes
        // for now just implemented general exception
        console.log(error);
        this.isError = true;
      });
  }


  convertToCurrency(SumInsured: string) {
   
    this.premiumParamModel.SumInsured = this.currencyPipe.transform(SumInsured.replace(/\D/g, '').replace(/^0+/,'') ,'$', '$ ','1.2-2')
    
  }



  calculatePremiumValue(occupationId: number) {
      if (occupationId == 0) this.monthlyPremium = 0;
        if(this.premiumForm.form.valid){
        this.premiumParamModel.OccupationId = occupationId;
        this.premiumCalculatorService.getPremiumValue(this.premiumParamModel).subscribe({
          next: (data: any )=> {
              this.monthlyPremium = data.Premium;
              this.eMessage = data.Message;
          },
          error: error => {
              this.eMessage = error.message;
              console.error('There was an error!', error);
          }
      })
    }
  }


  calculateAge(DOB : Date)
  { 
    if(DOB){
      const bdate = new Date(DOB);
      var timeDiff = Math.abs(Date.now() - new Date(bdate).getTime())
      //Used Math.floor instead of Math.ceil
      //so 26 years and 140 days would be considered as 26, not 27.
      this.premiumParamModel.Age = Math.floor(timeDiff / (1000 * 3600 * 24) / 365.25);
    }
  }
  checkFutureDaet(DOB: Date)
  {

    if(DOB)
    {
      const bdate = new Date(DOB);
      
      

    }

  }
}
