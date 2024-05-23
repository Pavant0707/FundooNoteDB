using ModelLayer.NotesRegisterModel;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface INotesBL
    {
        public Notes CreateNotes(long UserId, NotesRegisterModel notesRegisterModel);
        public object UpdateNote(int NotesId, NotesRegisterModel notesRegisterModel);
        public object DeleteNote(int NotesId, NotesRegisterModel notesRegisterModel);
        public object GetAllNotes();
        public int PinNotes(int NotesId,long UserId);
        public int IsArchive(int NotesId, long UserId);
        public int IsTrash(int NotesId, long UserId);
        public bool Addcolor(int NotesId, string color, long UserId);
        public object AddRemainder(long UserId, long NotesId, DateTime Remainder);

        public object GetNoteByIdAndDesc(long NotesId, string Description);

        public object GetNotesByUserId(long UserId);
        public object GetNotesByNotesCreatedDate(DateTime created);

        public object GetNotesByFirstLetter(string Title);

        public object GetNotesByTitleAndDescription(string Title, string Description);

        public object GetNotesByCreatedDate(DateTime CreatedDate);

        public object GetNotesByCretatedDateOnly(DateOnly CreatedDate);
        public int countnotes(long UserId);
        public int GetCount();
        public object GetNotesById(int NotesId);
        public Notes GetNotesByNotesId(int NotesId);
    }
}
