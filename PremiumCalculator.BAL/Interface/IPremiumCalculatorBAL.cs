using PremiumCalculator.BAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumCalculator.BAL.Interface
{
    public interface IPremiumCalculatorBAL
    {
       Task<PremiumParametersResponse> getMonthlyPremiumValue(PremiumParametersData premiumParamData);
    }
}
