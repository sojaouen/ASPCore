using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebASPCorePremiereApp.Models
{
    public class Livre
    {
        public int Id { get; set; }
        [Required, MinLength(5)]
        public string Titre { get; set; }
        [Display(Name ="Annee de sortie")]
        [Range(1800,2030)]
        public int Annee { get; set; }
        public string Resume { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string ISBN { get; set; }
        public string Photo { get; set; }

        public int ThemeId { get; set; }

        // Un livre peut avoir plusieurs auteurs

        public virtual ICollection<Auteur> Auteurs { get; set; }

        // Proprietes de navigation
        public Theme Theme { get; set; }
    }
}
