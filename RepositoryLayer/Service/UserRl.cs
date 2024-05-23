using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelLayer.ForgetPassword;
using ModelLayer.ResetPasswordModel;
using ModelLayer.SessionLoginModel;
using ModelLayer.UserLoginModel.cs;
using ModelLayer.UserModel;
using ModelLayer.UserUpdateModel;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class UserRl : IUserRL
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;
        private readonly object userId;

        public UserRl(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;

        }

        public UserEntity UserRegistration(UserRegistrationModel registrationModel)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = registrationModel.FirstName;
                userEntity.LastName = registrationModel.LastName;
                userEntity.Email = registrationModel.Email;
                userEntity.Password = EncryptPassword(registrationModel.Password);


                fundooContext.UserTable.Add(userEntity);
                int result = fundooContext.SaveChanges();
                if (result != 0)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public object GetAllUser()
        {
            try
            {
                var getalluser = fundooContext.UserTable.ToList();
                if (getalluser != null)
                {
                    return getalluser;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public object UserLogin(UserLoginModel loginModel)
        {
            try
            {
                string encodedPassword = EncryptPassword(loginModel.Password);
                var userlogin = fundooContext.UserTable.FirstOrDefault(user => user.Email == loginModel.Email && user.Password == encodedPassword);


                if (userlogin != null)
                {
                    var token = GenerateToken(userlogin.UserId, userlogin.Email);
                    return token;

                }
                else
                {
                    return null;
                }
            }


            catch (Exception ex)
            {
                return ex;
            }


        }

        public static string EncryptPassword(string password)
        {
            try
            {
                byte[] encrypt_password = new byte[password.Length];
                encrypt_password = Encoding.UTF8.GetBytes(password);
                string encodedPassword = Convert.ToBase64String(encrypt_password);
                return encodedPassword;

            }
            catch (Exception ex)
            {
                return $"Encryption Failed.! {ex.Message}";
            }
        }


        public static string DecryptPassword(string encryptedPassword)
        {
            try
            {
                byte[] decrypt_password = Convert.FromBase64String(encryptedPassword);
                string originalPassword = Encoding.UTF8.GetString(decrypt_password);
                return originalPassword;


            }
            catch (Exception ex)
            {
                return $"Decryption Failed.! {ex.Message}";
            }
        }

        private string GenerateToken(long UserId, string Email)
        {
            // Create a symmetric security key using the JWT key specified in the configuration
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            // Create signing credentials using the security key and HMAC-SHA256 algorithm
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            // Define claims to be included in the JWT
            var claims = new[]
            {
          new Claim("Email",Email),
          new Claim("UserId", UserId.ToString())
      };
            // Create a JWT with specified issuer, audience, claims, expiration time, and signing credentials
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMonths(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public object UserupadteModel(long userId, UserUpdate_Model user)
        {
            try
            {
                var res = fundooContext.UserTable.FirstOrDefault(user => user.UserId == userId);
                if (res != null)
                {
                    res.FirstName = user.FirstName;
                    res.LastName = user.LastName;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public object DeleteById(long userId,UserRegistrationModel user)
        {
            try
            {
                var res = fundooContext.UserTable.FirstOrDefault(user => user.UserId == userId);
                if (res != null)
                {
                    fundooContext.UserTable.Remove(res);
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


 
        
        public ForgotPasswordModel ForgotPassword(string Email)
        {
            UserEntity user = fundooContext.UserTable.ToList().Find(user => user.Email == Email);

            ForgotPasswordModel forgotPasswordModel = new ForgotPasswordModel();
            forgotPasswordModel.UserId = user.UserId;
            forgotPasswordModel.Email = user.Email;
            forgotPasswordModel.Token = GenerateToken(user.UserId, user.Email);

            return forgotPasswordModel;

        }
        public bool ResetPassword(string Email, ResetPassword password)
        {
            UserEntity User = fundooContext.UserTable.ToList().Find(x => x.Email == Email);
            if (User != null)
            {
                User.Password = EncryptPassword(password.ConfirmPassword);
                fundooContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
        public object GetAllUsers1()
        {
            try
            {
                var getalluser=fundooContext.UserTable.ToList();
                if(getalluser.Count != null)
                {
                    return getalluser;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public object UpdateUser1(long UserId,UserUpdate_Model user)
        {
            var update=fundooContext.UserTable.FirstOrDefault(user=>user.UserId==UserId);
            if(update != null)
            {
                update.FirstName = user.FirstName;
                update.LastName = user.LastName;
                fundooContext.SaveChanges();
                return update;
            }
            else
            {
                return null;
            }
        }

        public SessionLoginModel sessionLogin(UserLoginModel user)
        {
            string encodedPassword = EncryptPassword(user.Password);
            var userlogin = fundooContext.UserTable.FirstOrDefault(user => user.Email == user.Email && user.Password == encodedPassword);

            if (userlogin != null)
            {
                SessionLoginModel sessionModel = new SessionLoginModel();
                sessionModel.UserId = userlogin.UserId;
                sessionModel.FirstName = userlogin.FirstName;
                sessionModel.LastName = userlogin.LastName;
                sessionModel.Email = userlogin.Email;
                sessionModel.Password = encodedPassword;
                sessionModel.Token = GenerateToken(userlogin.UserId, userlogin.Email);
                return sessionModel;

            }
            else
            {
                return null;
            }
        }

        public bool CheckEmailExist(string email)
        {
            try
            {
                var user=fundooContext.UserTable.FirstOrDefault(u => u.Email == email);
                if (user != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }

        public object GetById(long UserId)
        {
            try
            {
                var user = fundooContext.UserTable.FirstOrDefault(user => user.UserId == UserId);
                if (user != null)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public object GetByName(string FirstName)
        {
            try
            {
                var user = fundooContext.UserTable.FirstOrDefault(user => user.FirstName == FirstName);
                if (user != null)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public int GetCount()
        {
            try
            {
                var count = fundooContext.UserTable.Count();
                if (count > 0)
                {
                    return count;
                }
                else
                {
                    return 0;
                }
            }catch( Exception ex)
            {
                throw ex;
            }
        }

        public object UpdateAndInsert(long UserId, UserUpdate_Model user)
        {
            try
            {
                var user1=fundooContext.UserTable.FirstOrDefault(user=>user.UserId == UserId);  
                if (user1 != null)
                {
                   user1.FirstName = user.FirstName;
                    user1.LastName = user.LastName;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    UserEntity userEntity = new UserEntity();
                    userEntity.FirstName = user.FirstName;
                    userEntity.LastName = user.LastName;
                    //userEntity.Password = user.Password;
                    return userEntity;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public object checkId(long UserId, UserUpdate_Model userUpdate_Model)
        {
            try
            {
                var user = fundooContext.UserTable.FirstOrDefault(user => user.UserId == UserId);
                if (user != null)
                {
                    user.FirstName = user.FirstName;
                    user.LastName = user.LastName;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    UserEntity userEntity = new UserEntity();
                    userEntity.FirstName = user.FirstName;
                    userEntity.LastName = user.LastName;
                    userEntity.Email = user.Email;
                    userEntity.Password = user.Password;
                    return userEntity;
                   
                }
            }catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    }

    
