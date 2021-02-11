 using Microsoft.VisualStudio.TestTools.UnitTesting;
using PremiumCalculator.BAL.BusinessService;
using PremiumCalculator.BAL.Interface;
using PremiumCalculator.BAL.Models;
using PremiumCalculator.DAL.DataModel;
using PremiumCalculator.DAL.Interface;
using PremiumCalculator.DAL.Services;
using PremiumCalculator.WebAPI.Controllers;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;

namespace PremiumCalculator.WebAPI.Tests.Controllers
{
    [TestClass]
    public class MonthlyPremiumControllerTest
    {
        IOccupationBAL _occupationBAL;
        IPremiumCalculatorBAL _premiumCalculatorBAL;

        public MonthlyPremiumControllerTest()
        {
            setupFixtures();
        }

        public void setupFixtures()
        {
            InsuranceDbEntities insuranceDbEntities = new InsuranceDbEntities();
            IOccupationDAL _occupationalDAL = new OccupationDAL(insuranceDbEntities);
            IOccupationRatingDAL _occupationRatingDAL = new OccupationRatingDAL(insuranceDbEntities);
            _occupationBAL = new OccupationBAL(_occupationalDAL, _occupationRatingDAL);
            _premiumCalculatorBAL = new PremiumCalculatorBAL(_occupationBAL);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetOccupationsTestAsync()
        {
            var controller = new MonthlyPremiumController(_occupationBAL, _premiumCalculatorBAL)
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration()
            };

            var response = await controller.GetOccupationsList();

            var controllerResponse = response.ExecuteAsync(CancellationToken.None).Result;

            Assert.IsNotNull(response);
            Assert.IsTrue(controllerResponse.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, controllerResponse.StatusCode);

            List<OccupationData> occupations;
            Assert.IsTrue(controllerResponse.TryGetContentValue(out occupations));
            Assert.AreEqual(6, occupations.Count);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetPremiumValueTestAsync()
        {
            var controller = new MonthlyPremiumController(_occupationBAL, _premiumCalculatorBAL)
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration()
            };

            // Test data
            var premiumParams = new PremiumParametersData();
            premiumParams.Age = 25;
            premiumParams.OccupationId = 1;
            premiumParams.SumInsured = "$ 150000";

            var response = await controller.GetMonthlyPremiumValue(premiumParams);
            var controllerResponse = response.ExecuteAsync(CancellationToken.None).Result;

            Assert.IsNotNull(response);
            Assert.IsTrue(controllerResponse.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, controllerResponse.StatusCode);
            Assert.IsNotNull(controllerResponse.Content);           

            decimal premiumValue;
            Assert.IsTrue(controllerResponse.TryGetContentValue<decimal>(out premiumValue));
            Assert.AreEqual((decimal)468.75, premiumValue);
        }
    }
}
