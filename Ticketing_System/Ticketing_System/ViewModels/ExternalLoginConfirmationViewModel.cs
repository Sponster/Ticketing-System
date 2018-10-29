using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ticketing_System.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {

        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }
}