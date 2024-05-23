using BusinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class LabelBL:ILabelBL
    {
        private readonly ILabel ilabelRL;

        public LabelBL(ILabel ilabelRL)
        {
            this.ilabelRL = ilabelRL;
        }

        public labelEntity AddLabel(int NotesId, long UserId, string LabelName)
        {
            return ilabelRL.AddLabel(NotesId, UserId, LabelName);
        }
        public labelEntity UpdateLabel(long UserId, long LabelId, String LabelName)
        {
            return ilabelRL.UpdateLabel(UserId, LabelId, LabelName);
        }
        public object DeleteLabelByLabelId(long LabelId, long UserId)
        {
            return ilabelRL.DeleteLabelByLabelId(LabelId, UserId);
        }

        public object GetAllLable()
        {
            return ilabelRL.GetAllLabel();
        }
    }
}
