using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
            customer.Credit = "unknown";
            customer.HousingLoan = "unknown";
            customer.PersonalLoan = "yes";
            customer.Age = 13;
            customer.ContactType = "telephone";
            customer.Job = "housemaid";
            customer.MaritalStatus = "single";

            return View("InsertData", customer);
        }

        public ActionResult InsertData()
        {
           
            Customer customer = new Customer();
            customer.Credit = "unknown";
            customer.HousingLoan = "unknown";
            customer.PersonalLoan = "unknown";

            return View(customer);
        }

        [HttpPost]
        public ActionResult Result(Customer customer)
        {
            //Mock - Implement Connection to Machine Learning Part here
            string filePath = @"C:\Users\D060248\Desktop\IDSS\PW3\IDSSProject\IDSSProject\csv-output\file.csv";
            string delimiter = ";";

            string header = "age; job; marital; education; default; balance; housing; loan; contact; day; month; duration; campaign; pdays; previous; poutcome";
            string clientData = customer.Age.ToString() + delimiter
                              + customer.Job + delimiter
                              + customer.MaritalStatus + delimiter
                              + customer.Education + delimiter
                              + customer.Credit + delimiter
                              + customer.YearlyBalance.ToString() + delimiter
                              + customer.HousingLoan + delimiter
                              + customer.PersonalLoan + delimiter
                              + customer.ContactType + delimiter
                              + customer.LastContactDay.ToString() + delimiter
                              + customer.LastContactMonth + delimiter
                              + customer.Duration.ToString() + delimiter
                              + customer.NumberOfContactsThis + delimiter
                              + customer.DaysSinceLastContact.ToString() + delimiter
                              + customer.NumberOfContactsPrior.ToString() + delimiter
                              + customer.OutcomePreviousCampaign + delimiter;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(header);
            sb.AppendLine(clientData);
            System.IO.File.WriteAllText(filePath, sb.ToString());

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