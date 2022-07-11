using DatabaseLayer.Collaborator;
using RepositoryLayer.Services.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ICollaboratorBL
    {
        Task AddCollab(int UserId, int NoteId, EmailValidationCollab emailValidationCollab);
        Task DeleteCollab(int UserId, int NoteId);
        Task<List<Collaborator>> GetAllCollab(int UserId);
    }
}
