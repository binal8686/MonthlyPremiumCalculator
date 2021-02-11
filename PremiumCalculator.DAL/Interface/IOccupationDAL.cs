using PremiumCalculator.DAL.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumCalculator.DAL.Interface
{
    public interface IOccupationDAL
    {
        IQueryable<Occupation>  GetOccupations();
    }
}
