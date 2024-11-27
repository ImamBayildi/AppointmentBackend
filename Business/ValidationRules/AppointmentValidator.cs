using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{//19 farklı dil hata mesajı desteği
    public class AppointmentValidator : AbstractValidator<AppointmentEntity>
    {
        public AppointmentValidator()
        {
            //RuleFor(p => p.LicencePlate).NotEmpty();
            //RuleFor(p => p.FirstName).MinimumLength(2);
            //RuleFor(p => p.PhoneNumber).NotEmpty();
            //RuleFor(p => p.x).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);
            //RuleFor(p => p.VehicleModel).Must(StartWithA).WithMessage("Ürünler A harfi ile başlamalı").WithMessage("");
        }

        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
