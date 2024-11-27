using Core.Entities;
using Core.Utilities.ResultPattern.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAppointmentService
    {
        IResult Add(AppointmentEntity appointment);
        IDataResult<AppointmentEntity> Update(AppointmentEntity appointment);
        IResult Delete(AppointmentEntity appointment);
        IResult DeleteById(int id);
        IDataResult<List<AppointmentEntity>> GetAll();
        IDataResult<AppointmentEntity> GetById();


        //IDataResult<List<AppointmentEntity>> GetByExpression(Expression<Func<AppointmentEntity, bool>> filter = null);//Bunu nasıl yaparım??
    }
}
