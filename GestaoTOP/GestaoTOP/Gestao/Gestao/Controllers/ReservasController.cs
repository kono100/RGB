using System.Linq.Expressions;
using Gestao.Models;
using Gestao.Data;
using Gestao.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Gestao.Controllers
{
    public class ReservasController : Controller
    {
        private readonly IESContext _context;

        //METODO CONSTRUTOR PARA INJETAR O DB NO _CONTEXT E TER ACCESSO AOS DADOS
        public ReservasController(IESContext context)
        {
            this._context = context;
        }

        public async Task<IActionResult> Index()
        {
            //VAI PERCORRER A TABELA Reservas INTEIRA E VAI MANDAR ORDENADO PRA VIEW (FAMOSO SELECT * FROM)
            var Reservass = await _context.Reservas
                .Include(i => i.Morador)
                .OrderBy(d => d.Nome)
                .ToListAsync();
            return View(Reservass);
        }

        //GET: Reservas/CREATE
        [HttpGet]
        public IActionResult Create()
        {
            var instituicoes = _context.Morador.OrderBy(i => i.Nome).ToList();
            instituicoes.Insert(0, new Morador()
            {
                MoradorID = 0,
                Nome = "Selecione o Morador"

            });

            ViewBag.Morador = instituicoes;

            return View();
        }

        //POST Reservas/CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reservas Reservas)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(Reservas);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("Erro", "Não foi possível inserir os dados");
            }
            return View(Reservas);
        }

        //GET EDIT
        [HttpGet]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Reservas = await _context.Reservas.SingleOrDefaultAsync
                (m => m.ReservasID == id);

            if (Reservas == null)
            {
                return NotFound();
            }

            ViewBag.Instituicoes = new SelectList(_context.Morador.OrderBy(b => b.Nome),
                "MoradorID", "Nome", Reservas.fk_MoradorID);

            return View(Reservas);
        }

        private bool ReservasExists(long? id)
        {
            return _context.Reservas.Any(e => e.ReservasID == id);
        }


        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, Reservas Reservas)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Reservas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (!ReservasExists(Reservas.ReservasID))
                    {
                        return NotFound();
                    }
                }

                ViewBag.Instituicoes = new SelectList(_context.Morador.OrderBy(b => b.Nome),
                "MoradorID", "Nome", Reservas.fk_MoradorID);

                return RedirectToAction(nameof(Index));
            }
            return View(Reservas);
        }

        //DETALHES
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Reservas = await _context.Reservas.SingleOrDefaultAsync
                (m => m.ReservasID == id);

            _context.Morador.Where(i => Reservas.fk_MoradorID == i.MoradorID).Load();

            if (Reservas == null)
            {
                return NotFound();
            }
            return View(Reservas);
        }

        //GET DELETE
        [HttpGet]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Reservas = await _context.Reservas
                .SingleOrDefaultAsync(d => d.ReservasID == id);

            _context.Morador.Where(i => Reservas.fk_MoradorID == i.MoradorID).Load();

            if (Reservas == null)
            {
                return NotFound();
            }
            return View(Reservas);
        }

        //POST DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var Reservas = await _context.Reservas.SingleOrDefaultAsync
                (m => m.ReservasID == id);
            _context.Reservas.Remove(Reservas);
            await _context.SaveChangesAsync();

            TempData["Message"] = $"Reservas {Reservas.Nome.ToUpper()} foi removido";

            return RedirectToAction(nameof(Index));
        }




    }



}
