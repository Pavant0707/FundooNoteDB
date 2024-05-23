using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ICollaboratorBL
    {
        public Collaborator AddCollaborator(long UserId, int NotesId, string CollaboratorEmail);
        public object UpdateCollaborator(long UserId, int NotesId, string CollaboratorEmail);
        public object DeleteCollaborator(long UserId,int NotesId);
        public object DeleteByCollaboratorId(long UserId, long CollaboratorId);
        public object GetAllCollaborator();
        public object collaborated(long UserId);
    }
}
