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
        public override ClientDTO CopyDBtoDTO(Client source)
        {
            ClientDTO newClientDTO = ObjMethods.CopyProperties<Client,ClientDTO>(source);
            if (newClientDTO.PostalCodeId != null)
            {
                DistrictService districtService = new DistrictService();
                newClientDTO.DistrictDTO = districtService.Search(newClientDTO.PostalCodeId);
                newClientDTO.DistrictName = newClientDTO.DistrictDTO?.Name;
                newClientDTO.CountryName = newClientDTO.DistrictDTO?.CountryName;
            }
            return newClientDTO;
        }
        public override Client CopyDTOtoDB(ClientDTO dtoModel)
        {
            Client newClient = ObjMethods.CopyProperties<ClientDTO, Client>(dtoModel);
            if (dtoModel.PostalCodeId != "")
            {
                District newDistrict = ctx.Districts.Find(dtoModel.PostalCodeId);
                if (newDistrict == null)
                {
                    Country currentDistrictCountry = null;
                    if (dtoModel.CountryName!=null)
                    {
                        currentDistrictCountry = ctx.Countries.FirstOrDefault(c => c.Name.Equals(dtoModel.CountryName, StringComparison.OrdinalIgnoreCase));
                        if (currentDistrictCountry == null)
                        {
                            currentDistrictCountry = new Country() { Name = dtoModel.CountryName };
                        }
                    }
                    newDistrict = new District()
                    {
                        PostalCodeId = dtoModel.PostalCodeId,
                        Name = dtoModel.DistrictName
                    };
                    newDistrict.Country = currentDistrictCountry;
                }
                newClient.District = newDistrict;
            }
            return newClient;
        }
    }
}
