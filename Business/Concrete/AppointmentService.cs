using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.CrossCuttingConcerns;
using Business.ValidationRules;
using Core.Aspects.AutofacAspect.ExceptionLog;
using Core.Aspects.AutofacAspect.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.ResultPattern;
using Core.Utilities.ResultPattern.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentValidation;
using Microsoft.Extensions.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AppointmentService : IAppointmentService
    {
        IAppointmentDal _appointmentDal;
        //Başka bir tablodan veri çekmemiz gerekirse ilgili servisii injection ederiz. Kendisine ait olanın dışında bir DAL inection yaparsak, kendi kurallarını, loglarını uygulayamaz ve yeni kurallarda karmaşaya yol açar

        public AppointmentService(IAppointmentDal appointmentDal)
        {
            _appointmentDal = appointmentDal;
        }

        //.netCache. Farklı yöntemlerden biri RedisCache, elasticseardch'um logstache
        //[ValidationAspect(typeof(AppointmentValidator))]//Interceptorların devreye gireceğini söyle: Business > DependencyResolvers > Autofac > AutofacBusinessModule
        [ExceptionLogAspect(typeof(ErrorDatabaseLogger))]
        [SecuredOperationAspect("product.add")]
        [CacheAspect]//key value
        public IResult Add(AppointmentEntity appointment)
        {
            //ValidationTool.Validate(new AppointmentValidator(),appointment);//Validation aspect'e taşındı. Attribute ile gönderiliyor

            //Tarih kontrolü Validation ile yapılacak!!!!!!!!!!!
            //Plaka sorgulama

            //IResult result = BusinessRulesEngine.Run(CheckTheWeeklyLimitExceeded(appointment), CheckIdExist(appointment.Id));//Polimorphysm
            //if (result!=null)
            //{
            //    return result;
            //}

                _appointmentDal.Add(appointment);
                return new Result(true, "veri kaydı yapıldı");

        }

        private IResult CheckIdExist(int id)//yalandan kontrol, iş motoruna koyucam.
        {
            var result = _appointmentDal.GetAll(a => a.Id == id).Any();
            if (result)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }

        private IResult CheckTheWeeklyLimitExceeded(AppointmentEntity appointment)//Haftalık kısıt kontrolü
        {
            var now = DateTime.UtcNow;

            var checkIfExist = _appointmentDal.GetAll(a =>//burda hata olabilir!!
                a.LicencePlate == appointment.LicencePlate &&
                a.PhoneNumber == appointment.PhoneNumber &&
                (now - a.AppointmentDate).Days < 7
            );

            if (checkIfExist != null)
            {
                return new ErrorResult("Haftalık limit dolu");
            }
            return new SuccessResult();
        }


        public IResult Delete(AppointmentEntity appointment)
        {
            try
            {
                _appointmentDal.Delete(appointment);
                return new Result(true, "msg");
            }
            catch (Exception)
            {

                return new Result(false, "Exception");
            }
        }

        public IResult DeleteById(int id)
        {
            try
            {
                var deletingAppointment = _appointmentDal.Get(p => p.Id == id);
                _appointmentDal.Delete(deletingAppointment);
                return new Result(true, "msg");
            }
            catch (Exception)
            {

                return new Result(false, "msg");
            }
        }

        public IDataResult<List<AppointmentEntity>> GetAll()
        {
            try
            {
                return new SuccessDataResult<List<AppointmentEntity>>(_appointmentDal.GetAll(), "Messages.AppointmentSuccessGet");
            }
            catch (Exception)
            {

                return new ErrorDataResult<List<AppointmentEntity>>("error message");
            }
        }

        public IDataResult<AppointmentEntity> GetById()
        {
            throw new NotImplementedException();
        }

        //[ValidationAspect(typeof(AppointmentValidator))]
        public IDataResult<AppointmentEntity> Update(AppointmentEntity appointment)
        {
            throw new NotImplementedException();
        }
    }
}
