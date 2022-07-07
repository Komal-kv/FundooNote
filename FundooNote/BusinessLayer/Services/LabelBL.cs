using BusinessLayer.Interfaces;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class LabelBL : ILabelBL
    {
        ILabelRL labelRL;
        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }
        public async Task AddLabel(int UserId, int NoteId, string Labelname)
        {
            try
            {
                await labelRL.AddLabel(UserId, NoteId, Labelname);  
            }
            catch(Exception e)
            {
                throw e; 
            }
        }
    }
}
