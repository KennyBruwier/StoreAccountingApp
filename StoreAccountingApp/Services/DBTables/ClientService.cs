using StoreAccountingApp.CustomMethods;
using StoreAccountingApp.Models;
using StoreAccountingApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StoreAccountingApp.Services
{
    public class ClientService
    {
        _DBStoreAccountingContext ctx;

        public object DialogResult { get; private set; }
        public ClientService()
        {
            ctx = new _DBStoreAccountingContext();
        }
        public List<ClientDTO> GetAll()
        {
            List<ClientDTO> clientList = new List<ClientDTO>();
            var ObjQuery = from Client in ctx.Clients
                           select Client;
            foreach (var client in ObjQuery)
            {
                clientList.Add(ObjMethods.CopyProperties<Client, ClientDTO>(client));
            }
            return clientList;
        }
        public bool Add(ClientDTO newClientDTO)
        {
            //                                                          <----- Add validations here
            if (newClientDTO.ClientId != 0)
            {
                if (ctx.Clients.Find(newClientDTO.ClientId) != null)
                {
                    MessageBoxResult dialogResult = MessageBox.Show($"An client with id {newClientDTO.ClientId} was already found, do you want to update it instead?",
                                                                    "client already exists", MessageBoxButton.YesNo);
                    if (dialogResult == MessageBoxResult.Yes)
                        return Update(newClientDTO);
                    else
                        throw new ArgumentException($"Add operation failed, id {newClientDTO.ClientId} already exists");
                }
            }

            if (ctx.Clients.FirstOrDefault(a => (a.Firstname == newClientDTO.Firstname) && (a.Lastname == newClientDTO.Lastname)) != null)
                throw new ArgumentException($"Add operation failed, {newClientDTO.Firstname} {newClientDTO.Lastname} already exists");
            try
            {
                ctx.Clients.Add(ObjMethods.CopyProperties<ClientDTO, Client>(newClientDTO));
                return ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ClientDTO Search(int clientId)
        {
            ClientDTO ObjClient = null;
            var ObjClientToFind = ctx.Clients.Find(clientId);
            if (ObjClientToFind != null)
            {
                ObjClient = ObjMethods.CopyProperties<Client, ClientDTO>(ObjClientToFind);
            }
            return ObjClient;
        }
        public bool Update(ClientDTO objClientToUpdate)
        {
            var ObjClient = ctx.Clients.Find(objClientToUpdate.ClientId);
            if (ObjClient != null)
            {
                ObjClient = ObjMethods.CopyProperties<ClientDTO, Client>(objClientToUpdate);
            }
            return ctx.SaveChanges() > 0;
        }
        public bool Delete(int clientId)
        {
            var ObjClientToDelete = ctx.Clients.Find(clientId);
            if (ObjClientToDelete != null)
                ctx.Clients.Remove(ObjClientToDelete);
            return ctx.SaveChanges() > 0;
        }
    }
}
