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
    public class CountryService
    {
        _DBStoreAccountingContext ctx;

        public object DialogResult { get; private set; }
        public CountryService()
        {
            ctx = new _DBStoreAccountingContext();
        }
        public List<CountryDTO> GetAll()
        {
            List<CountryDTO> countryList = new List<CountryDTO>();
            var ObjQuery = from Country in ctx.Countries
                           select Country;
            foreach (var country in ObjQuery)
            {
                countryList.Add(ObjMethods.CopyProperties<Country, CountryDTO>(country));
            }
            return countryList;
        }
        public bool Add(CountryDTO newCountryDTO)
        {
            //                                                          <----- Add validations here
            if (newCountryDTO.CountryId != 0)
            {
                if (ctx.Countries.Find(newCountryDTO.CountryId) != null)
                {
                    MessageBoxResult dialogResult = MessageBox.Show($"A country with id {newCountryDTO.CountryId} was already found, do you want to update it instead?",
                                                                    "country already exists", MessageBoxButton.YesNo);
                    if (dialogResult == MessageBoxResult.Yes)
                        return Update(newCountryDTO);
                    else
                        throw new ArgumentException($"Add operation failed, id {newCountryDTO.CountryId} already exists");
                }
            }

            if (ctx.Countries.FirstOrDefault(a => (a.Name == newCountryDTO.Name)) != null)
                throw new ArgumentException($"Add operation failed, {newCountryDTO.Name} already exists");
            try
            {
                ctx.Countries.Add(ObjMethods.CopyProperties<CountryDTO, Country>(newCountryDTO));
                return ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CountryDTO Search(int countryId)
        {
            CountryDTO ObjCountry = null;
            var ObjCountryToFind = ctx.Countries.Find(countryId);
            if (ObjCountryToFind != null)
            {
                ObjCountry = ObjMethods.CopyProperties<Country, CountryDTO>(ObjCountryToFind);
            }
            return ObjCountry;
        }
        public CountryDTO Search(string countryName)
        {
            CountryDTO ObjCountry = null;
            var ObjCountryToFind = ctx.Countries.FirstOrDefault(c=>c.Name == countryName);
            if (ObjCountryToFind != null)
            {
                ObjCountry = ObjMethods.CopyProperties<Country, CountryDTO>(ObjCountryToFind);
            }
            return ObjCountry;
        }
        public bool Update(CountryDTO objCountryToUpdate)
        {
            var ObjCountry = ctx.Countries.Find(objCountryToUpdate.CountryId);
            if (ObjCountry != null)
            {
                ObjCountry = ObjMethods.CopyProperties<CountryDTO, Country>(objCountryToUpdate);
            }
            return ctx.SaveChanges() > 0;
        }
        public bool Delete(int countryId)
        {
            var ObjCountryToDelete = ctx.Countries.Find(countryId);
            if (ObjCountryToDelete != null)
                ctx.Countries.Remove(ObjCountryToDelete);
            return ctx.SaveChanges() > 0;
        }
    }
}
