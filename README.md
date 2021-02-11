# MonthlyPremiumCalculator
   
Need to create project to calculate premium based on given inputs.

# Details:
As a Member I would like to have an ability to choose various options on the screen So that I can view the monthly premiums calculated and displayed on the screen

 Develop an UI which accepts the below data and return a monthly premium amount to be calculated.

Name
Age
Date of Birth
Occupation
Death – Sum Insured.
 

The UI provides a below list of occupations


## Occupation List 
Occupation  Rating
_ _ _ _ _ _ _ _ _ _
Cleaner  |  Light Manual

Doctor   |  Professional

Author   |  White Collar

Farmer   |  Heavy Manual

Mechanic |  Heavy Manual

Florist  |  Light Manual

 There is a factor associated with each rating as below,

## Occupation Rating

Rating        ==>     Factor
_ _ _ _ _ _ _ _ _ _ _ _ _
Professional   ==>    1.0

White Collar   ==>    1.25

Light Manual   ==>    1.50

Heavy Manual   ==>    1.75


NEED TO COSIDER BELOW POINTS WHILE 

 -> For any given individual, the monthly premium is calculated using the below formula
    
    Death Premium = (Death Cover amount * Occupation Rating Factor * Age) /1000 * 12

 -> All input fields are mandatory.
 
 -> Given all the input fields are specified, change in the occupation dropdown should trigger the premium calculation
 
 
     
 ## Images
  
  1) Monthly Premium Calculator (image 1)
  
 <img src="/Images/UI%20Page%20look.png">
 
 2) Monthly Premium Calculator - Screen while error in WebAPI Integration (image 2)
  
 <img src="/Images/UI%20Page%20with%20error.png">
 
 
 
 ## Design of Solution
 There are different layers in this solution:
 - Data layer 
 - Business layer
 - WebAPI 
 - Web UI
 - WebAPI Integration Testing via Postman
 
 ## Data Layer - Data.MonthlyPremiumCal
 - Use EF to handle Database
 
 ## Business layer
 - To calculate premium based on given input
 
 ## WebAPI
 - Use of WebAPI2
 - Create master tables to get given Occupation and Occupation Factors
 - Used dependency injection with Autofac package 
 - Exception handling and Error log using Elmah. And log saving at C:\\ElmahLog_MonthlyPremium
 
 ## Web UI
 - Use of Angular 8 with Angular CLI
 - Used bootstrap for the application and less for css
 - Run this with -np serve which provide http://localhost:4200/ to view UI.
 
 ## WebAPI Integration Testing via Postman
 
 1) To get all the Occupation list 
  Steps to follow:
  - Open Postname app on desktop
  - Open New Tab with GET method
  - Enter URL - http://localhost:55487/api/MonthlyPremium/GetOccupationsList
  - Click on Send button
  This will provide you list of Occupations in JSON string
  
  
  
  2) To get Monthly Premium based on given input
  Steps to follow:
   - Open Postname app on desktop
  -  Open New Tab with POST method
  - Enter URL - http://localhost:55487/api/MonthlyPremium/GetMonthlyPremiumValue
  - Go to Body and select x-www-form-urlencoded
  - Enter parameters like,  
      --Age (int) 
      --OccupationId (int)
      --SumInsured  (long)
            
  - Click on Send button
  This will provide you calculator monthly premium.
  
  
 # Steps to run this Monthly Premium Calculator Project:
     Download MonthlyPremiumCalculator project code from GitHub shared link.
     1)	There are three main project files 
     -	Data.MonthlyPremiumCal
     -	Business.MonthlyPremiumCal
     -	PremiumCalculator.WebAPI

     2)	Add then into solution and add references if needed. 
     3)	And, run the project

     Now, to run UI part (Web) 
     ## Prerequisite 
        - Install Visual Studio Code
        - Install Node and NPM
        - Install Angular CLI (Latest version) 

     1)	Open Visual Studio Code 
     2)	Open Folder – Web from MonthlyPremiumCalculator project
     3)	Open Terminal (Terminal – New Terminal)
     4)	Enter command – ng serve
     5)	It will compile and give your port to run. E.g., http://localhost:4200/
     6)	Open that link and you will see above screen (image 1).
     7)	Fill up the input box and at last select Occupation.
     8)	Finally, you will get your Monthly Premium.     
     9)      Above screen (image 2) to have issue with WebAPI integration.



   ## References 
   
   - For [Dependency Injection with Autofac package](https://github.com/autofac/Autofac.Extensions.DependencyInjection) 
   - For [Postman to Test WebAPI](https://www.postman.com/product/api-client/) 
   - To run [Angular application](https://github.com/angular/angular-cli)
