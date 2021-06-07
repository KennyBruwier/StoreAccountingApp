using StoreAccountingApp.Commands;
using StoreAccountingApp.DTO;
using StoreAccountingApp.GeneralClasses;
using StoreAccountingApp.Models;
using StoreAccountingApp.Services.DBTables;
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
        private readonly RelayCommand saveCommand;
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
            return serviceModel.Delete(CurrentDTOModel.Validation.GetPrimaryKeysValue());
        }
        #endregion
    }
}
