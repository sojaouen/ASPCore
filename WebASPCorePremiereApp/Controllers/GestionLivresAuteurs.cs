﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebASPCorePremiereApp.Models;

namespace WebASPCorePremiereApp.Controllers
{
    public class GestionLivresAuteurs : Controller
    {
        private LivreDbContext _context;
        public GestionLivresAuteurs(LivreDbContext livreDbContext)
        {
            _context = livreDbContext;
        }
        public IActionResult AssocierLivreAuteur()
        {
            // On va passer a la vue une liste de livres et une liste d'auteurs
            ViewData["Livres"] = new SelectList(_context.Livres, "Id", "Titre");
            ViewData["Auteurs"] = new SelectList(_context.Auteurs, "Id", "Nom");
            return View();
        }

        [HttpPost]

        public IActionResult AssocierLivreAuteur(int livreId, int auteurId)
        {
            Livre livreAAssocier = _context.Livres.Find(livreId);
            Auteur AuteurAAssocier = _context.Auteurs.Find(auteurId);

            if(livreAAssocier.Auteurs == null)
            {
                livreAAssocier.Auteurs = new List<Auteur>();
            }
            livreAAssocier.Auteurs.Add(AuteurAAssocier);
            _context.SaveChanges();

            ViewData["Livres"] = new SelectList(_context.Livres, "Id", "Titre");
            ViewData["Auteurs"] = new SelectList(_context.Auteurs, "Id", "Nom");

            return View();
        }
    }
}
