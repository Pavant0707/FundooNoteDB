using ModelLayer.ForgetPassword;
using ModelLayer.ResetPasswordModel;
using ModelLayer.SessionLoginModel;
using ModelLayer.UserLoginModel.cs;
using ModelLayer.UserModel;
using ModelLayer.UserUpdateModel;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {

        public UserEntity UserRegistration(UserRegistrationModel registrationModel);
        public object UserLogin(UserLoginModel loginModel);
        public object GetAllUser();
        public object UserupadteModel(long userId, UserUpdate_Model user);
        public object DeleteById(long userId, UserRegistrationModel user);
        public ForgotPasswordModel ForgotPassword(string email);
        public bool ResetPassword(string Email, ResetPassword password);
        public Object GetAllUsers1();
        public object GetById(long UserId);
        public object GetByName(string FirstName);
        public int GetCount();
        public object UpdateAndInsert(long UserId, UserUpdate_Model user);
        public SessionLoginModel sessionLogin(UserLoginModel user);
        public object checkId(long UserId, UserUpdate_Model userUpdate_Model);

    }

}
