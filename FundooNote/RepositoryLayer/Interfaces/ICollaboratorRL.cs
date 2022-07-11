using DatabaseLayer.Collaborator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface ICollaboratorRL
    {
        Task AddCollab(int UserId, int NoteId, EmailValidationCollab emailValidationCollab);
    }
}
