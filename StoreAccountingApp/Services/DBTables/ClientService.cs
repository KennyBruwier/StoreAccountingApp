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
        public override Client CopyDTOtoDB(ClientDTO dtoModel)
        {
            Client newClient = ObjMethods.CopyProperties<ClientDTO, Client>(dtoModel);
            if (dtoModel.PostalCodeId != "")
            {
                District newDistrict = ctx.Districts.Find(dtoModel.PostalCodeId);
                if (newDistrict == null)
                {
                    newDistrict = new District()
                    {
                        PostalCodeId = dtoModel.PostalCodeId,
                        Name = dtoModel.DistrictName
                    };
                    Country currentDistrictCountry;
                    currentDistrictCountry = ctx.Countries.FirstOrDefault(c => c.Name.Equals(dtoModel.CountryName, StringComparison.OrdinalIgnoreCase));
                    if (currentDistrictCountry == null)
                    {
                        currentDistrictCountry = new Country() { Name = dtoModel.CountryName };
                    }
                    newDistrict.Country = currentDistrictCountry;
                }
                newClient.District = newDistrict;
            }
            return newClient;
        }
    }
}
