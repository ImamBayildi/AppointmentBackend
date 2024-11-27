using Core.Aspects.AutofacAspect.ExceptionLog;
using DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Business.CrossCuttingConcerns
{
    public class ErrorDatabaseLogger : IErrorDatabaseLogger
    {
        public void Log(ErrorLog errorLog)
        {
            using (var context = new AppointmentDBContext())
            {
                var logEntity = new LogEntity
                {
                    MethodName = errorLog.MethodName,
                    ExceptionMessage = errorLog.ErrorMessage,
                    MethodParameters = JsonConvert.SerializeObject(errorLog.LogParameters),
                    ErrorDateTime = errorLog.ErrorDateTime
                };

                var addingData = context.Entry(logEntity);
                addingData.State = EntityState.Added;
                context.SaveChanges();
            }
        }
    }
}