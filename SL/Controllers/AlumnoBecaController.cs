using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SL.Controllers
{
    public class AlumnoBecaController : ApiController
    {

        [HttpGet]
        [Route("api/AlumnoBeca/BecasAsignadas/{IdAlumno}")]

        public IHttpActionResult BecasAsignadas(int IdAlumno)
        {
            ML.Result result = BL.AlumnoBeca.BecasAsignadasGetByIdAlumno(IdAlumno);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}