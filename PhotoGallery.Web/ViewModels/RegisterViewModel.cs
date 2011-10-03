using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PhotoGallery.Web.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [DisplayName("电子邮件")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DisplayName("密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DisplayName("确认密码")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}