using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Web.Mvc;
using IDSSProject.Models;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;

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
            string filePath = @"C:\Users\D060248\Desktop\IDSS\PW3\IDSSProject\IDSSProject\csv-output\file.csv";
            string delimiter = ";";

            string header = "age; job; marital; education; default; balance; housing; loan; contact; day; month; duration; campaign; pdays; previous; poutcome; emp.var.rate; cons.price.idx; cons.conf.idx; euribor3m; nr.employed;";
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
                              + customer.OutcomePreviousCampaign + delimiter
                              + customer.EmploymentVariationRate + delimiter
                              + customer.ConsumerPriceIndex + delimiter
                              + customer.Euribor3Month + delimiter
                              + customer.NumberOfEmployees + delimiter;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(header);
            sb.AppendLine(clientData);
            System.IO.File.WriteAllText(filePath, sb.ToString());

            //string htmlResponse = Post("localhost:8000/getPrediction/", "csv=" + sb.ToString());
            
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

        public static string Post(string url, object postData)
        {
            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(url);

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, postData);

            byte[] data = ms.ToArray();

            httpWReq.Method = "POST";
            httpWReq.ContentType = "application/x-www-form-urlencoded";
            httpWReq.ContentLength = data.Length;

            using (Stream newStream = httpWReq.GetRequestStream())
            {
                newStream.Write(data, 0, data.Length);
            }

            HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();

            Stream stream = response.GetResponseStream();

            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");

            StreamReader streamReader = new StreamReader(stream, encode);

            string html = streamReader.ReadToEnd();

            response.Close();

            streamReader.Close();

            return html;
        }
    }
}