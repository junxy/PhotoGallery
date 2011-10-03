using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PhotoGallery.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [DisplayName("电子邮件")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DisplayName("密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("记住我?")]
        public bool RememberMe { get; set; }

    }
}