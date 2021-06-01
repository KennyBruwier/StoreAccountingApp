using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.Models.Abstracts
{
    public abstract class RecordTimeStamps : BaseModel
    {
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required]
        public DateTime UpdateAt { get; set; } = DateTime.Now;
        public DateTime? ClosedAt { get; set; }
    }
}
