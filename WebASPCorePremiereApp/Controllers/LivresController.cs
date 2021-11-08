using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebASPCorePremiereApp.Models;

namespace WebASPCorePremiereApp.Controllers
{
    [Authorize]
    public class LivresController : Controller
    {
        private readonly LivreDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LivresController(LivreDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Livres
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var livreDbContext = _context.Livres.Include(l => l.Theme);
            return View(await livreDbContext.ToListAsync());
        }

        // GET: Livres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livre = await _context.Livres
                .Include(l => l.Theme)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livre == null)
            {
                return NotFound();
            }

            return View(livre);
        }

        // GET: Livres/Create
        public IActionResult Create()
        {
            // On envoie la liste des themes a la vue qui va les afficher dans un DownList
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Libelle");
            return View();
        }

        // POST: Livres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Livre livre, IFormFile Photo)
        {
            if (ModelState.IsValid)
            {
                // Verifier si le fichier est valide
                // Enregistrer sur un dossier(partage de fichiers, ...)
                // 1- Recuperer le chemin du dossier root du site
                string rootPath = _webHostEnvironment.WebRootPath;

                string fileName = Path.GetFileNameWithoutExtension(Photo.FileName) + Guid.NewGuid() + Path.GetExtension(Photo.FileName);
                //On cree le chemin du dichier qui va contenir l'image (chemin du fichier de destination)
                string path = Path.Combine(rootPath + "/Images/", fileName);
                // On copie la Photo dans le fichier de destination
                // FileAs -- n'existe plus alors on copie physiquement a l'aide du filestream
                var fileStream = new FileStream(path, FileMode.Create); // On peut utiliser un using
                await Photo.CopyToAsync(fileStream); // Faire une copie asynchrone
                fileStream.Close(); // Liberer la ressource


                // recuperer le nom du fichier dans l'objet Livre

                livre.Photo = fileName;


                if (Photo != null && Photo.Length > 0)
                {
                    // 1 - Recuperer le dossier de sauvegarde
                    // A l'aide des variables d'environnement du site
                }
                _context.Add(livre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Libelle", livre.ThemeId);
            return View(livre);
        }

        // GET: Livres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livre = await _context.Livres.FindAsync(id);
            if (livre == null)
            {
                return NotFound();
            }
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Libelle", livre.ThemeId);
            return View(livre);
        }

        // POST: Livres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titre,Annee,Resume,ISBN,ThemeId")] Livre livre)
        {
            if (id != livre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(livre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivreExists(livre.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Libelle", livre.ThemeId);
            return View(livre);
        }

        // GET: Livres/Delete/5
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livre = await _context.Livres
                .Include(l => l.Theme)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livre == null)
            {
                return NotFound();
            }

            return View(livre);
        }

        // POST: Livres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var livre = await _context.Livres.FindAsync(id);
            _context.Livres.Remove(livre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LivreExists(int id)
        {
            return _context.Livres.Any(e => e.Id == id);
        }

        // Recuperer les 3 derniers livres enregistres

        public IActionResult GetLast3()
        {
            var listNew = _context.Livres.OrderByDescending(x => x.Id).Take(3).ToList();

            // Retourner une vue partielle

            return PartialView("_livreNouveau", listNew);
        }
    }
}
