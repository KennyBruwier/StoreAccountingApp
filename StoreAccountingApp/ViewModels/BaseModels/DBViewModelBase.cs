using StoreAccountingApp.Commands;
using StoreAccountingApp.DTO;
using StoreAccountingApp.GeneralClasses;
using StoreAccountingApp.Models;
using StoreAccountingApp.Services.DBTables;
using StoreAccountingApp.Stores;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.ViewModels
{
    public class DBViewModelBase<DTOModel, ServiceModel, DBModel> : ViewModelBase
        where DTOModel:BaseDTO, new()
        where ServiceModel:BaseService<DTOModel,DBModel>, new()
        where DBModel:BaseModel, new()
    {
        protected AccountStore _accountStore;
        private readonly ServiceModel serviceModel;
        private DTOModel currentDTOModel;
        public DTOModel CurrentDTOModel
        {
            get { return currentDTOModel; }
            set 
            { 
                currentDTOModel = value; 
                OnPropertyChanged("CurrentDTOModel"); 
            }
        }
        public bool IsAdmin => CheckRole("admin");

        public DBViewModelBase()
        {
            this.tableName = GetClassName();
            serviceModel = new ServiceModel();
            LoadData();
            currentDTOModel = new DTOModel();
            saveCommand = new RelayCommand(SaveAndCatch);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(UpdateAndCatch);
            deleteCommand = new RelayCommand(DeleteAndCatch);
            clearCommand = new RelayCommand(Clear);
        }
        protected void OnCurrentAccountChanged()
        {
            OnPropertyChanged(nameof(IsAdmin));
        }
        protected bool CheckRole(string roleName)
        {
            if (_accountStore.CurrentAccount != null)
                switch (roleName.ToLower())
                {
                    case "admin": return _accountStore.CurrentAccount.AccountType.Admin;
                    case "stock manager": return _accountStore.CurrentAccount.AccountType.StockManager;
                    case "seller": return _accountStore.CurrentAccount.AccountType.Seller;
                }
            return false;
        }

        public override void LoadValidation()
        {
            currentDTOModel.LoadValidation();
        }
        #region DisplayOperation
        private List<DTOModel> dTOModelList;
        public List<DTOModel> DTOModelList
        {
            get { return dTOModelList; }
            set { dTOModelList = value; OnPropertyChanged("DTOModelList"); }
        }
        private void LoadData()
        {
            DTOModelList = serviceModel.GetAll();
        }
        #endregion
        #region SaveOperation
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
        }
        public void SaveAndCatch()
        {
            LoadValidation();
            CatchOperation(Save);
            LoadData();
        }
        public bool Save()            
        {
            return serviceModel.Add(CurrentDTOModel);
        }
        #endregion
        #region SearchOperation
        private readonly RelayCommand searchCommand;
        public RelayCommand SearchCommand
        {
            get { return searchCommand; }
        }
        public void Search()
        {
            try
            {
                CurrentDTOModel.LoadValidation();
                var recordFound = serviceModel.Search(CurrentDTOModel.Validation.GetPrimaryKeysValue());
                if (recordFound == null)
                {
                    DBField[][]UniqueFields = CurrentDTOModel.Validation.GetUniqueValueFields();
                    if (UniqueFields != null)
                        recordFound = serviceModel.Search(UniqueFields);
                }
                if (recordFound != null)
                {
                    CurrentDTOModel = recordFound;
                    Message = String.Format("{0} found",tableName);
                }
                else
                {
                    CurrentDTOModel = new DTOModel(); // empty the textbox fields
                    Message = String.Format("{0} not found", tableName);
                }
            }
            catch (DbEntityValidationException ex)
            {
                Message = ex.Message;
            }
        }
        #endregion
        #region UpdateOperation
        private readonly RelayCommand updateCommand;
        public RelayCommand UpdateCommand
        {
            get { return updateCommand; }
        }
        public void UpdateAndCatch()
        {
            LoadValidation();
            CatchOperation(Update);
            LoadData();
        }
        public bool Update()
        {
            return serviceModel.Update(CurrentDTOModel);
        }
        #endregion
        #region DeleteOperation
        private readonly RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get { return deleteCommand; }
        }
        public void DeleteAndCatch()
        {
            LoadValidation();
            CatchOperation(Delete);
            LoadData();
        }
        public bool Delete()
        {
            bool bSuccess = serviceModel.Delete(CurrentDTOModel.Validation.GetPrimaryKeysValue());
            if (bSuccess) CurrentDTOModel = new DTOModel() ;
            return bSuccess;
        }
        #endregion
        private readonly RelayCommand clearCommand;
        public RelayCommand ClearCommand
        {
            get { return clearCommand; }
        }
        public void Clear()
        {
            CurrentDTOModel = new DTOModel();
        }
        public override void Dispose()
        {
            _accountStore.CurrentAccountChanged -= OnCurrentAccountChanged;
            base.Dispose();
        }
    }
}
