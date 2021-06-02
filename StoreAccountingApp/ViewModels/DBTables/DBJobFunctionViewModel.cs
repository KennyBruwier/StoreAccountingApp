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

namespace StoreAccountingApp.ViewModels
{
    public class DBJobFunctionViewModel : DBViewModelBase
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
            saveCommand = new RelayCommand(Save);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(Update);
            deleteCommand = new RelayCommand(Delete);
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
        public void Save()
        {
            try
            {
                var IsSaved = ObjJobFunctionService.Add(CurrentJobFunctionDTO);
                LoadData();
                if (IsSaved)
                    Message = "JobFunction saved";
                else
                    Message = "Save operation failed";
            }
            catch (Exception ex)
            {
                Message = CreateValidationErrorMsg(ex);
            }
        }
        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged("Message"); }
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
            catch (Exception ex)
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
        public void Update()
        {
            try
            {
                if (ObjJobFunctionService.Update(CurrentJobFunctionDTO))
                {
                    Message = "JobFunction updated";
                    LoadData();
                }
                else
                {
                    Message = "Update operation failed";
                }
            }
            catch (Exception ex)
            {
                Message = CreateValidationErrorMsg(ex);
            }
        }
        #endregion
        #region DeleteOperation
        private RelayCommand deleteCommand;

        public RelayCommand DeleteCommand
        {
            get { return deleteCommand; }
        }
        public void Delete()
        {
            try
            {
                if (ObjJobFunctionService.Delete(CurrentJobFunctionDTO.JobFunctionId))
                {
                    Message = "JobFunction Deleted";
                    LoadData();
                }
                else
                    Message = "Delete operation failed";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion
    }
}
