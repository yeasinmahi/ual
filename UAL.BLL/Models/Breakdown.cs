//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UAL.BLL.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Breakdown
    {
        public string PONumber { get; set; }
        public string Style1 { get; set; }
        public string Style2 { get; set; }
        public string Style3 { get; set; }
        public string Color { get; set; }
        public string ReferenceNo { get; set; }
        public string OrderNo { get; set; }
    
        public virtual ArtWorkUpload ArtWorkUpload { get; set; }
        public virtual Order Order { get; set; }
    }
}
