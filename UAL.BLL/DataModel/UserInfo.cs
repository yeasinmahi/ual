using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UAL.BLL.DataModel
{
    public class UserInfo
    {
        [Required]
        [DisplayName("User Name")]
        public string userName { get; set; }
        [DisplayName("Select User Type")]
        public string userType { get; set; }
        [Required]
        [DisplayName("Password")]
        public string Password { get; set; }

    }
}