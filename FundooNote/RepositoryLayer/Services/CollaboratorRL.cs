using DatabaseLayer.Collaborator;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class CollaboratorRL : ICollaboratorRL
    {
        FundooContext fundooContext; 
        IConfiguration configuration;

        public CollaboratorRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }
    
        public async Task AddCollab(int UserId, int NoteId, EmailValidationCollab emailValidationCollab)
        {
            try
            {
                Collaborator col = new Collaborator();
                {
                    col.UserId = UserId;
                    col.NoteId = NoteId;
                    col.CollaboratorEmail = emailValidationCollab.CollaboratorEmail;
                };

                await fundooContext.Collabs.AddAsync(col);
                await fundooContext.SaveChangesAsync();
               
            }
            catch(Exception e)
            {

                throw e;
            }
        }
    }
}
