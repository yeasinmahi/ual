using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace UAL.BLL.DataModel
{
    public class Pricelist
    {
        [DisplayName("Trim Description")]
        public string Description { get; set; }

        [DisplayName("REF #")]
        public string Reference { get; set; }
        [DisplayName("Option/Combo")]
        public string Combo { get; set; }
        public string Finishing { get; set; }
        [DisplayName("BRAND")]
        public string Brand { get; set; }
        public string Division { get; set; }

        [DisplayName("Ex-Facory PRICE / Dzn.(US$)")]
        public string Price { get; set; }
        public System.DateTime ApprovalDate { get; set; }
        public System.DateTime sentForApprovalDate { get; set; }
        public string label { get; set; }
        public string buyer { get; set; }
        public System.DateTime dateOfSubmission { get; set; }
        public string ApprovedBy { get; set; }
        public string ReceivedFrom { get; set; }
    }
}