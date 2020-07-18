using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Domain.ViewModels.Identity
{
    public class RegisterUserViewModel
    {
        [Required]
        [MinLength(3, ErrorMessage = "Минимальная длина 3 символа")]
        [Display(Name = "Имя пользователя")]
        [Remote("IsNameFree", "Account")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
