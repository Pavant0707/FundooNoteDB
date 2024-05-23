using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class CollaboratorRL: ICollaboratorRL
    {
        private readonly FundooContext fundooContext;

        public CollaboratorRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public Collaborator AddCollaborator(long UserId, int NotesId, string CollaboratorEmail)
        {
            try
            {
                Collaborator collaborator = new Collaborator();
                collaborator.UserId = UserId;
                collaborator.NotesId = NotesId;
                collaborator.CollaboratorEmail = CollaboratorEmail;
                fundooContext.Add(collaborator);
                fundooContext.SaveChanges();
                return collaborator;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object collaborated(long UserId)
        {
            try
            {
                var user = fundooContext.Collaborator.FirstOrDefault(user => user.UserId == UserId);
                if (user == null)
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

        public object DeleteByCollaboratorId(long UserId, long CollaboratorId)
        {
            try
            {
                var res = fundooContext.Collaborator.FirstOrDefault(user => UserId == UserId && user.CollaboratorId == CollaboratorId);
                if (res != null)
                {
                    fundooContext.Remove(res);
                    fundooContext.SaveChanges();
                    return res;
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

        public object DeleteCollaborator(long UserId, int NotesId)
        {
            try
            {
                var result = fundooContext.Collaborator.FirstOrDefault(user=>UserId==UserId && user.NotesId==NotesId);
                if(result == null)
                {
                    fundooContext.Remove(result);
                    fundooContext.SaveChanges() ;
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

        public object GetAllCollaborator()
        {
            try
            {
                var res = fundooContext.Collaborator.ToList();
                if(res != null)
                {
                    return res;
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

        public object UpdateCollaborator(long UserId, int NotesId, string CollaboratorEmail)
        {
            try
            {
                var result = fundooContext.Collaborator.FirstOrDefault(user => user.UserId == UserId && user.NotesId == NotesId);
                if (result != null)
                {
                    result.CollaboratorEmail = CollaboratorEmail;
                    fundooContext.SaveChanges();
                    return result; 
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
    }
}
