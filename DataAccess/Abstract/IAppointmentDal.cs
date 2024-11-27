using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IAppointmentDal : IEntityRepository<AppointmentEntity>//IEntity interface'ini uygulayan bir Entity olmalıdır
    {
    }
}
