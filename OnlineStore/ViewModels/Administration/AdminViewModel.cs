using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineStore.ViewModels.Administration
{
    public class AdminViewModel
    {
        [Required(ErrorMessage = "Wybierz użytkownika, który ma zostać administratorem")]
        [Display(Name = "Identyfikator użytkownika")]
        public string UserId { get; set; }

        [Display(Name = "Nazwa administratora")]
        public string UserName { get; set; }

        public AdminViewModel(string id, string name)
        {
            UserId = id;
            UserName = name;
        }

        // MVC requires public constructor
        public AdminViewModel() { }
    }
}