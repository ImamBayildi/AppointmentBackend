using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.AutofacAspect.Validation//Core > Validation > ValidationTool içerisindeki tool'u kullanır
{
    //Aspect ile çalışacak metot, başında, sonunda yada nerde istersen
    public class ValidationAspect : MethodInterception//Bu bir attribute. ValidationAspect, sen bir MethodInterception'sın [ValidationAspect(typeof(ProductValidator))] : Bu metodu doğrula, product validator kullanarak.
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)//Bana validator type'ı ver. attribute'da Type geçmek zorunlu
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))//Bir IValidator gelecek. Eğer gönderilen validator type bir IValidator değilse kız. (Defensive coding, olmasa da çalışır) IsAssignable:Atanabilir mi, validatorType'a
            {
                //Peki DevTime'da hata nasıl alırım?
                throw new System.Exception("AspectMessages.WrongValidationType");
            }

            _validatorType = validatorType;//IValidator olduğuna emin olduktan sonra eşitle
        }
        protected override void OnBefore(IInvocation invocation)//OnBefore'da çalışacak. Override ettim
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);//reflection Runtime'da çalışır. Gönderilen validator bir instance değil, new'lenmemiş. O yüzden runtime'da reflection ile türetiyorum
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];//AppointmentValidator'ın base type'ını bul, onun generic argümanlarından ilkini, çalıştığı entity type'ını bul [0]
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);//Invocation:İlgili metot(Add,update..). İlgili metodun argümanlarına(parametrelerine) bak ve entityType'ı, validator type'a eşit olan parametreleri bul
            foreach (var entity in entities)//Her birini tek tek gez ve
            {
                ValidationTool.Validate(validator, entity);//Validation tool'u kullanarak validate et. Validaton tool artık burada
            }
        }

    }
}//Ders 12 sonu Interceptors ve Validation Aspects, 17 Şubat Ders Sonu commiti
