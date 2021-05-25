using StoreAccountingApp.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.Models
{
    public class JobFunction : RecordTimeStamps
    {
        [Key]
        public int JobFunctionId { get; set; }
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }
        public string Description { get; set; }
        [MaxLength(20)]
        public string Category { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
