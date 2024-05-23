using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.ResponseModel;
using RepositoryLayer.Entity;

namespace FundooNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL ilableBL;

        public LabelController(ILabelBL ilableBL)
        {
            this.ilableBL = ilableBL;
        }

        [Authorize]
        [HttpPost]
        [Route("AddLabel")]
        public IActionResult AddLabel(int NotesId, string LabelName)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "UserId").Value);

                var result = ilableBL.AddLabel(NotesId, UserId, LabelName);

                if (result != null)
                {
                    return Ok(new ResponseModel<labelEntity> { success = true, message = "Label Added Successfullt", data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<labelEntity> { success = false, message = "Label Added Failed " });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
            [Authorize]
            [HttpPost]
            [Route("UpdateLabel")]
            public IActionResult UpdateLabel(long LabelId, String LabelName)
            {
                try
                {
                    long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "UserId").Value);
                    var result = ilableBL.UpdateLabel(LabelId, UserId, LabelName);

                    if (result != null)
                    {
                        return Ok(new ResponseModel<labelEntity> { success = true, message = "Update label Successfully", data = result });
                    }
                    else
                    {
                        return BadRequest(new ResponseModel<labelEntity> { success = false, message = "Update Label Failed" });
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        [Authorize]
        [HttpPost]
        [Route("Delete")]
        public IActionResult DeleteLabelByLabelId(long LabelId)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "UserId").Value);
                var result = ilableBL.DeleteLabelByLabelId(LabelId,UserId);

                if (result != null)
                {
                    return Ok(new  { success = true, message = "Update label Successfully", data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<labelEntity> { success = false, message = "Update Label Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [Authorize]
        [HttpPost]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            try
            {

                var result = ilableBL.GetAllLable();

                if (result != null)
                {
                    return Ok(new { success = true, message = " Successfully", data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<labelEntity> { success = false, message = " Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
    
}
