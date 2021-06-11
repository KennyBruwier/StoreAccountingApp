using StoreAccountingApp.CustomMethods;
using StoreAccountingApp.Models;
using StoreAccountingApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Entity.Validation;
using StoreAccountingApp.Services.DBTables;

namespace StoreAccountingApp.Services
{
    public class ShopService : BaseService<ShopDTO,Shop>
    {
        public ShopService()
        {
        }
        
        public override ShopDTO CopyDBtoDTO(Shop source)
        {
            ShopDTO newShopDTO = ObjMethods.CopyProperties<Shop, ShopDTO>(source);
            if (newShopDTO.PostalCodeId != null)
            {
                DistrictService districtService = new DistrictService();
                newShopDTO.DistrictDTO = districtService.Search(newShopDTO.PostalCodeId);
                newShopDTO.DistrictName = newShopDTO.DistrictDTO?.Name;
                newShopDTO.CountryName = newShopDTO.DistrictDTO?.CountryName;
            }
            return newShopDTO;
        }
        public override Shop CopyDTOtoDB(ShopDTO dtoModel)
        {
            Shop newShop = ObjMethods.CopyProperties<ShopDTO, Shop>(dtoModel);
            if (dtoModel.PostalCodeId != "")
            {
                District newDistrict = ctx.Districts.Find(dtoModel.PostalCodeId);
                if (newDistrict == null)
                {
                    Country currentDistrictCountry = null;
                    if (dtoModel.CountryName != null)
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
                newShop.District = newDistrict;
            }
            return newShop;

            //Shop newShop = ObjMethods.CopyProperties<ShopDTO, Shop>(dtoModel);
            //if (dtoModel.PostalCodeId != "")
            //{
            //    District newDistrict = ctx.Districts.Find(dtoModel.PostalCodeId);
            //    if (newDistrict == null)
            //    {
            //        newDistrict = new District()
            //        {
            //            PostalCodeId = dtoModel.PostalCodeId,
            //            Name = dtoModel.DistrictName
            //        };
            //        Country currentDistrictCountry;
            //        currentDistrictCountry = ctx.Countries.FirstOrDefault(c => c.Name.Equals(dtoModel.CountryName, StringComparison.OrdinalIgnoreCase));
            //        if (currentDistrictCountry == null)
            //        {
            //            currentDistrictCountry = new Country() { Name = dtoModel.CountryName };
            //        }
            //        newDistrict.Country = currentDistrictCountry;
            //    }
            //    newShop.District = newDistrict;
            //}
            //return newShop;
        }

    }
}
