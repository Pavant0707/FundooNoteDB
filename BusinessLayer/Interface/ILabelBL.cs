using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ILabelBL
    {
        public labelEntity AddLabel(int NotesId, long UserId, string LabelName);
        public labelEntity UpdateLabel(long UserId, long LabelId, String LabelName);
        public object DeleteLabelByLabelId(long LabelId, long UserId);
        public object GetAllLable();
    }
}
