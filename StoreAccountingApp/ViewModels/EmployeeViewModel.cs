using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using StoreAccountingApp.Models;
using StoreAccountingApp.Commands;
using StoreAccountingApp.Models.DTO;

namespace StoreAccountingApp.ViewModels
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        EmployeeService ObjEmployeeService;
        private EmployeeDTO currentEmployeeDTO;
        public EmployeeDTO CurrentEmployeeDTO
        {
            get { return currentEmployeeDTO; }
            set { currentEmployeeDTO = value; OnPropertyChanged("CurrentEmployeeDTO"); }
        }
        public EmployeeViewModel()
        {
            ObjEmployeeService = new EmployeeService();
            LoadData();
            CurrentEmployeeDTO = new EmployeeDTO();
            saveCommand = new RelayCommand(Save);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(Update);
            deleteCommand = new RelayCommand(Delete);
        }
        #region INotifyPropertyChanged_Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region DisplayOperation
        private List<EmployeeDTO> accountTypeList;
        public List<EmployeeDTO> EmployeeList
        {
            get { return accountTypeList; }
            set { accountTypeList = value; OnPropertyChanged("EmployeeList"); }
        }
        private void LoadData()
        {
            EmployeeList = ObjEmployeeService.GetAll();
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
                var IsSaved = ObjEmployeeService.Add(CurrentEmployeeDTO);
                LoadData();
                if (IsSaved)
                    Message = "Employee saved";
                else
                    Message = "Save operation failed";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
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
                var ObjEmployee = ObjEmployeeService.Search(CurrentEmployeeDTO.EmployeeId);
                if (ObjEmployee != null)
                {
                    CurrentEmployeeDTO = ObjEmployee;
                    Message = "Employee found";
                }
                else
                {
                    CurrentEmployeeDTO = new EmployeeDTO(); // empty the textbox fields
                    Message = "Employee not found";
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
                if (ObjEmployeeService.Update(CurrentEmployeeDTO))
                {
                    Message = "Employee updated";
                    LoadData();
                }
                else
                {
                    Message = "Update operation failed";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
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
                if (ObjEmployeeService.Delete(CurrentEmployeeDTO.EmployeeId))
                {
                    Message = "Employee Deleted";
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
