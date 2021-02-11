using Elmah;
using PremiumCalculator.BAL.BusinessService;
using PremiumCalculator.BAL.Interface;
using PremiumCalculator.BAL.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PremiumCalculator.WebAPI.Controllers
{
    public class MonthlyPremiumController : ApiController
    {
        IOccupationBAL _occupationBAL;
        IPremiumCalculatorBAL _premiumCalculatorBAL;

        public MonthlyPremiumController(IOccupationBAL occupationBAL, IPremiumCalculatorBAL premiumCalculatorBAL)
        {
            _occupationBAL = occupationBAL;
            _premiumCalculatorBAL = premiumCalculatorBAL;
        }


        public string Index()
        {

            try
            {
                return "API has started successfully";

            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                return "There is an issue starting the API";
            }
        }
        [HttpGet]
        [ActionName("GetOccupationsList")]
        public async Task<IHttpActionResult> GetOccupationsList()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _occupationBAL.getAllOccupationsList();
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, result));
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex));
            }
        }

        [HttpPost]
        [ActionName("GetMonthlyPremiumValue")]
        public async Task<IHttpActionResult> GetMonthlyPremiumValue([FromBody]PremiumParametersData premiumParametersData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _premiumCalculatorBAL.getMonthlyPremiumValue(premiumParametersData);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, result));
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex));
            }
        }

    }
}
