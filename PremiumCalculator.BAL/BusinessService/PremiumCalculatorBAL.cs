using PremiumCalculator.BAL.Interface;
using PremiumCalculator.BAL.Models;
using System;
using System.Threading.Tasks;

namespace PremiumCalculator.BAL.BusinessService
{
    public class PremiumCalculatorBAL : IPremiumCalculatorBAL
    {
        IOccupationBAL _occupationBAL;
        public PremiumCalculatorBAL(IOccupationBAL occupationBAL)
        {
            _occupationBAL = occupationBAL;
        }

        public async Task<PremiumParametersResponse> getMonthlyPremiumValue(PremiumParametersData premiumParamData)
        {
            var occupationFactor = await _occupationBAL.getOccupationFactor(premiumParamData.OccupationId);
            PremiumParametersResponse resp = new PremiumParametersResponse();
            var deathSum = premiumParamData.SumInsured.Substring(1, premiumParamData.SumInsured.Length - 1).Trim();
            decimal deathSumvalue;
            if (Decimal.TryParse(deathSum, out deathSumvalue))
            {
                resp.Premium = (deathSumvalue * occupationFactor * premiumParamData.Age) / (1000 * 12);
                resp.Message = "Monthly Premium is successfully calculated based on given information.";
            }
            else
            {
                resp.Premium = 0;
                resp.Message = "Internal Server Error! Please try again! (Or) Please make sure that Web API is running.";
            }
            
            return resp;
        }
    }
}
