using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MassFacultyUpdateNew.Models
{
    public class User
    {
        [Required]
        public string EmailID { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
