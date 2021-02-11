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
    public class OccupationDAL : IOccupationDAL
    {

        private DbContext _dbContext;

        public OccupationDAL(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Occupation> GetOccupations()
        {
           return _dbContext.Set<Occupation>().AsQueryable();
        }
    }
}
