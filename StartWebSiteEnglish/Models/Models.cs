using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StartWebSiteEnglish.Models
{

    //public class UserContext : DbContext
    //{
    //    public UserContext() : base("EnglishSiteDB") { }

    //    public DbSet<User> Users { get; set; }
    //}   

    public class User
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Your name")]
        [Required(ErrorMessage = "Field can't be empty")]
        public string Name { get; set; }

        [Display(Name = "Email addres")]
        [Required(ErrorMessage = "Field can't be empty")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Неправильный пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public DateTime DateRegistration { get; set; }

        public string PhotoUrl { get; set; }

        public int Complexity { get; set; }

        public int LevelProgress { get; set; }

        public int Age { get; set; }

    }

    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "Введите Ваш логин")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class RegisterModel
    {
        [Display(Name = "Email addres")]
        [Required(ErrorMessage = "Введите Ваш Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Display(Name = "Your name")]
        [Required(ErrorMessage = "Field can't be empty")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Подтвердите пароль")]
        [DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

    }

    public class ForgotPasswordViewModel
    {
        public string Email { get; set; }
    }

    public class EditPassword: ResetPassword
    {
        [Required(ErrorMessage = "Введите старый пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class ResetPassword
    {
        [Required(ErrorMessage = "Введите новый пароль")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Подтвердите новый пароль")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmNewPassword { get; set; }
    }
}