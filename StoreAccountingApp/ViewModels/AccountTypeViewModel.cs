using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using StoreAccountingApp.Models;

namespace StoreAccountingApp.ViewModels
{
    public class AccountTypeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        AccountTypeService ObjAccountTypeService;
        public AccountTypeViewModel()
        {
            ObjAccountTypeService = new AccountTypeService();
            LoadData();
        }
        private List<AccountTypeDTO> accountTypeList;
        public List<AccountTypeDTO>AccountTypeList
        {
            get { return accountTypeList; }
            set { accountTypeList = value; OnPropertyChanged("AccountTypeList"); }
        }
        private void LoadData()
        {
            AccountTypeList = ObjAccountTypeService.GetAll();
        }
    }
}
