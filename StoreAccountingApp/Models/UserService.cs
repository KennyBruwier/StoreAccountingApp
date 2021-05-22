using StoreAccountingApp.CustomMethods;
using StoreAccountingApp.DBModels;
using StoreAccountingApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StoreAccountingApp.Models
{
    public class UserService
    {
        private _DBStoreAccountingContext ctx;

        public object DialogResult { get; private set; }
        public UserService()
        {
            ctx = new _DBStoreAccountingContext();
        }
        public List<UserDTO> GetAll()
        {
            List<UserDTO> UserList = new List<UserDTO>();
            var ObjQuery = from User in ctx.Users
                           select User;
            foreach (var User in ObjQuery)
            {
                UserList.Add(ObjMethods.CopyProperties<User, UserDTO>(User));
            }
            return UserList;
        }
        public bool Add(UserDTO newUserDTO)
        {
            //                                                          <----- Add validations here
            if (newUserDTO.UserId != 0)
            {
                if (ctx.Users.Find(newUserDTO.UserId) != null)
                {
                    MessageBoxResult dialogResult = MessageBox.Show($"An user record with user id {newUserDTO.UserId}was already found, do you want to update it instead?",
                                                                    "User already exists", MessageBoxButton.YesNo);
                    if (dialogResult == MessageBoxResult.Yes)
                        return Update(newUserDTO);
                    else
                        throw new ArgumentException($"Add operation failed, user id {newUserDTO.UserId}already exists");
                }
            }

            try
            {
                ctx.Users.Add(ObjMethods.CopyProperties<UserDTO, User>(newUserDTO));
                return ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UserDTO Search(int userId)
        {
            UserDTO ObjUser = null;
            var ObjUserToFind = ctx.Users.Find(userId);
            if (ObjUserToFind != null)
            {
                ObjUser = ObjMethods.CopyProperties<User, UserDTO>(ObjUserToFind);
            }
            return ObjUser;
        }
        public bool Update(UserDTO objUserToUpdate)
        {
            var ObjUser = ctx.Users.Find(objUserToUpdate.UserId);
            if (ObjUser != null)
            {
                ObjUser = ObjMethods.CopyProperties<UserDTO, User>(objUserToUpdate);
            }
            return ctx.SaveChanges() > 0;
        }
        public bool Delete(int userId)
        {
            var ObjUserToDelete = ctx.Users.Find(userId);
            if (ObjUserToDelete != null)
                ctx.Users.Remove(ObjUserToDelete);
            return ctx.SaveChanges() > 0;
        }

    }
}
