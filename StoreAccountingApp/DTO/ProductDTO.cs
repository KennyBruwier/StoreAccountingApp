using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreAccountingApp.DTO.Abstracts;

namespace StoreAccountingApp.DTO
{
    public class ProductDTO : RecordTimeStampsDTO
    {
        private int productId;
        public int ProductId
        {
            get { return productId; }
            set { productId = value; OnPropertyChanged("ProductId"); }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }
        private string manufacturer;
        public string Manufacturer
        {
            get { return manufacturer; }
            set { manufacturer = value; OnPropertyChanged("Manufacturer"); }
        }
        private string barcode;
        public string Barcode
        {
            get { return barcode; }
            set { barcode = value; OnPropertyChanged("Barcode"); }
        }
        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged("Description"); }
        }
        private string details;
        public string Details
        {
            get { return details; }
            set { details = value; OnPropertyChanged("Details"); }
        }
        public override void LoadValidation()
        {
            Validation = new GeneralClasses.CheckValidation();
            Validation.AddPrimaryKey(nameof(ProductId), ProductId);
            Validation.AddNonNullFields(nameof(Name), Name);
        }
    }
}
