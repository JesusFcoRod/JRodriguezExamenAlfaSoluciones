using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class BecaController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Beca beca = new ML.Beca();
            ML.Result result = new ML.Result();

            result.Objects = new List<object>();

            try
            {
                using (var client = new HttpClient())
                {
                    string urlApi = ConfigurationManager.AppSettings["urlApi"];
                    client.BaseAddress = new Uri(urlApi);

                    var responseTask = client.GetAsync("Beca/GetAll");
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var redTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        redTask.Wait();

                        foreach (var resultItem in redTask.Result.Objects)
                        {
                            ML.Beca resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Beca>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }
                    }
                    beca.Becas = result.Objects;
                }

            }
            catch (Exception ex)
            {
                result.Correct = true;
                result.Exception = ex;
                result.ErrorMessage = ex.Message;
            }

            return View(beca);
        }

        [HttpGet]

        public ActionResult Form(int? IdBeca)
        {
            ML.Beca beca = new ML.Beca();

            if (IdBeca != null)
            {
                beca.IdBeca = IdBeca.Value;
                ML.Result result = new ML.Result();

                try
                {
                    using (var client = new HttpClient())
                    {
                        string urlApi = ConfigurationManager.AppSettings["urlApi"];
                        client.BaseAddress = new Uri(urlApi);

                        var responseTask = client.GetAsync("Beca/GetById/" + IdBeca);
                        responseTask.Wait();

                        var resultServicio = responseTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();

                            string usuarioCast = readTask.Result.Object.ToString();

                            ML.Beca resultItem = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Beca>(usuarioCast);
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
                    beca = (ML.Beca)result.Object;
                    return View(beca);
                }
                else
                {
                    ViewBag.Message = "Ocurrio algo al consultar la informacion de la beca" + result.ErrorMessage;
                    return PartialView("Modal");
                }
            }
            else
            {
                return View(beca);
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Beca beca)
        {
            using (var client = new HttpClient())
            {
                if (beca.IdBeca == 0)
                {
                    string urlApi = ConfigurationManager.AppSettings["urlApi"];
                    client.BaseAddress = new Uri(urlApi);

                    var postTask = client.PostAsJsonAsync<ML.Beca>("Beca/Add", beca);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Se ha registrado la beca";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "No se ha registrado la beca";
                        return PartialView("Modal");
                    }

                }
                else
                {
                    string urlApi = ConfigurationManager.AppSettings["urlApi"];
                    client.BaseAddress = new Uri(urlApi);

                    var postTask = client.PostAsJsonAsync<ML.Beca>("Beca/Update", beca);
                    postTask.Wait();

                    var result = postTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Se ha actualizado la beca";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "No se ha actualizado la beca";
                        return PartialView("Modal");
                    }

                }
            }
        }

        [HttpGet]
        public ActionResult Delete(int? IdBeca)
        {
            ML.Beca beca = new ML.Beca();
            beca.IdBeca = IdBeca.Value;

            using (var client = new HttpClient())
            {
                string urlApi = ConfigurationManager.AppSettings["urlApi"];
                client.BaseAddress = new Uri(urlApi);

                var postTask = client.GetAsync("Beca/Delete/" + IdBeca);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Mesagge = "Se ha eliminado la beca";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Mesagge = "No se ha eliminado la beca";
                    return PartialView("Modal");
                }
            }
        }
    }
}