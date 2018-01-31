using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UAL.BLL.DataModel
{
    public class detailedSample
    {
        public string Reference { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Label { get; set; }
        public string Combo { get; set; }
        public string Buyer { get; set; }
        public string comments { get; set; }
        public string version { get; set; }
        public DateTime dateOfSubmission { get; set; }
        public string Price { get; set; }
        public string Finishing { get; set; }
        public string NoteAtFooter { get; set; }
        public DateTime dateOfApproval { get; set; }
        public DateTime sentForApprovalDate { get; set; }
        public string ReceivedFrom { get; set; }
    }
}