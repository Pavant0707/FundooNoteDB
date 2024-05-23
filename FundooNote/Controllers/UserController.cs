using BusinessLayer.Interface;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ModelLayer.ForgetPassword;
using ModelLayer.ResetPasswordModel;
using ModelLayer.ResponseModel;
using ModelLayer.SendModelLayer;
using ModelLayer.UserLoginModel.cs;
using ModelLayer.UserModel;
using ModelLayer.UserUpdateModel;
using RepositoryLayer.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using RepositoryLayer.Migrations; 
  using static MassTransit.Logging.DiagnosticHeaders;

namespace FundooNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL iuserBL;
        private readonly IBus bus;
       // private readonly ILogger<UserController> logger;



        public UserController(IUserBL iuserBL, IBus ibus)
        {
            this.iuserBL = iuserBL;
            this.bus = bus;
            //this.logger = logger;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult RegisterUser(UserRegistrationModel registrationModel)
        {
            try
            {
                var result = iuserBL.UserRegistration(registrationModel);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Registration Successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Registration Failed" });
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("UserGetAll1")]
        public IActionResult GetAllUser()
        {
            try
            {
                var result = iuserBL.GetAllUser();
                if (result != null)
                {
                    //throw new Exception("Error Occured");
                    return Ok(new { success = true, message = "Data Feached", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Try again" });
                }
            }
            catch (Exception ex)
            {
               // logger.LogError(ex.ToString());
                throw ex;
            }
        }
        [HttpPost]
        [Route("Loginuser")]
        public IActionResult UserLogin(UserLoginModel loginModel)
        {
            try
            {
                var res = iuserBL.UserLogin(loginModel);
                if (res != null)
                {
                    return Ok(new { success = true, message = "login suceesfull", data = res });

                }
                else
                {
                    return Ok(new { success = false, message = "try again" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetAllUSer()
        {
            try
            {
                var result = iuserBL.GetAllUser();
                if (result != null)
                {
                    return Ok(new { success = true, message = "Data feach", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "no data found" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
        //[HttpPost]
        //[Route("ForgotPassword")]
        //public async Task<IActionResult> ForgotPassword(string Email)
        //{

        //    var password = iuserBL.ForgotPassword(Email);

        //    if (password != null)
        //    {
        //        SendModelLayer send = new SendModelLayer();
        //        ForgotPasswordModel forgotPasswordModel = iuserBL.ForgotPassword(Email);
        //        send.SendMail(forgotPasswordModel.Email, forgotPasswordModel.Token);
        //        Uri uri = new Uri("rabbitmq:://localhost/FunDooNotesEmailQueue");
        //        var endPoint = await bus.GetSendEndpoint(uri);
        //        await endPoint.Send(forgotPasswordModel);
        //        return Ok(new { success = true, message = "Mail sent Successfully",Data = password.Token });
        //    }
        //    else
        //    {
        //        return BadRequest(new { success = false, message = "Email Does not Exist" });
        //    }


        //}
        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string Email)
        {

            var password = iuserBL.ForgotPassword(Email);

            if (password != null)
            {
                SendModelLayer send = new SendModelLayer();
                ForgotPasswordModel forgotPasswordModel = iuserBL.ForgotPassword(Email);
                send.SendMail(forgotPasswordModel.Email, forgotPasswordModel.Token);
                Uri uri = new Uri("rabbitmq:://localhost/FunDooNotesEmailQueue");
                var endPoint = await bus.GetSendEndpoint(uri);
                await endPoint.Send(forgotPasswordModel);
                return Ok(new { success = true, message = "Mail sent Successfully", data = password.Token });
            }
            else
            {
                return BadRequest(new { success = false, message = "Email Does not Exist" });
            }


        }


        [Authorize]
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(ResetPassword reset)
        {
            string Email = User.Claims.FirstOrDefault(x => x.Type == "Email").Value;
            var res = iuserBL.ResetPassword(Email, reset);
            if (res)
            {
                return Ok(new { success = true, message = "Password Reset is done" });

            }
            else
            {
                return BadRequest("Password is not Updated");
            }
        }
        [HttpPost]
        [Route("UpdateUser")]
        public IActionResult UserUpadteModel(long userId, UserUpdate_Model user)
        {
            try
            {
                var result = iuserBL.UserupadteModel(userId, user);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Data Update", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "no data found" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]
        [Route("UpdateUser1")]
        public IActionResult UserUpadteModel1(long userId, UserUpdate_Model user)
        {
            try
            {
                var result = iuserBL.UserupadteModel(userId, user);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Data Update", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "no data found" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]
        [Route("Delete")]
        public IActionResult DeleteById(long userId, UserRegistrationModel user)
        {
            try
            {
                var result = iuserBL.DeleteById(userId, user);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Data Deleted", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "no data found" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
       [HttpGet]
        [Route("Get by id")]
        public IActionResult GetById(long userId)
        {
            try
            {
                var result = iuserBL.GetById(userId);
                if (result != null) 
                {
                    return Ok(new { success = true, message = "get by id", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "no data found" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet]
        [Route("Get by name")]
        public IActionResult GetByName(string FirstName)
        {
            try
            {
                var result = iuserBL.GetByName(FirstName);
                if (result != null)
                {
                    return Ok(new { success = true, message = "get by Name", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "no data found" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet]
        [Route("co.u.nt")]
        public IActionResult GetCount()
        {
            try
            {
                var result = iuserBL.GetCount();
                if (result != null)
                {
                    return Ok(new { success = true, message = "get count", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "no data found" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]
        [Route("update and insert")]
        public IActionResult UpdateAndInsert(long UserId, UserUpdate_Model user)
        {
            try
            {
                var result = iuserBL.UpdateAndInsert(UserId, user);
                if (result != null)
                {
                    return Ok(new { success = true, message = "UpdateAndInsert", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "no data found" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]
        [Route("Session login")]
        public IActionResult sessionLogin(UserLoginModel user)
        {
            try
            {
                var result = iuserBL.sessionLogin(user);
                if (result != null)
                {
                    return Ok(new { success = true, message = "login", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]
        [Route("Check_id")]
        public IActionResult checkId(long UserId, UserUpdate_Model userUpdate_Model)
        {
            try
            {
                var result = iuserBL.checkId(UserId, userUpdate_Model);
                if (result != null)
                {
                    return Ok(new { success = true, message = "login", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "no" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        //find the count of notes for a user.
         //2) search a note using any parameter from note table and show details of it
 //   3) find the users who have collaborated

        //    [HttpPost]
        //    [Route("UserLoginMethod")]
        //    public IActionResult UserLoginMethod(UserLoginModel loginModel)
        //    {
        //        var result = iuserBL.sessionLogin(loginModel);


    //        if (result != null)
    //        {

    //            HttpContext.Session.SetInt32("UserID", (int)result.UserId);

    //            return Ok(new ResponseModel<object> { success = true, message = "Login Successfully ", data = result });
    //        }
    //        else
    //        {
    //            return BadRequest(new ResponseModel<object> { success = false, message = "Login Failed ", data = result });
    //        }
    //    }
    }
    }

