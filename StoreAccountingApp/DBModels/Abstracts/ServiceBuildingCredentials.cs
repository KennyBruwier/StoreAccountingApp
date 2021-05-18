﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.DBModels.Abstracts
{
    public abstract class ServiceBuildingCredentials : Address
    {
        [Required]
        [MaxLength(20)]
        public string BuildingName { get; set; }
        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Active";
        [Required]
        public DateTime InService { get; set; } = DateTime.Now;
        public DateTime? OutOfService { get; set; }
    }
}
