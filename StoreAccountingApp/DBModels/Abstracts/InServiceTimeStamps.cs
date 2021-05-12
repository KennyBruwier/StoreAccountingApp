using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.DBModels.Abstracts
{
    public abstract class InServiceTimeStamps : RecordTimeStamps
    {
        [Required]
        public DateTime InService { get; set; } = DateTime.Now;
        [Required]
        public DateTime OutOfService { get; set; } = DateTime.Now;
    }
}
