using Core.Aspects.AutofacAspect.ExceptionLog;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Aspects.AutofacAspect.ExceptionLog
{
    public class LogEntity
    {
        [Key]
        public int Id { get; set; }
        public string MethodName { get; set; }
        public string ExceptionMessage { get; set; }
        public string MethodParameters { get; set; }
        public DateTime ErrorDateTime { get; set; }

        [NotMapped]
        public List<LogParameter> LogParameters
        {
            get => JsonConvert.DeserializeObject<List<LogParameter>>(MethodParameters);
            set => MethodParameters = JsonConvert.SerializeObject(value);
        }
    }
}
