using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebASPCorePremiereApp.Models
{
    public class Theme
    {
        public int Id { get; set; }
        [Required, MinLength(3)]
        public string Libelle { get; set; }
        [Required, MinLength(3, ErrorMessage = "La note doit etre superieure a 3 caracteres")]
        public string Note { get; set; }

        // Dans une thematique on peut avoir plusieurs livres

        public virtual ICollection<Livre> Livres { get; set; }

    }
}
