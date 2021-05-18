﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreAccountingApp.Models.DTO.Abstracts;

namespace StoreAccountingApp.Models.DTO
{
    class SaleProductDTO : ItemTransactionDTO
    {
        private int saleId;
        public int SaleId
        {
            get { return saleId; }
            set { saleId = value; OnPropertyChanged("SaleId"); }
        }
        private int productId;
        public int ProductId
        {
            get { return productId; }
            set { productId = value; OnPropertyChanged("ProductId"); }
        }

    }
}
