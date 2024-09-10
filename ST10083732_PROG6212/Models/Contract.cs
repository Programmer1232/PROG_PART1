using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10083732_PROG6212.Models
{

}



namespace ContractMonthlyClaimSystemWPF.Models
{
    public class Contract
    {
        public int Id { get; set; }

        //Lindokuhle Mbatha
        public string ContractName { get; set; }

        //07 February 2015
        public DateTime StartDate { get; set; }

        //21 December 2020
        public DateTime EndDate { get; set; }
        //1000
        public decimal TotalValue { get; set; }
    }

    public enum ClaimStatus
    {
        Pending,
        Verified,
        Approved,
        Rejected
    }

    public class MonthlyClaim
    {
        public int Id { get; set; }

        //4362536152
        public int ContractId { get; set; }

        //12 March 2019
        public DateTime ClaimDate { get; set; }

        //R12 000
        public decimal Amount { get; set; }

        //Funeral Plan
        public string Description { get; set; }

        //Pending
        public ClaimStatus Status { get; set; }


        public string SupportingDocumentPath { get; set; }

        //Lindokuhle Funerals
        public virtual Contract Contract { get; set; }
    }
}
