﻿using StoreAccountingApp.DBModels.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.DBModels
{
    public class Sale : Invoice
    {
        [Key]
        public int SaleId { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        public int StoreId { get; set; }
        public virtual Store Store { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
