using DatabaseLayer.Collaborator;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface ICollaboratorRL
    {
        Task AddCollab(int UserId, int NoteId, EmailValidationCollab emailValidationCollab);
        Task DeleteCollab(int UserId, int NoteId);
        Task<List<Collaborator>> GetAllCollab(int UserId);
    }
}
