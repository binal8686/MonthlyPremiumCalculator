import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Occupation } from '../Class/occupation'
import { PremiumParameters } from '../Class/premiumparameters';

@Injectable({
  providedIn: 'root'
})
export class PremiumCalculatorService {
  ApiUrl = 'http://localhost:55487/Api';    
  constructor(private http:HttpClient) { }    

  getOccupations():Observable<Occupation[]>    
  {    
    return this.http.get<Occupation[]>(this.ApiUrl + '/MonthlyPremium/GetOccupationsList');    
  }  
  
  getPremiumValue(premiumparameters: PremiumParameters)   
  {    
    return this.http.post(this.ApiUrl + '/MonthlyPremium/GetMonthlyPremiumValue', premiumparameters);
  }  
}
