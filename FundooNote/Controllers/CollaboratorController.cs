using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.ResponseModel;
using ModelLayer.UserModel;
using RepositoryLayer.Entity;
using System.Diagnostics.Eventing.Reader;

namespace FundooNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorBL collaborator;

        public CollaboratorController(ICollaboratorBL collaborator)
        {
            this.collaborator = collaborator;
        }
        [HttpPost]
        [Route("AddCollaborator")]
        public IActionResult AddCollaborator( int NotesId, string CollaboratorEmail)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "UserId").Value);
                var result = collaborator.AddCollaborator( UserId,  NotesId,  CollaboratorEmail);
                if (result != null)
                {
                    return Ok(new ResponseModel<Collaborator> { success = true, message = "Registration Successful", data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<Collaborator> { success = false, message = "Registration Failed" });
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut]
        [Route("UpdateCollaborator")]
        public IActionResult UpdateCollaborator(int NotesId, string CollaboratorEmail)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "UserId").Value);
                var result = collaborator.UpdateCollaborator(UserId, NotesId, CollaboratorEmail);
                if (result != null)
                {
                    return Ok(new  { success = true, message = "update Successful", data = result });
                }
                else
                {
                    return BadRequest(new  { success = false, message = "update Failed" });
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [Authorize]
        [HttpGet]
        [Route("GetCollaborator")]
        public IActionResult GetAllCollaborator()
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "UserId").Value);
                var result = collaborator.GetAllCollaborator();
                if (result != null)
                {
                    return Ok(new { success = true, message = " Successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Failed" });
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("DeleteCollaborator")]
        public IActionResult DeleteCollaborator(int NotesId)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "UserId").Value);
                var result = collaborator.DeleteCollaborator( UserId,NotesId);
                if (result != null)
                {
                    return Ok(new { success = true, message = " Successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Failed" });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("DeleteColloboratorId")]
        public IActionResult DeleteByCollaboratorId(long CollaboratorId)
        {

            long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "UserId").Value);

            var result = collaborator.DeleteByCollaboratorId(UserId, CollaboratorId);
            if (result != null)
            {
                return Ok(new ResponseModel<object> { success = true, message = "Deleted Collaborator Id Successfully ", data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<object> { success = false, message = "Deleted Collaborator Id Failed ", data = result });
            }
        }
        //public object collaborated(long UserId)
        [Authorize]
        [HttpPost]
        [Route("ColloboratorId")]
        public IActionResult collaborated(long UserId)
        {

            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "UserId").Value);

            var result = collaborator.collaborated(userId);
            if (result != null)
            {
                return Ok(new ResponseModel<object> { success = true, message = " Collaborator Id Successfully ", data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<object> { success = false, message = " Collaborator Id Failed ", data = result });
            }
        }

    }
}
