using Core.DataAccess;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfAppointmentDal : EfEntityRepositoryBase<AppointmentEntity,AppointmentDBContext>, IAppointmentDal
    {

        /*
         * public List<CarDetailDto> GetCarDetails()//need double join
        {
            using (CarDBContext carContext = new CarDBContext())
            {

                var query = from car in carContext.Cars
                             join brand in carContext.Brands on car.BrandId equals brand.Id
                             join color in carContext.Colors on car.ColorId equals color.Id
                             select new CarDetailDto
                             {
                                 BrandName = brand.BrandName,
                                 ColorName = color.ColorName,
                                 ModelYear = car.ModelYear,
                                 DailyPrice = car.DailyPrice,
                                 Description = car.Description
                             };
                return query.ToList();
            }
        }
        */
    }
}


/*
 var query =
    from t1 in myTABLE1List // List<TABLE_1>
    join t2 in myTABLE1List
      on new { A = t1.ColumnA, B = t1.ColumnB } equals new { A = t2.ColumnA, B = t2.ColumnB }
    join t3 in myTABLE1List
      on new { A = t2.ColumnA, B =  t2.ColumnB } equals new { A = t3.ColumnA, B = t3.ColumnB }


 // Gerekli using direktifi

// Sorguyu çalıştırarak sonucu almak
var carDetails = query.ToList();
*/