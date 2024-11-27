using Core.Utilities.ResultPattern;
using Core.Utilities.ResultPattern.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
    public class BusinessRulesEngine
    {
        public static IResult Run(params IResult[] logics)//istediğim kadar kural parametresi (IResult parametresi) yollayabilirim
        {
            foreach (var logic in logics)
            {
                if (!logic.IsSuccess)
                {
                    return logic;//error result döndürür. Tüm hataları da döndürmek için buraya logic eklenebilir. (List of IResult gibi)
                }
            }

            return new SuccessResult();//null dönebilirim, boolean sonucu kesin olarak ifade etmiyor. İleride tereddüte düşebilirim
        }
    }
}
//24 Şubat ders sonu
