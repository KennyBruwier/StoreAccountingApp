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
    public class ClientViewModel : INotifyPropertyChanged
    {
        ClientService ObjClientService;
        private ClientDTO currentClientDTO;
        public ClientDTO CurrentClientDTO
        {
            get { return currentClientDTO; }
            set { currentClientDTO = value; OnPropertyChanged("CurrentClientDTO"); }
        }
        public ClientViewModel()
        {
            ObjClientService = new ClientService();
            LoadData();
            CurrentClientDTO = new ClientDTO();
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
        private List<ClientDTO> accountTypeList;
        public List<ClientDTO> ClientList
        {
            get { return accountTypeList; }
            set { accountTypeList = value; OnPropertyChanged("ClientList"); }
        }
        private void LoadData()
        {
            ClientList = ObjClientService.GetAll();
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
                var IsSaved = ObjClientService.Add(CurrentClientDTO);
                LoadData();
                if (IsSaved)
                    Message = "Client saved";
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
                var ObjClient = ObjClientService.Search(CurrentClientDTO.ClientId);
                if (ObjClient != null)
                {
                    CurrentClientDTO = ObjClient;
                    Message = "Client found";
                }
                else
                {
                    CurrentClientDTO = new ClientDTO(); // empty the textbox fields
                    Message = "Client not found";
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
                if (ObjClientService.Update(CurrentClientDTO))
                {
                    Message = "Client updated";
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
                if (ObjClientService.Delete(CurrentClientDTO.ClientId))
                {
                    Message = "Client Deleted";
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
