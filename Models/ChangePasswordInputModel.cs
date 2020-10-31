using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShadiWebApplication.Models
{
    public class ChangePasswordInputModel
    {
        [Required]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string ConfirmedNewPassword { get; set; }

    }
}
