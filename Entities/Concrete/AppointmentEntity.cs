using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class AppointmentEntity:IEntity
    {
        public int Id { get; set; }
        public DateTimeOffset AppointmentDate { get; set; }
        public string AppointmentType { get; set; }//harici tabloya çıkarıldıktan sonra integer
        public string LicencePlate { get; set; }//plaka
        public string VehicleModel { get; set; }//now only motorcycles
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Details { get; set; }

    }
}

/*
Talep ID//demand
Tarih ve Saat
Talep Tipi
Plaka
Motor Modeli
İsim Soyisim
Telefon Numarası ( Doğrulama )
Talep Detay
*/