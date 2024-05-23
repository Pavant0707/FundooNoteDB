using ModelLayer.NotesRegisterModel;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface ILabel
    {
        public labelEntity AddLabel(int NotesId, long UserId, string LabelName);
        public object GetAllLabel();
        public labelEntity UpdateLabel(long UserId, long LabelId, String LabelName);
        public object DeleteLabelByLabelId(long LabelId, long UserId);     
    }
}
