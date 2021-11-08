using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebASPCorePremiereApp.Models
{
    public class Auteur
    {
        public int Id { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }

        // Un auteur peut avoir ecrit plusieurs livres
        public virtual ICollection<Livre> Livres { get; set; }
    }
}
