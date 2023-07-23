using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace tieuluan_ban_giay.Models
{
    public class ResetPassword
    {
        [Required(ErrorMessage="Nhập mật khẩu mới")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("Mật khẩu mới",ErrorMessage ="Mật khẩu mới và mật khẩu cũ không trùng nhau")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string ResetCode { get; set; }
    }
}