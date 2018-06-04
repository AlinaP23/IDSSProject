using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IDSSProject.Models
{
    public class Customer
    {
        public int Age { get; set; }
        public string Job { get; set; }
        public string MaritalStatus { get; set; }
        public string Education { get; set; }
        public int YearlyBalance { get; set; }
        public bool HousingLoan { get; set; }
        public bool PersonalLoan { get; set; }
        public bool Credit { get; set; }
        public string ContactType { get; set; }
        public int LastContactDay { get; set; }
        public int LastContactMonth { get; set; }
        public int Duration { get; set; }
        public int NumberOfContactsThis { get; set; }
        public int NumberOfContactsPrior { get; set; }
        public int DaysSinceLastContact { get; set; }
        public bool OutcomePreviousCampaign { get; set; }
    }
}