using BusinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class CollaboratorBL: ICollaboratorBL
    {
        private readonly ICollaboratorRL collaboratorRL;

        public  CollaboratorBL (  ICollaboratorRL collaboratorRL)
        {
            this.collaboratorRL = collaboratorRL;
        }

        public Collaborator AddCollaborator(long UserId, int NotesId, string CollaboratorEmail)
        {
            return collaboratorRL.AddCollaborator(UserId, NotesId, CollaboratorEmail);    
        }

        public object collaborated(long UserId)
        {
            return collaboratorRL.collaborated(UserId);
        }

        public object DeleteByCollaboratorId(long UserId, long CollaboratorId)
        {
            return collaboratorRL.DeleteByCollaboratorId(UserId, CollaboratorId);
        }

        public object DeleteCollaborator(long UserId, int NotesId)
        {
            return collaboratorRL.DeleteCollaborator(UserId, NotesId);
        }

        public object GetAllCollaborator()
        {
            return collaboratorRL.GetAllCollaborator();
        }

        public object UpdateCollaborator(long UserId, int NotesId, string CollaboratorEmail)
        {
            return collaboratorRL.UpdateCollaborator(UserId,NotesId, CollaboratorEmail);
        }
    }
}
