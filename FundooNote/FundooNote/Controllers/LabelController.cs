using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNote.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LabelController : ControllerBase
    {
        FundooContext fundooContext;
        ILabelBL labelBL;

        public LabelController(FundooContext fundooContext, ILabelBL labelBL)
        {
            this.fundooContext = fundooContext;
            this.labelBL = labelBL;

        }
        [Authorize]
        [HttpPost("AddLabel/{NoteId}/{LabelName}")]
        public async Task<ActionResult> AddLabel(int NoteId, string LabelName)
        {
            try
            {
                var currentUser = HttpContext.User;

                int userid = Convert.ToInt32(currentUser.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var Label = fundooContext.Labels.Where(x => x.UserId == userid && x.NoteId == NoteId).FirstOrDefault();

                if (Label != null)
                {
                    return this.BadRequest(new { status = 301, isSuccess = false, Message = "Enter Distinct NoteId" });

                }
                await this.labelBL.AddLabel(userid, NoteId, LabelName);

                return this.Ok(new { status = 200, isSuccess = true, Message = "Label created successfully!" });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
