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
    
    public partial class Indent
    {
        public long ID { get; set; }
        public long IndentNo { get; set; }
        public string WorkOrder { get; set; }
        public string ReferenceNo { get; set; }
        public string Status { get; set; }
    
        public virtual ArtWorkUpload ArtWorkUpload { get; set; }
        public virtual Order Order { get; set; }
    }
}