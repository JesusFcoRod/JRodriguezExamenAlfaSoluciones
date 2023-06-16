using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class AlumnoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
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

        public ActionResult Form(int? IdAlumno)
        {
            ML.Alumno alumno = new ML.Alumno();

            if (IdAlumno != null)
            {
                alumno.IdAlumno = IdAlumno.Value;
                ML.Result result = new ML.Result();

                try
                {
                    using (var client = new HttpClient())
                    {
                        string urlApi = ConfigurationManager.AppSettings["urlApi"];
                        client.BaseAddress = new Uri(urlApi);

                        var responseTask = client.GetAsync("Alumno/GetById/" + IdAlumno);
                        responseTask.Wait();

                        var resultServicio = responseTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();

                            string usuarioCast = readTask.Result.Object.ToString();

                            ML.Alumno resultItem = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Alumno>(usuarioCast);
                            result.Object = resultItem;
                            result.Correct = true;

                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.Exception = ex;
                    result.ErrorMessage = ex.Message;
                }
                if (result.Correct)
                {
                    alumno = (ML.Alumno)result.Object;
                    return View(alumno);
                }
                else
                {
                    ViewBag.Message = "Ocurrio algo al consultar la informacion del alumno" + result.ErrorMessage;
                    return PartialView("Modal");
                }
            }
            else
            {
                return View(alumno);
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Alumno alumno)
        {
            using (var client = new HttpClient())
            {
                if (alumno.IdAlumno == 0)
                {
                    string urlApi = ConfigurationManager.AppSettings["urlApi"];
                    client.BaseAddress = new Uri(urlApi);

                    var postTask = client.PostAsJsonAsync<ML.Alumno>("Alumno/Add", alumno);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Se ha registrado el alumno";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "No se ha registrado el alumno";
                        return PartialView("Modal");
                    }

                }
                else
                {
                    string urlApi = ConfigurationManager.AppSettings["urlApi"];
                    client.BaseAddress = new Uri(urlApi);

                    var postTask = client.PostAsJsonAsync<ML.Alumno>("Alumno/Update", alumno);
                    postTask.Wait();

                    var result = postTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Se ha actualizado el alumno";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "No se ha actualizado el alumno";
                        return PartialView("Modal");
                    }

                }
            }
        }

        [HttpGet]
        public ActionResult Delete(int? IdAlumno)
        {
            ML.Alumno alumno = new ML.Alumno();
            alumno.IdAlumno = IdAlumno.Value;

            using (var client = new HttpClient())
            {
                string urlApi = ConfigurationManager.AppSettings["urlApi"];
                client.BaseAddress = new Uri(urlApi);

                var postTask = client.GetAsync("Alumno/Delete/" + IdAlumno);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Mesagge = "Se ha eliminado el alumno";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Mesagge = "No se ha eliminado el alumno";
                    return PartialView("Modal");
                }
            }
        }


    }
}