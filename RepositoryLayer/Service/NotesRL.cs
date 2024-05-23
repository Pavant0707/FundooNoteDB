using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ModelLayer.NotesRegisterModel;

using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class NotesRL : INotesRL
    {
        public readonly FundooContext fundooContext;
        public NotesRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        public Notes CreateNotes(long UserId, NotesRegisterModel notesRegisterModel)
        {
            try
            {
                Notes notes = new Notes();
                notes.Title = notesRegisterModel.Title;
                notes.Description = notesRegisterModel.Description;
                notes.Reminder = notesRegisterModel.Reminder;
                notes.color = notesRegisterModel.color;
                notes.Image = notesRegisterModel.Image;
                notes.IsArchive = notesRegisterModel.IsArchive;
                notes.IsPin = notesRegisterModel.IsPin;
                notes.IsTrash = notesRegisterModel.IsTrash;
                notes.CreatedAt = notesRegisterModel.CreatedAt;
                notes.UpdatedAt = notesRegisterModel.UpdatedAt;
                notes.UserId = UserId;
                fundooContext.Notes.Add(notes);
                fundooContext.SaveChanges();
                return notes;
            }catch (Exception ex)
            {
                throw ex;
            }
        }

        public object DeleteNote(int NotesId, NotesRegisterModel notesRegisterModel)
        {
            try
            {
                var notes = fundooContext.Notes.FirstOrDefault(notes => notes.NotesId == NotesId);
                if(notes != null)
                {
                    fundooContext.Notes.Remove(notes);
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
        public int IsArchive(int NotesId, long UserId)
        {
            try
            {
                var notes=fundooContext.Notes.FirstOrDefault(notes=>notes.NotesId == NotesId);
                if (notes != null)
                {
                    if (notes.IsArchive)
                    {
                        notes.IsArchive = false;
                        fundooContext.SaveChanges();
                        return 1;
                    }
                    else
                    {
                        notes.IsArchive= true;
                        fundooContext.SaveChanges();
                        return 2;
                    }
                }
                else
                {
                    return 3;
                }
            }catch (Exception ex)
            {
                throw ex;
            }
        }
        public int IsTrash(int NotesId, long UserId)
        {
            try
            {
                var notes = fundooContext.Notes.FirstOrDefault(notes => notes.NotesId == NotesId);
                if (notes != null)
                {
                    if (notes.IsArchive)
                    {
                        notes.IsArchive = false;
                        fundooContext.SaveChanges();
                        return 1;
                    }
                    else
                    {
                        notes.IsArchive = true;
                        fundooContext.SaveChanges();
                        return 2;
                    }
                }
                else
                {
                    return 3;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetAllNotes()
        {
            try
            {
                var notes= fundooContext.Notes.ToList();
                if(notes != null)
                {
                    return notes;
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

        public int PinNotes(int NotesId, long UserId)
        {
            try
            {
                Notes notes = fundooContext.Notes.FirstOrDefault(note=>note.NotesId==NotesId);
                if(notes.IsPin)
                {
                    notes.IsPin = false;
                    fundooContext.SaveChanges();
                    return 1;
                }
                else
                {
                    notes.IsPin=true;
                    fundooContext.SaveChanges();
                    return 2;
                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public object UpdateNote(int NotesId, NotesRegisterModel notesRegisterModel)
        {
            try
            {
                var update = fundooContext.Notes.FirstOrDefault(x => x.NotesId == NotesId);
                if (update != null)
                {
                 update.Title = notesRegisterModel.Title;
                 update.Description = notesRegisterModel.Description;
                 update.Reminder = notesRegisterModel.Reminder;
                 update.color = notesRegisterModel.color;
                 update.Image = notesRegisterModel.Image;
                 update.IsArchive = notesRegisterModel.IsArchive;
                 update.IsPin = notesRegisterModel.IsPin;
                 update.IsTrash = notesRegisterModel.IsTrash;
                 update.CreatedAt = notesRegisterModel.CreatedAt;
                 update.UpdatedAt = notesRegisterModel.UpdatedAt;
                 update.NotesId= NotesId;
                    fundooContext.SaveChanges();
                    return true;
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

        public bool Addcolor(int NotesId,string color, long UserId)
        {
            Notes notes = fundooContext.Notes.FirstOrDefault(note=>note.NotesId==NotesId);
            if(notes != null)
            {
                notes.color = color;
                fundooContext.SaveChanges();
                return true;    
            }
            else
            {
                return false;
            }
        }

        public object AddRemainder(long UserId, long NotesId, DateTime Remainder)
        {
            try
            {
                var user=fundooContext.Notes.FirstOrDefault(user=>user.UserId==UserId && user.NotesId==NotesId);
                if(user!=null) 
                {
                    if(Remainder> DateTime.Now)
                    {
                        fundooContext.Entry(user).State=EntityState.Modified;
                        fundooContext.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex) { 
            throw ex;   
            }
        }

        public object GetNoteByIdAndDesc(long NotesId, string Description)
        {
            try
            {
                var note=fundooContext.Notes.FirstOrDefault(notes=>notes.NotesId==NotesId && notes.Description==Description);
                if(note != null)
                {
                    return note;
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

        public object GetNotesByUserId(long UserId)
        {
            try
            {
                var user = fundooContext.Notes.FirstOrDefault(user=>user.UserId==UserId);
                if(user != null)
                {
                    return user;
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

        public object GetNotesByNotesCreatedDate(DateTime created)
        {
            try
            {
                var user = fundooContext.Notes.FirstOrDefault(user => user.CreatedAt==created);
                if(user != null)
                {
                    return user;
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

        public Notes GetNotesByNotesId(int NotesId)
        {
            try
            {
                var user = fundooContext.Notes.FirstOrDefault(note=>NotesId==NotesId);
                if(user != null)
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

        public object GetNotesByFirstLetter(string Title)
        {
            try
            {
                var user = fundooContext.Notes.Where(user=>user.Title.Contains('a'));
                if(user != null)
                {
                    return user;
                }else
                {
                    return null;
                }
            }catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetNotesByTitleAndDescription(string Title, string Description)
        {
            try
            {
                var user = fundooContext.Notes.FirstOrDefault(user => user.Title == Title && user.Description == Description);
                if(user != null)
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

        public object GetNotesByCreatedDate(DateTime CreatedDate)
        {
            try
            {
                var user = fundooContext.Notes.FirstOrDefault(user=>user.CreatedAt==CreatedDate);
                if(user != null)
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

        public object GetNotesByCretatedDateOnly(DateOnly CreatedDate)
        {
            try
            {
                var user = fundooContext.Notes.FirstOrDefault(user => user.CreatedAt.Equals(CreatedDate));
                if(user != null)
                {
                    return user;
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

        public int countnotes(long UserId)
        {
            try
            {
                var count = fundooContext.Notes.Count(user => user.UserId == UserId);
                if (count > 0)
                {
                    return count;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int GetCount()
        {
            try
            {
                var count = fundooContext.Notes.Count();
                if (count > 0)
                {
                    return count;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetNotesById(int NotesId)
        {
            try
            {
                var user = fundooContext.Notes.FirstOrDefault(notes => notes.NotesId == NotesId);
                if (user != null)
                {
                    return user;
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
        public object UploadImage(long NotesId, long UserId, IFormFile Image)
        {
            try
            {
                var note = fundooContext.Notes.FirstOrDefault(note => note.NotesId == NotesId && note.UserId == UserId);
                if (note != null)
                {
                    Account account = new Account("dm17wglra", "473225824619789", "xlA - YBS8lx79XbUxdR1i81hG9GE");

                    Cloudinary cloudinary = new Cloudinary(account);

                    ImageUploadParams uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(Image.FileName, Image.OpenReadStream()),
                        PublicId = note.Title
                    };

                    ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);

                    note.UpdatedAt = DateTime.Now;
                    note.Image = uploadResult.Url.ToString(); ;
                    fundooContext.SaveChanges();

                    return note;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}