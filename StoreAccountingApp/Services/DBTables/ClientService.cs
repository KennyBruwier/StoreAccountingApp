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
using System.Data.Entity.Validation;
using StoreAccountingApp.Services.DBTables;

namespace StoreAccountingApp.Services
{
    public class ClientService:BaseService<ClientDTO,Client>
    {
        public ClientService()
        {
        }
        public override Client DTOtoDBModel(ClientDTO dtoModelSource)
        {
            //        Client newClient = new Client();
            //        newClient = ObjMethods.CopyProperties<ClientDTO, Client>(newClientDTO);
            //        if (newClientDTO.PostalCodeId != "")
            //        {
            //            District newDistrict = ctx.Districts.Find(newClientDTO.PostalCodeId);
            //            if (newDistrict == null)
            //            {
            //                newDistrict = new District()
            //                {
            //                    PostalCodeId = newClientDTO.PostalCodeId,
            //                    Name = newClientDTO.DistrictName
            //                };
            //                Country currentDistrictCountry;
            //                currentDistrictCountry = ctx.Countries.FirstOrDefault(c => c.Name.Equals(newClientDTO.CountryName, StringComparison.OrdinalIgnoreCase));
            //                if (currentDistrictCountry == null)
            //                {
            //                    currentDistrictCountry = new Country() { Name = newClientDTO.CountryName };
            //                }
            //                newDistrict.Country = currentDistrictCountry;
            //            }
            //            newClient.District = newDistrict;
            //        }

            return base.DTOtoDBModel(dtoModelSource);
        }
        //public bool Add(ClientDTO newClientDTO)
        //{
        //    //                                                          <----- Add validations here
        //    if (newClientDTO.ClientId != 0)
        //    {
        //        if (ctx.Clients.Find(newClientDTO.ClientId) != null)
        //        {
        //            MessageBoxResult dialogResult = MessageBox.Show($"An client with id {newClientDTO.ClientId} was already found, do you want to update it instead?",
        //                                                            "client already exists", MessageBoxButton.YesNo);
        //            if (dialogResult == MessageBoxResult.Yes)
        //                return Update(newClientDTO);
        //            else
        //                throw new ArgumentException($"Add operation failed, id {newClientDTO.ClientId} already exists");
        //        }
        //    }

        //    if (ctx.Clients.FirstOrDefault(a => (a.Firstname == newClientDTO.Firstname) && (a.Lastname == newClientDTO.Lastname)) != null)
        //        throw new ArgumentException($"Add operation failed, {newClientDTO.Firstname} {newClientDTO.Lastname} already exists");
        //    try
        //    {
        //        Client newClient = new Client();
        //        newClient = ObjMethods.CopyProperties<ClientDTO, Client>(newClientDTO);
        //        if (newClientDTO.PostalCodeId != "")
        //        {
        //            District newDistrict = ctx.Districts.Find(newClientDTO.PostalCodeId);
        //            if (newDistrict == null)
        //            {
        //                newDistrict = new District()
        //                {
        //                    PostalCodeId = newClientDTO.PostalCodeId,
        //                    Name = newClientDTO.DistrictName
        //                };
        //                Country currentDistrictCountry;
        //                currentDistrictCountry = ctx.Countries.FirstOrDefault(c => c.Name.Equals(newClientDTO.CountryName, StringComparison.OrdinalIgnoreCase));
        //                if (currentDistrictCountry == null)
        //                {
        //                    currentDistrictCountry = new Country() { Name = newClientDTO.CountryName };
        //                }
        //                newDistrict.Country = currentDistrictCountry;
        //            }
        //            newClient.District = newDistrict;
        //        }
        //        ctx.Clients.Add(newClient);
        //        return ctx.SaveChanges() > 0;
        //    }
        //    catch (DbEntityValidationException ex)
        //    {
        //        throw ((System.Data.Entity.Validation.DbEntityValidationException)ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
