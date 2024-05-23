using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class LabelRL : ILabel
    {
        private readonly FundooContext fundooContext;

        public LabelRL(FundooContext fundooContext )
        {
            this.fundooContext = fundooContext; 
        }

        public labelEntity AddLabel(int NotesId, long UserId, string LabelName)
        {
            try
            {
                var result = fundooContext.Notes.FirstOrDefault(note => note.NotesId == NotesId && note.UserId == UserId);
                if (result != null)
                {
                    labelEntity label = new labelEntity();
                    label. NotesId = NotesId;
                    label. UserId = UserId;
                    label. LabelName = LabelName;
                    fundooContext.Add(label);
                    fundooContext.SaveChanges();
                    return label;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public object GetAllLabel()
        {
            try
            {
                var res = fundooContext.Notes.ToList();
                if (res != null)
                {
                    return res;
                }
                else
                {
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public labelEntity UpdateLabel(long UserId, long LabelId, string LabelName)
        {
            try
            {
                var result = fundooContext.Labels.FirstOrDefault(lab => lab.LabelId == LabelId && lab.UserId == UserId);

                if (result != null)
                {
                    result.LabelName = LabelName;
                    fundooContext.SaveChanges();
                    return result;
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
        public object DeleteLabelByLabelId(long LabelId, long UserId)
        {
            try
            {
                var result = fundooContext.Labels.FirstOrDefault(lab => lab.LabelId == LabelId && lab.UserId == UserId);
                if (result != null)
                {
                    fundooContext.Remove(result);
                    fundooContext.SaveChanges();
                    return result;
                }
                else
                {
                    return "Label Id is not present";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
