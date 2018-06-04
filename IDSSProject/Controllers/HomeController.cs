using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IDSSProject.Models;

namespace IDSSProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Customer customer = new Customer();
            return View("InsertData", customer);
        }

        public ActionResult InsertData()
        {
            Customer customer = new Customer();

            return View(customer);
        }

        [HttpPost]
        public ActionResult Result(Customer customer)
        {
            //Mock - Implement Connection to Machine Learning Part here
            Outcome outcome = new Outcome();
            outcome.Success = true;

            outcome.FirstInfluenceType = "Level of Education:";
            outcome.FirstInfluenceValue = "Bachelor";
            outcome.FirstImageLink = "/Images/Influences/Bachelor.png";

            outcome.SecondInfluenceType = "Marital Status:";
            outcome.SecondInfluenceValue = "Married";
            outcome.SecondImageLink = "/Images/Influences/Married.png";

            outcome.ThirdInfluenceType = "Housing Loan:";
            outcome.ThirdInfluenceValue = "Existing";
            outcome.ThirdImageLink = "/Images/Influences/HousingLoan.png";

            outcome.FourthInfluenceType = "Personal Loan:";
            outcome.FourthInfluenceValue = "Existing";
            outcome.FourthImageLink = "/Images/Influences/PersonalLoan.png";

            outcome.FifthInfluenceType = "Contact Type:";
            outcome.FifthInfluenceValue = "Mobile";
            outcome.FifthImageLink = "/Images/Influences/Mobile.png";
            return View(outcome);
        }
    }
}