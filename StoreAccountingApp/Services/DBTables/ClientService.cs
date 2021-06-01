using StoreAccountingApp.CustomMethods;
using StoreAccountingApp.Models;
using StoreAccountingApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel.DataAnnotations;

namespace StoreAccountingApp.Services
{
    public class ClientService
    {
        private readonly _DBStoreAccountingContext ctx;

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
                Client newClient = new Client();
                newClient = ObjMethods.CopyProperties<ClientDTO, Client>(newClientDTO);
                if (newClientDTO.PostalCodeId != "")
                {
                    District newDistrict = ctx.Districts.Find(newClientDTO.PostalCodeId);
                    if (newDistrict == null)
                    {
                        newDistrict = new District()
                        {
                            PostalCodeId = newClientDTO.PostalCodeId,
                            Name = newClientDTO.DistrictName
                        };
                        Country currentDistrictCountry;
                        currentDistrictCountry = ctx.Countries.FirstOrDefault(c => c.Name.Equals(newClientDTO.CountryName, StringComparison.OrdinalIgnoreCase));
                        if (currentDistrictCountry == null)
                        {
                            currentDistrictCountry = new Country() { Name = newClientDTO.CountryName };
                        }
                        newDistrict.Country = currentDistrictCountry;
                    }
                    newClient.District = newDistrict;
                }
                ctx.Clients.Add(newClient);
                return ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw ((System.Data.Entity.Validation.DbEntityValidationException)ex);
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
