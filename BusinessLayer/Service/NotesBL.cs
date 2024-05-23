using BusinessLayer.Interface;
using ModelLayer.NotesRegisterModel;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class NotesBL : INotesBL
    {
        public readonly INotesRL inotes;

        public NotesBL(INotesRL inotes)
        {
            this.inotes = inotes;
        }

        public bool Addcolor(int NotesId, string color, long UserId)
        {
            return inotes.Addcolor(NotesId, color, UserId);
        }

        public object AddRemainder(long UserId, long NotesId, DateTime Remainder)
        {
            return inotes.AddRemainder(NotesId, UserId, Remainder);
        }

        public int countnotes(long UserId)
        {
            return inotes.countnotes(UserId);
        }

        public Notes CreateNotes(long UserId, NotesRegisterModel notesRegisterModel)
        {
            return inotes.CreateNotes(UserId, notesRegisterModel);
        }

        public object DeleteNote(int NotesId, NotesRegisterModel notesRegisterModel)
        {
            return inotes.DeleteNote(NotesId, notesRegisterModel);
        }

        public object GetAllNotes()
        {
            return inotes.GetAllNotes();
        }

        public int GetCount()
        {
            return inotes.GetCount();
        }

        public object GetNoteByIdAndDesc(long NotesId, string Description)
        {
            return inotes.GetNoteByIdAndDesc(NotesId, Description);
        }

        public object GetNotesByCreatedDate(DateTime CreatedDate)
        {
            return inotes.GetNotesByCreatedDate((DateTime) CreatedDate);
        }

        public object GetNotesByCretatedDateOnly(DateOnly CreatedDate)
        {
            return inotes.GetNotesByCretatedDateOnly(CreatedDate);
        }

        public object GetNotesByFirstLetter(string Title)
        {
            return inotes.GetNotesByFirstLetter((string) Title);
        }

        public object GetNotesById(int NotesId)
        {
            return inotes.GetNotesById(NotesId);
        }

        public object GetNotesByNotesCreatedDate(DateTime created)
        {
            return inotes.GetNotesByCreatedDate(created);
        }

        public Notes GetNotesByNotesId(int NotesId)
        {
            return inotes.GetNotesByNotesId(NotesId);
        }

        public object GetNotesByTitleAndDescription(string Title, string Description)
        {
            return inotes.GetNotesByTitleAndDescription((string) Title, Description);
        }

        public object GetNotesByUserId(long UserId)
        {
            return inotes.GetNotesByUserId(UserId);
        }

        public int IsArchive(int NotesId, long UserId)
        {
            return inotes.IsArchive(NotesId, UserId); 
        }

        public int IsTrash(int NotesId, long UserId)
        {
            return inotes.IsTrash(NotesId, UserId);
        }

        public int PinNotes(int NotesId, long UserId)
        {
            return inotes.PinNotes(NotesId, UserId);
        }

        public object UpdateNote(int NotesId, NotesRegisterModel notesRegisterModel)
        {
            return inotes.UpdateNote(NotesId, notesRegisterModel);
        }
    }
}
