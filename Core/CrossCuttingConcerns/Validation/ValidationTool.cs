using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool
    {

        public static void Validate(IValidator validator, object entitty)//Banabir IValidator ve doğrulamam için bir varlık(entity) ver
        {

            var context = new ValidationContext<object>(entitty);//or Dto
            //AppointmentValidator appointmentValidator = new AppointmentValidator();
            var result = validator.Validate(context);//Verilen doğrulayıcı sınıfıyla Validate et
            if (!result.IsValid)//Doğrulanmadıysa
            {
                throw new ValidationException(result.Errors);//Patlat
            }

        }

    }
}
