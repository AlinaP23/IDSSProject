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
using weka;
using weka.classifiers;
using weka.core;
using weka.core.converters;
using libsvm;
using weka.classifiers.functions;
using weka.classifiers.meta;

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
            string delimiter = ",";

            string header = "age, job, marital, education, default, housing, loan, contact, month, day_of_week, campaign, pdays, previous, poutcome, emp.var.rate, cons.price.idx, cons.conf.idx, euribor3m, nr.employed, y,";
            string clientData = customer.Age.ToString() + delimiter
                              + customer.Job + delimiter
                              + customer.MaritalStatus + delimiter
                              + customer.Education + delimiter
                              + customer.Credit + delimiter
                             // + customer.YearlyBalance.ToString() + delimiter
                              + customer.HousingLoan + delimiter
                              + customer.PersonalLoan + delimiter
                              + customer.ContactType + delimiter
                              + customer.LastContactMonth + delimiter
                              + customer.LastContactDay + delimiter
                              //+ customer.Duration.ToString() + delimiter
                              + customer.NumberOfContactsThis + delimiter
                              + customer.DaysSinceLastContact.ToString() + delimiter
                              + customer.NumberOfContactsPrior.ToString() + delimiter
                              + customer.OutcomePreviousCampaign + delimiter
                              + customer.EmploymentVariationRate + delimiter
                              + customer.ConsumerPriceIndex + delimiter
                              + customer.Euribor3Month + delimiter
                              + customer.NumberOfEmployees + delimiter
                              + " "+ delimiter;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(header);
            sb.AppendLine(clientData);
            System.IO.File.WriteAllText(filePath, sb.ToString());

            CSVLoader loader = new CSVLoader();
            loader.setSource(new java.io.File("C:/Users/D060248/Desktop/IDSS/PW3/IDSSProject/IDSSProject/csv-output/file.csv"));
            Instances instances = loader.getDataSet();

            Outcome outcome = MachineLearning(instances);
            
            //Mock - Implement Connection to Machine Learning Part here
            //Outcome outcome = new Outcome();
            //outcome.Success = true;

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

        public Outcome MachineLearning(Instances instances)
        {
            Outcome outcome = new Outcome();

            //load model 
            StackingC cls = (StackingC) SerializationHelper.read("C:/Users/D060248/Desktop/IDSS/PW3/IDSSProject/IDSSProject/MachineLearning/ultimateIdss.model");
            //predict outcome
            instances.setClassIndex(19);
            //double value = cls.classifyInstance(instances.instance(0));
            double[] values = cls.distributionForInstance(instances.instance(0));

            if (values[0] < values[1])
            {
                outcome.Success = false;
            }
            else
            {
                outcome.Success = true;
            }
            //get name of class value
            //String prediction = instances.classAttribute().value((int) value);
            //outcome.FirstInfluenceType = prediction;

            return outcome;
        }
    }
}