using PremiumCalculator.DAL.DataModel;
using PremiumCalculator.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumCalculator.DAL.Services
{
    public class OccupationRatingDAL : IOccupationRatingDAL
    {

        private DbContext _dbContext;

        public OccupationRatingDAL(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

     
        public IQueryable<OccupationRating> GetOccupaitonRatings()
        {
            return _dbContext.Set<OccupationRating>().AsQueryable();
        }
    }
}
