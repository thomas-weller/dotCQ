namespace BootstrapMvcSample.Controllers
{
    using System;
    using System.Web.Mvc;

    using WebTriq.Controllers;

    public class ErrorsController : ZeroController
    {
        public ActionResult General(Exception exception)
        {
            ViewBag.Title = "Error";

            return View("General", exception);
        }

        public ActionResult Http404()
        {

            ViewBag.Title = "Resource not found";

            return View("Http404");
        }

    }
}
