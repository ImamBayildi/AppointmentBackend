using Core.DataAccess;
using Core.Entities.Security;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfUserDal : EfEntityRepositoryBase<User, AppointmentDBContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new AppointmentDBContext())
            {
                var result = from op in context.t_OperationClaims
                             join uc in context.t_UserOperationClaims
                                 on op.Id equals uc.OperationClaimId
                             where uc.UserId == user.Id
                             select new OperationClaim { Id = op.Id, Name = op.Name };
                return result.ToList();

            }
        }
    }
}