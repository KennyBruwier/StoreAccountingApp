using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreAccountingApp.DTO.Abstracts;

namespace StoreAccountingApp.DTO
{
    public class SupplierDTO : PersonCredentialsDTO
    {
        private int supplierId;
        public int SupplierId
        {
            get { return supplierId; }
            set { supplierId = value; OnPropertyChanged("SupplierId"); }
        }
        private string organisation;
        public string Organisation
        {
            get { return organisation; }
            set { organisation = value; OnPropertyChanged("Organisation"); }
        }
        public override void LoadValidation()
        {
            Validation = new GeneralClasses.CheckValidation();
            Validation.AddPrimaryKey(nameof(SupplierId), SupplierId);
            Validation.AddNonNullFields(nameof(Firstname), Firstname);
            Validation.AddNonNullFields(nameof(Lastname), Lastname);
            Validation.AddNonNullFields(nameof(EmailAddress), EmailAddress);
            Validation.AddUniqueValueFields(nameof(Firstname), Firstname);
            Validation.AddUniqueValueFields(nameof(Lastname), Lastname);
            Validation.AddUniqueValueFields(nameof(EmailAddress), EmailAddress);
        }
    }
}
