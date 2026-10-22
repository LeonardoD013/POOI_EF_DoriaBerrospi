using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace POOI_EF_DoriaBerrospi.Models
{
    public class Consumidor
    {
        [Display(Name = "DNI")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "El DNI debe contener 8 caracteres Numericos")]
        public string dnicont { get; set; }

        [Display(Name = "Nombres")]
        [Required]
        public string nomcont { get; set; }

        [Display(Name = "Apellidos")]
        [Required]
        public string apecont { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        public string emailcont { get; set; }

        [Display(Name = "Pais")]
        [Required]
        public string idpais { get; set; }

        public Consumidor() { }

        public Consumidor(string dnicont, string nomcont, string apecont, string emailcont, string idpais)
        {
            this.dnicont = dnicont;
            this.nomcont = nomcont;
            this.apecont = apecont;
            this.emailcont = emailcont;
            this.idpais = idpais;
        }
    }
}