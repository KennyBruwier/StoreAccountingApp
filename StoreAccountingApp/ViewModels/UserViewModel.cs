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
    public class UserViewModel : INotifyPropertyChanged
    {
        UserService ObjUserService;
        private UserDTO currentUserDTO;
        public UserDTO CurrentUserDTO
        {
            get { return currentUserDTO; }
            set { currentUserDTO = value; OnPropertyChanged("CurrentUserDTO"); }
        }
        public UserViewModel()
        {
            ObjUserService = new UserService();
            LoadData();
            CurrentUserDTO = new UserDTO();
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
        private List<UserDTO> accountTypeList;
        public List<UserDTO> UserList
        {
            get { return accountTypeList; }
            set { accountTypeList = value; OnPropertyChanged("UserList"); }
        }
        private void LoadData()
        {
            UserList = ObjUserService.GetAll();
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
                var IsSaved = ObjUserService.Add(CurrentUserDTO);
                LoadData();
                if (IsSaved)
                    Message = "User saved";
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
                var ObjUser = ObjUserService.Search(CurrentUserDTO.UserId);
                if (ObjUser != null)
                {
                    CurrentUserDTO = ObjUser;
                    Message = "User found";
                }
                else
                {
                    CurrentUserDTO = new UserDTO(); // empty the textbox fields
                    Message = "User not found";
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
                if (ObjUserService.Update(CurrentUserDTO))
                {
                    Message = "User updated";
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
                if (ObjUserService.Delete(CurrentUserDTO.UserId))
                {
                    Message = "User Deleted";
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
