using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IDSSProject.Models
{
    public class Customer
    {
        // bank client data 
        public int Age { get; set; }
        public string Job { get; set; }
        public string MaritalStatus { get; set; }
        public string Education { get; set; }
        public int YearlyBalance { get; set; }
        public string HousingLoan { get; set; }
        public string PersonalLoan { get; set; }
        public string Credit { get; set; }
        // related with the last contact of the current campaign
        public string ContactType { get; set; }
        public int LastContactDay { get; set; }
        public string LastContactMonth { get; set; }
        public int Duration { get; set; }
        // other attributes
        public int NumberOfContactsThis { get; set; }
        public int NumberOfContactsPrior { get; set; }
        public int DaysSinceLastContact { get; set; }
        public string OutcomePreviousCampaign { get; set; }
        // social and economic context attributes
        public int EmploymentVariationRate { get; set; }
        public int ConsumerPriceIndex { get; set; }
        public int ConsumerConfidenceIndex { get; set; }
        public int Euribor3Month { get; set; }
        public int NumberOfEmployees { get; set; }

    }
}