using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class AlumnoBecaController : Controller
    {
        [HttpGet]
        public ActionResult AlumnoGetAll()
        {
            ML.Alumno alumno = new ML.Alumno();
            ML.Result result = new ML.Result();

            result.Objects = new List<object>();

            try
            {
                using (var client = new HttpClient())
                {
                    string urlApi = ConfigurationManager.AppSettings["urlApi"];
                    client.BaseAddress = new Uri(urlApi);

                    var responseTask = client.GetAsync("Alumno/GetAll");
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var redTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        redTask.Wait();

                        foreach (var resultItem in redTask.Result.Objects)
                        {
                            ML.Alumno resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Alumno>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }
                    }
                    alumno.Alumnos = result.Objects;
                }

            }
            catch (Exception ex)
            {
                result.Correct = true;
                result.Exception = ex;
                result.ErrorMessage = ex.Message;
            }

            return View(alumno);
        }
        [HttpGet]

        public ActionResult BecasAsignadasByAlumno(int IdAlumno)
        {
            ML.AlumnoBeca alumnobeca = new ML.AlumnoBeca();
            ML.Result result = new ML.Result();

            result.Objects = new List<object>();

            try
            {
                using (var client = new HttpClient())
                {
                    string urlApi = ConfigurationManager.AppSettings["urlApi"];
                    client.BaseAddress = new Uri(urlApi);

                    var responseTask = client.GetAsync("AlumnoBeca/BecasAsignadas/" + IdAlumno);
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var redTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        redTask.Wait();

                        foreach (var resultItem in redTask.Result.Objects)
                        {
                            ML.AlumnoBeca resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.AlumnoBeca>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }
                    }
                    alumnobeca.AlumnosBecas = result.Objects;
                }

            }
            catch (Exception ex)
            {
                result.Correct = true;
                result.Exception = ex;
                result.ErrorMessage = ex.Message;
            }

            return View(alumnobeca);
        }
    }
}