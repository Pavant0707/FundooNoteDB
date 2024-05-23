using BusinessLayer.Interface;
using GreenPipes.Caching;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using ModelLayer.NotesRegisterModel;
using ModelLayer.ProductRegisisterModel;
using ModelLayer.ResponseModel;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System.Text;
using System.Text.Json;

namespace FundooNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBL inotes;
        private readonly IDistributedCache cache;
        private readonly FundooContext fundooContext;

        public NotesController(INotesBL inotes,IDistributedCache cache,FundooContext fundooContext)
        {
            this.inotes = inotes;
            this.cache = cache;
            this.fundooContext = fundooContext;
        }

        [HttpPost]
[Route("Register Notes")]

public IActionResult CreateNotes(NotesRegisterModel notesRegisterModel)
{
    try
    {

                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "UserId").Value);
                //long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "UserId").Value);
                //var result=inotes.CreateNotes(notesRegisterModel);
                //var result = inotes.CreateNotes(UserId,notesRegisterModel);
                Notes notes = inotes.CreateNotes(UserId, notesRegisterModel);
        if (notes != null)
        {
            return Ok(new ResponseModel<Notes> { success = true, message = "Notes added", data = notes });
        }
        else
        {
            return BadRequest(new ResponseModel<Notes> { success = false, message = "Try again" });
        }
    }
    catch (Exception ex)
    {

        throw;
    }
}
        [HttpPost]
        [Route("Update Notes")]
        public IActionResult UpdateNote(int NotesId, NotesRegisterModel notesRegisterModel)
        {
            try
            {

                var update = inotes.CreateNotes(NotesId, notesRegisterModel);
                if (update != null)
                {
                    return Ok(new { success = true, message = "notes updated", data = update });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Try again" });
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("Get Notes")]
        public IActionResult GetAllNotes()
        {
            try
            {

                var update = inotes.GetAllNotes();
                if (update != null)
                {
                    return Ok(new { success = true, message = "notes featched", data = update });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Try again" });
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("delete Notes")]
        public IActionResult DeleteNote(int NotesId, NotesRegisterModel notesRegisterModel)
        {
            try
            {

                var update = inotes.DeleteNote(NotesId, notesRegisterModel);
                if (update != null)
                {
                    return Ok(new { success = true, message = "notes delete", data = update });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Try again" });
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("Add color")]
        public IActionResult Addcolor(int NotesId, string color, long UserId)
        {
            try
            {

                var notes = inotes.Addcolor(NotesId, color, UserId);
                if (notes != null)
                {
                    return Ok(new { success = true, message = "added", data = notes });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Try again" });
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [Authorize]
        [HttpGet]
        [Route("Archive Notes")]
        public IActionResult ArchiveNotes(int NotesId)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "UserId").Value);

                var result = inotes.IsArchive(NotesId, UserId);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { success = true, message = "Archive  Or UnArchive Notes Successfully", data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { success = false, message = "Archive Notes Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpGet]
        [Route("Pin Notes")]
        public IActionResult PinNotes(int NotesId)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "UserId").Value);

                var result = inotes.PinNotes(NotesId, UserId);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { success = true, message = "pin  Or Unpin Notes Successfully", data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { success = false, message = "pin Notes Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpGet]
        [Route("GetNotesByCretaedDateOnly")]
        public IActionResult GetNotesByCreatedDateOnly(DateOnly createdDate)
        {
            var result = inotes.GetNotesByCretatedDateOnly(createdDate);

            if (result != null)
            {
                return Ok(new ResponseModel<object> { success = true, message = "Successfully ", data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<object> { success = true, message = "Failed" });
            }
        }
        [HttpGet]
        [Route("GetNotesByCretaedDatee")]
        public IActionResult GetNotesByCreatedDatee(DateTime createdDate)
        {
            try
            {
                var result = inotes.GetNotesByCreatedDate(createdDate);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { success = true, message = "Successfully Notes Retrieve By CreatedDate ", data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { success = true, message = "Failed To Retrieve Notes By CreatedDate" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpGet]
        [Route("GetNotesByTitleAndDescription")]
        public IActionResult GetNotesByTitleAndDescription(string Title, string Description)
        {
            try
            {
                var result = inotes.GetNotesByTitleAndDescription(Title, Description);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { success = true, message = "Succesfully Retrieve Notes By Title And Description  ", data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { success = false, message = "Failed To Retrieve Notes ", data = result });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Route("GetByFirstLetter")]
        public IActionResult GetNotesByFirstLetter(string FirstLetter)
        {
            var result = inotes.GetNotesByFirstLetter(FirstLetter);
            if (result != null)
            {
                return Ok(new ResponseModel<object> { success = true, message = "Successfully Retrieve By First Letter", data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<object> { success = false, message = "Failed ", data = result });
            }
        }
        [HttpGet]
        [Route("GetNotesByBNotesId")]
        public IActionResult GetNotesByNotesId(int NotesId)
        {
            try
            {
                var result = inotes.GetNotesByNotesId(NotesId);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { success = true, message = "Data Retrieve Succesfully", data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { success = false, message = "Data Retreive Failed " });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpGet]
        [Route("AddRemainder")]
        public IActionResult AddRemainder(long NotesId, DateTime Remainder)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "UserId").Value);

                var result = inotes.AddRemainder(UserId, NotesId, Remainder);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { success = true, message = "Add Remainder Successfully", data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { success = false, message = "Add Remainder Failed " });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Route("GetNotesByCreatedDate")]
        public IActionResult GetNotesByCreatedDate(DateTime created)
        {
            try
            {
                var result = inotes.GetNotesByNotesCreatedDate(created);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { success = true, message = "Data Retrieve Successfully", data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { success = false, message = "Data Retrieve Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Route("GetNotesByUserId")]
        public IActionResult GetNotesByUserId(long UserId)
        {
            try
            {
                var result = inotes.GetNotesByUserId(UserId);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { success = true, message = "Data Retrieve Successfully", data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { success = false, message = "Data Retrieve Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Route("GetNotesByIdAndDesc")]
        public IActionResult GetNotesByNotesIdAndDesc(long NotesId, string Description)
        {
            try
            {
                var result = inotes.GetNoteByIdAndDesc(NotesId, Description);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { success = true, message = "Data Retrieve Successfully", data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { success = true, message = "Data Not Retrieve Successfully" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpGet]
        [Route("TrashNotes")]
        public IActionResult TrashNotes(int NotesId)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "UserId").Value);

                var result = inotes.IsTrash(NotesId, UserId);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { success = true, message = "Trash Or UnTrashNotes Sucessfully", data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { success = false, message = "Trash Or UnTrash Failed " });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpGet]
        [Route("RedisCache")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            var cacheKey = "Pavan";
            string serializedNotedList;

            var NotesList = new List<Notes>();
            byte[] redisNotesList = await cache.GetAsync(cacheKey);
            if (redisNotesList != null)
            {
                serializedNotedList = Encoding.UTF8.GetString(redisNotesList);
                NotesList = JsonConvert.DeserializeObject<List<Notes>>(serializedNotedList);

            }
            else
            {
                NotesList = fundooContext.Notes.ToList();
                serializedNotedList = JsonConvert.SerializeObject(NotesList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotedList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await cache.SetAsync(cacheKey, redisNotesList, options);
            }
            return Ok(NotesList);
        }
        //find the count of notes for a user.
        [HttpGet]
        [Route("count")]
        public IActionResult countnotes(long UserId)
        {
            try
            {
                var result = inotes.countnotes(UserId);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { success = true, message = "count Successfully", data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { success = false, message = " Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Route("count2")]
        public IActionResult GetCount()
        {
            try
            {
                var result = inotes.GetCount();
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

        [HttpGet]
        [Route("GetNotesById")]
        public IActionResult GetNotesById(int NotesId)
        {
            try
            {
                var result = inotes.GetNotesById(NotesId);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { success = true, message = "Successfully", data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { success = false, message = " Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
