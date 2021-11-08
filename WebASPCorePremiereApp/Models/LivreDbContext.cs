using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebASPCorePremiereApp.Models;

namespace WebASPCorePremiereApp.Models
{
    public class LivreDbContext : IdentityDbContext
    {
        public LivreDbContext(DbContextOptions<LivreDbContext> options) : base(options)
        {

        }

        public DbSet<Livre> Livres { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Auteur> Auteurs { get; set; }
    }
}
