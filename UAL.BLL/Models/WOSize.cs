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
    
    public partial class WOSize
    {
        public WOSize()
        {
            this.Sales = new HashSet<Sale>();
        }
    
        public string sn1 { get; set; }
        public Nullable<int> s1 { get; set; }
        public string sn2 { get; set; }
        public Nullable<int> s2 { get; set; }
        public string sn3 { get; set; }
        public Nullable<int> s3 { get; set; }
        public string sn4 { get; set; }
        public Nullable<int> s4 { get; set; }
        public string sn5 { get; set; }
        public Nullable<int> s5 { get; set; }
        public string sn6 { get; set; }
        public Nullable<int> s6 { get; set; }
        public string sn7 { get; set; }
        public Nullable<int> s7 { get; set; }
        public string sn8 { get; set; }
        public Nullable<int> s8 { get; set; }
        public string sn9 { get; set; }
        public Nullable<int> s9 { get; set; }
        public string sn10 { get; set; }
        public Nullable<int> s10 { get; set; }
        public string sn11 { get; set; }
        public Nullable<int> s11 { get; set; }
        public string sn12 { get; set; }
        public Nullable<int> s12 { get; set; }
        public string sn13 { get; set; }
        public Nullable<int> s13 { get; set; }
        public string sn14 { get; set; }
        public Nullable<int> s14 { get; set; }
        public long id { get; set; }
    
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
