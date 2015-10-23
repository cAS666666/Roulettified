using System;
using System.Web.Mvc;
using Roulettified.ViewModels;
using Roulettified.Utilities;
using System.Collections;

namespace Roulettified.Controllers
{
    public class HomeController : Controller
    {
        private PseudoRandomNumber prng = new PseudoRandomNumber();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Spin")]
        public ActionResult SpinPost(SpinViewModel spinModel)
        {
            // Save number of spins to session storage
            Session["spins"] = Convert.ToInt32(Session["spins"]) + 1;

            // Get pseudo random number (not truly random like radioactive decay)
            int compChoice = prng.getRandomNumber();

            // Save the spin to history
            ArrayList history = (ArrayList)Session["history"];
            int matchResult = 0;
            if(spinModel.choice == compChoice) { matchResult = 1; }

            history.Add(matchResult);
            Session["history"] = history;

            /* 
                Here we would normally persist Session["spins"] and spinModel.choice had we used
                accounts and database support to allow history for the player.
            */

            SpinViewResponseModel response = new SpinViewResponseModel();
            response.status = "ok";
            response.msg = "";
            response.spins = Convert.ToInt32(Session["spins"]);
            response.compChoice = compChoice;
            response.history = history;

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("Reset")]
        public ActionResult ResetPost()
        {
            // Clearing sessionState
            Session["history"] = new ArrayList();
            Session["spins"] = 0;

            ResetViewResponseModel response = new ResetViewResponseModel();
            response.status = "ok";
            response.reset = true;

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}