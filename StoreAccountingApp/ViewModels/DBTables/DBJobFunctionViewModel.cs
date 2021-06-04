using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using StoreAccountingApp.Services;
using StoreAccountingApp.Commands;
using StoreAccountingApp.DTO;
using System.Windows.Input;
using System.Data.Entity.Validation;
using StoreAccountingApp.GeneralClasses;

namespace StoreAccountingApp.ViewModels
{
    public class DBJobFunctionViewModel : ViewModelBase
    {
        JobFunctionService ObjJobFunctionService;
        private JobFunctionDTO currentJobFunctionDTO;
        public JobFunctionDTO CurrentJobFunctionDTO
        {
            get { return currentJobFunctionDTO; }
            set { currentJobFunctionDTO = value; OnPropertyChanged("CurrentJobFunctionDTO"); }
        }
        public DBJobFunctionViewModel()
        {
            ObjJobFunctionService = new JobFunctionService();
            LoadData();
            CurrentJobFunctionDTO = new JobFunctionDTO();
            saveCommand = new RelayCommand(SaveAndCatch);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(UpdateAndCatch);
            deleteCommand = new RelayCommand(DeleteAndCatch);
        }
        #region DisplayOperation
        private List<JobFunctionDTO> accountTypeList;
        public List<JobFunctionDTO> JobFunctionList
        {
            get { return accountTypeList; }
            set { accountTypeList = value; OnPropertyChanged("JobFunctionList"); }
        }
        private void LoadData()
        {
            JobFunctionList = ObjJobFunctionService.GetAll();
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
            CatchOperation(Save);
            LoadData();
        }
        public bool Save()
        {
            return ObjJobFunctionService.Add(CurrentJobFunctionDTO);
        }
        #endregion
        #region SearchOperation
        private RelayCommand searchCommand;
        public RelayCommand SearchCommand
        {
            get { return searchCommand; }
        }
        public void Search()
        {
            try
            {
                var ObjJobFunction = ObjJobFunctionService.Search(CurrentJobFunctionDTO.JobFunctionId);
                if (ObjJobFunction != null)
                {
                    CurrentJobFunctionDTO = ObjJobFunction;
                    Message = "JobFunction found";
                }
                else
                {
                    CurrentJobFunctionDTO = new JobFunctionDTO(); // empty the textbox fields
                    Message = "JobFunction not found";
                }
            }
            catch (DbEntityValidationException ex)
            {
                Message = ex.Message;
            }
        }
        #endregion
        #region UpdateOperation
        private RelayCommand updateCommand;
        public RelayCommand UpdateCommand
        {
            get { return updateCommand; }
        }
        public void UpdateAndCatch()
        {
            CatchOperation(Update);
            LoadData();
        }
        public bool Update()
        {
            return ObjJobFunctionService.Update(CurrentJobFunctionDTO);
        }
        #endregion
        #region DeleteOperation
        private RelayCommand deleteCommand;

        public RelayCommand DeleteCommand
        {
            get { return deleteCommand; }
        }
        public void DeleteAndCatch()
        {
            CatchOperation(Delete);
            LoadData();
        }
        public bool Delete()
        {
            return ObjJobFunctionService.Delete(CurrentJobFunctionDTO.JobFunctionId);
        }
        #endregion
    }
}
