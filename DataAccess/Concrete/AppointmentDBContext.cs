using Core.Aspects.AutofacAspect.ExceptionLog;
using Core.Entities.Security;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;


namespace DataAccess.Concrete
{
    public class AppointmentDBContext:DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=CANAVAR\SQLEXPRESS;Database=DBAppointment;TrustServerCertificate=True;User Id=sa;Password=0203;Integrated Security=true");
            //base.OnConfiguring(optionsBuilder);
        }

        public DbSet<AppointmentEntity> t_Appointments { get; set; }
        public DbSet<LogEntity> t_ErrorLogs { get; set; }

        public DbSet<OperationClaim> t_OperationClaims { get; set; }
        public DbSet<User> t_Users { get; set; }
        public DbSet<UserOperationClaim> t_UserOperationClaims { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //var converter = new ValueConverter<List<LogParameter>, string>(
            //    v => JsonConvert.SerializeObject(v),
            //    v => JsonConvert.DeserializeObject<List<LogParameter>>(v));

            //modelBuilder.Entity<LogEntity>()
            //    .Property(e => e.MethodParameters)
            //    .HasConversion(converter);
        }


    }
}


//Nuget packages'da sql server core dataAccess'de ekli değil, core için ekli. ?? Data Access+Core'da ekli olmalı ama neden dataAccessde eklememişim ve neden çalışıyor o eklentiyi incele