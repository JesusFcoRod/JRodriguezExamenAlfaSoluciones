using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SL.Controllers
{
    public class AlumnoController : ApiController
    {

        [HttpPost]
        [Route("api/Alumno/Add")]

        public IHttpActionResult Add([FromBody] ML.Alumno alumno)
        {
            ML.Result result = BL.Alumno.Add(alumno);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("api/Alumno/Update")]

        public IHttpActionResult Update([FromBody] ML.Alumno alumno)
        {
            ML.Result result = BL.Alumno.Update(alumno);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("api/Alumno/Delete/{idMateria}")]

        public IHttpActionResult Delete(int IdAlumno)
        {

            ML.Result result = BL.Alumno.Delete(IdAlumno);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpGet]
        [Route("api/Alumno/GetAll")]

        public IHttpActionResult GetAll()
        {
            ML.Result result = BL.Alumno.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet]
        [Route("api/Alumno/GetById/{IdAlumno}")]

        public IHttpActionResult GetById(int IdAlumno)
        {
            ML.Result result = BL.Alumno.GetById(IdAlumno);

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