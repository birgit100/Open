using System;
using System.ComponentModel.DataAnnotations;

namespace Open.Sentry.Models
{
    public class ValidFromGroup
    {
        [DataType(DataType.Date)]
        public DateTime? ValidFromDate { get; set; }
        public int ObjectCount { get; set; }
    }
}
