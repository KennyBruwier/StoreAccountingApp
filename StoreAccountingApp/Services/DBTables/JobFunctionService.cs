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

namespace StoreAccountingApp.Services
{
    public class JobFunctionService
    {
        _DBStoreAccountingContext ctx;

        public object DialogResult { get; private set; }
        public JobFunctionService()
        {
            ctx = new _DBStoreAccountingContext();
        }
        public List<JobFunctionDTO> GetAll()
        {
            List<JobFunctionDTO> jobFunctionList = new List<JobFunctionDTO>();
            var ObjQuery = from JobFunction in ctx.JobFunctions
                           select JobFunction;
            foreach (var jobFunction in ObjQuery)
            {
                jobFunctionList.Add(ObjMethods.CopyProperties<JobFunction, JobFunctionDTO>(jobFunction));
            }
            return jobFunctionList;
        }
        public bool Add(JobFunctionDTO newJobFunctionDTO)
        {
            //                                                          <----- Add validations here
            if (newJobFunctionDTO.JobFunctionId != 0)
            {
                if (ctx.JobFunctions.Find(newJobFunctionDTO.JobFunctionId) != null)
                {
                    MessageBoxResult dialogResult = MessageBox.Show($"A job function with id {newJobFunctionDTO.JobFunctionId} was already found, do you want to update it instead?",
                                                                    "job function already exists", MessageBoxButton.YesNo);
                    if (dialogResult == MessageBoxResult.Yes)
                        return Update(newJobFunctionDTO);
                    else
                        throw new ArgumentException($"Add operation failed, id {newJobFunctionDTO.JobFunctionId} already exists");
                }
            }

            if (ctx.JobFunctions.FirstOrDefault(a => (a.Title == newJobFunctionDTO.Title)) != null)
                throw new ArgumentException($"Add operation failed, {newJobFunctionDTO.Title} already exists");
            try
            {
                ctx.JobFunctions.Add(ObjMethods.CopyProperties<JobFunctionDTO, JobFunction>(newJobFunctionDTO));
                return ctx.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JobFunctionDTO Search(int jobFunctionId)
        {
            JobFunctionDTO ObjJobFunction = null;
            var ObjJobFunctionToFind = ctx.JobFunctions.Find(jobFunctionId);
            if (ObjJobFunctionToFind != null)
            {
                ObjJobFunction = ObjMethods.CopyProperties<JobFunction, JobFunctionDTO>(ObjJobFunctionToFind);
            }
            return ObjJobFunction;
        }
        public bool Update(JobFunctionDTO objJobFunctionToUpdate)
        {
            var ObjJobFunction = ctx.JobFunctions.Find(objJobFunctionToUpdate.JobFunctionId);
            if (ObjJobFunction != null)
            {
                ObjJobFunction = ObjMethods.CopyProperties<JobFunctionDTO, JobFunction>(objJobFunctionToUpdate);
            }
            try
            {
                return ctx.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(int jobFunctionId)
        {
            var ObjJobFunctionToDelete = ctx.JobFunctions.Find(jobFunctionId);
            if (ObjJobFunctionToDelete != null)
                ctx.JobFunctions.Remove(ObjJobFunctionToDelete);
            return ctx.SaveChanges() > 0;
        }
    }
}
