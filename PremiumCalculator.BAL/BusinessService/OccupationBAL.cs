using PremiumCalculator.BAL.Models;
using PremiumCalculator.DAL.Interface;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PremiumCalculator.BAL.BusinessService
{
    public class OccupationBAL : IOccupationBAL
    {
        IOccupationDAL _occupationDAL;

        IOccupationRatingDAL _occupationRatingDAL;

        public OccupationBAL()
        {
        }

        public OccupationBAL(IOccupationDAL occupationDAL, IOccupationRatingDAL occupationRatingDAL)
        {
            _occupationDAL = occupationDAL;
            _occupationRatingDAL = occupationRatingDAL;
        }

        public async Task<List<OccupationData>> getAllOccupationsList()
        {
            return await _occupationDAL.GetOccupations().Select(
                     oc => new OccupationData()
                     {
                         OccupationName = oc.OccupationName,
                         Id = oc.Id
                     }
                 ).ToListAsync();
        }

        public async Task<decimal> getOccupationFactor(int occupationId)
        {
            return await _occupationDAL.GetOccupations().Where(o => o.Id == occupationId).Join(
           _occupationRatingDAL.GetOccupaitonRatings(), o => o.OccupationRatingId, or => or.Id, (o, or) => or.Factor)
            .SingleOrDefaultAsync();
        }
    }
}
