using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LanchesMac.Context;
using LanchesMac.Models;
using Microsoft.AspNetCore.Authorization;
using LanchesMac.Settings;
using Microsoft.Extensions.Options;
using LanchesMac.Services.Interfaces;

namespace LanchesMac.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminLanchesController : Controller
    {
        private readonly string _pastaImagens;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly AppDbContext _context;
        private readonly IPhotoService _photoService;

        public AdminLanchesController(IOptions<ImagesSettings> myConfig, IWebHostEnvironment hostingEnvironment, AppDbContext context, IPhotoService photoService)
        {          
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _pastaImagens = Path.Combine(_hostingEnvironment.WebRootPath, myConfig.Value.PastaImagens);
            _photoService = photoService;
        }

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Lanches.Include(l => l.Categoria);
            return View(await appDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lanche = await _context.Lanches
                .Include(l => l.Categoria)
                .FirstOrDefaultAsync(m => m.LancheId == id);
            if (lanche == null)
            {
                return NotFound();
            }

            return View(lanche);
        }

        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Lanche lanche, IFormFile? anexo)
        {
            if (ModelState.IsValid)
            {
                if (anexo != null && anexo.Length > 0)
                {
                    var nomeArquivo = await _photoService.ProcessarFotoAsync(_pastaImagens, anexo);

                    if (nomeArquivo != null)
                    {
                        lanche.ImagemUrl = nomeArquivo;
                        lanche.ImagemThumbnailUrl = nomeArquivo; 
                    }
                }

                _context.Add(lanche);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome", lanche.CategoriaId);
            return View(lanche);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lanche = await _context.Lanches.FindAsync(id);
            if (lanche == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome", lanche.CategoriaId);
            return View(lanche);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Lanche lanche, IFormFile? anexo)
        {
            if (id != lanche.LancheId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (anexo != null && anexo.Length > 0)
                    {
                        var nomeArquivo = await _photoService.ProcessarFotoAsync(_pastaImagens, anexo);

                        if (nomeArquivo != null)
                        {
                            lanche.ImagemUrl = nomeArquivo;
                            lanche.ImagemThumbnailUrl = nomeArquivo;
                        }                       
                    }
                    _context.Update(lanche);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LancheExists(lanche.LancheId))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome", lanche.CategoriaId);
            return View(lanche);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lanche = await _context.Lanches
                .Include(l => l.Categoria)
                .FirstOrDefaultAsync(m => m.LancheId == id);
            if (lanche == null)
            {
                return NotFound();
            }

            return View(lanche);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lanche = await _context.Lanches.FindAsync(id);
            if (lanche != null)
            {
                _context.Lanches.Remove(lanche);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LancheExists(int id)
        {
            return _context.Lanches.Any(e => e.LancheId == id);
        }

        
    }
}
