using BusinessLayer.Interface;
using ModelLayer.ForgetPassword;
using ModelLayer.ResetPasswordModel;
using ModelLayer.SessionLoginModel;
using ModelLayer.UserLoginModel.cs;
using ModelLayer.UserModel;
using ModelLayer.UserUpdateModel;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class UserBL:IUserBL
    {
        private readonly IUserRL iuserRL;
        public UserBL(IUserRL iuserRL)
        {
            this.iuserRL = iuserRL;
        }
        public UserEntity UserRegistration(UserRegistrationModel registrationModel)
        {
            return iuserRL.UserRegistration(registrationModel);
        }

        public object UserLogin(UserLoginModel loginModel)
        {
            return iuserRL.UserLogin(loginModel);
        }

        public object GetAllUser()
        {
            return iuserRL.GetAllUser();
        }
        public object UserupadteModel(long userId, UserUpdate_Model user)
        {
            return iuserRL.UserupadteModel(userId, user);
        }
        public object DeleteById(long userId, UserRegistrationModel user)
        {
            return iuserRL.DeleteById(userId, user);
        }
        public ForgotPasswordModel ForgotPassword(string Email)
        {
            return iuserRL.ForgotPassword(Email);
        }

        public bool ResetPassword(string Email, ResetPassword password)
        {
            return iuserRL.ResetPassword(Email, password);
        }
        public Object GetAllUsers1()
        {
            return iuserRL.GetAllUsers1();
        }
        public object GetById(long UserId)
        {
            return iuserRL.GetById(UserId);
        }

        public object GetByName(string FirstName)
        {
            return iuserRL.GetByName(FirstName);
        }

        public int GetCount()
        {
            return iuserRL.GetCount();
        }

        public object UpdateAndInsert(long UserId, UserUpdate_Model user)
        {
            return iuserRL.UpdateAndInsert(UserId, user);
        }

        public SessionLoginModel sessionLogin(UserLoginModel user)
        {
            return iuserRL.sessionLogin(user);
        }

        public object checkId(long UserId, UserUpdate_Model userUpdate_Model)
        {
            return iuserRL.checkId(UserId, userUpdate_Model);    
        }
    }
}
