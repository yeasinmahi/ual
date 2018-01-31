using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UAL.BLL.DataModel
{
    public class Sample
    {
        [DisplayName("Reference No.")]
        public string Reference { get; set; }
        [Required]
        [DisplayName("Item Description")]
        public string Description { get; set; }
        [DisplayName("Brand Name")]
        public string Brand { get; set; }
        [Required]
        [DisplayName("Label")]
        public string Label { get; set; }
        [Required]
        [DisplayName("Combo")]
        public string Combo { get; set; }
        [Required]
        [DisplayName("Buyer")]
        public string Buyer { get; set; }
        [DisplayName("Comments")]
        public string comments { get; set; }
        [Required]
        [DisplayName("Version")]
        public string version { get; set; }
        public string ReceivedFrom { get; set; }
        public DateTime dateOfSubmission { get; set; }



    }
}