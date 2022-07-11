using BusinessLayer.Interfaces;
using DatabaseLayer.Collaborator;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CollaboratorBL : ICollaboratorBL
    {
        ICollaboratorRL collabRL;
        public CollaboratorBL(ICollaboratorRL collabRL)
        {
           this.collabRL = collabRL;
        }
        public async Task AddCollab(int UserId, int NoteId, EmailValidationCollab emailValidationCollab)
        {
            try
            {
                await this.collabRL.AddCollab(UserId, NoteId, emailValidationCollab);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task DeleteCollab(int UserId, int NoteId)
        {
            try
            {
                await this.collabRL.DeleteCollab(UserId, NoteId);
            }
            catch(Exception e)
            {

                throw e;
            }
        }

        public async Task<List<Collaborator>> GetAllCollab(int UserId)
        {
            try
            {
               return await this.collabRL.GetAllCollab(UserId);
            }
            catch(Exception e)
            {

                throw e;
            }
        }
    }
}
