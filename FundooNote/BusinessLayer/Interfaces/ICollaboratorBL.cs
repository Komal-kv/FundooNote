using DatabaseLayer.Collaborator;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ICollaboratorBL
    {
        Task AddCollab(int UserId, int NoteId, EmailValidationCollab emailValidationCollab);
    }
}
