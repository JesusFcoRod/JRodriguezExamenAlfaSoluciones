using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SL.Controllers
{
    public class BecaController : ApiController
    {
        [HttpPost]
        [Route("api/Beca/Add")]

        public IHttpActionResult Add([FromBody] ML.Beca beca)
        {
            ML.Result result = BL.Beca.Add(beca);

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
        [Route("api/Beca/Update")]

        public IHttpActionResult Update([FromBody] ML.Beca beca)
        {
            ML.Result result = BL.Beca.Update(beca);

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
        [Route("api/Beca/Delete/{IdBeca}")]

        public IHttpActionResult Delete(int IdBeca)
        {

            ML.Result result = BL.Beca.Delete(IdBeca);

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
        [Route("api/Beca/GetAll")]

        public IHttpActionResult GetAll()
        {
            ML.Result result = BL.Beca.GetAll();

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
        [Route("api/Beca/GetById/{IdBeca}")]

        public IHttpActionResult GetById(int IdBeca)
        {
            ML.Result result = BL.Beca.GetById(IdBeca);

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