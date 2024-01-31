using System.Linq.Expressions;
using Gestao.Data;
using Gestao.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Gestao.Controllers
{
    public class VeiculoController : Controller
    {
        private readonly IESContext _context;

        //METODO CONSTRUTOR PARA INJETAR O DB NO _CONTEXT E TER ACCESSO AOS DADOS
        public VeiculoController(IESContext context)
        {
            this._context = context;
        }

        public async Task<IActionResult> Index()
        {
            //VAI PERCORRER A TABELA Veiculo INTEIRA E VAI MANDAR ORDENADO PRA VIEW (FAMOSO SELECT * FROM)
            var Veiculos = await _context.Veiculo
                .Include(i => i.Morador)
                .OrderBy(d => d.Placa)
                .ToListAsync();
            return View(Veiculos);
        }

        //GET: Veiculo/CREATE
        [HttpGet]
        public IActionResult Create()
        {
            var instituicoes = _context.Morador.OrderBy(i => i.Nome).ToList();
            instituicoes.Insert(0, new Morador()
            {
                MoradorID = 0,
                //VisitanteID = 0,
                Nome = "Selecione o Morador"

            });

            ViewBag.Morador = instituicoes;

            return View();
        }

        //POST Veiculo/CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Veiculo Veiculo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(Veiculo);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("Erro", "Não foi possível inserir os dados");
            }
            return View(Veiculo);
        }

        //GET EDIT
        [HttpGet]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Veiculo = await _context.Veiculo.SingleOrDefaultAsync
                (m => m.VeiculoID == id);

            if (Veiculo == null)
            {
                return NotFound();
            }

            ViewBag.Instituicoes = new SelectList(_context.Morador.OrderBy(b => b.Nome),
                "MoradorID", "Nome", Veiculo.fk_MoradorID);

            return View(Veiculo);
        }

        private bool VeiculoExists(long? id)
        {
            return _context.Veiculo.Any(e => e.VeiculoID == id);
        }


        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, Veiculo Veiculo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Veiculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (!VeiculoExists(Veiculo.VeiculoID))
                    {
                        return NotFound();
                    }
                }

                ViewBag.Instituicoes = new SelectList(_context.Morador.OrderBy(b => b.Nome),
                "MoradorID", "Nome", Veiculo.fk_MoradorID);

                return RedirectToAction(nameof(Index));
            }
            return View(Veiculo);
        }

        //DETALHES
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Veiculo = await _context.Veiculo.SingleOrDefaultAsync
                (m => m.VeiculoID == id);

            _context.Morador.Where(i => Veiculo.fk_MoradorID == i.MoradorID).Load();

            if (Veiculo == null)
            {
                return NotFound();
            }
            return View(Veiculo);
        }

        //GET DELETE
        [HttpGet]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Veiculo = await _context.Veiculo
                .SingleOrDefaultAsync(d => d.VeiculoID == id);

            _context.Morador.Where(i => Veiculo.fk_MoradorID == i.MoradorID).Load();

            if (Veiculo == null)
            {
                return NotFound();
            }
            return View(Veiculo);
        }

        //POST DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var Veiculo = await _context.Veiculo.SingleOrDefaultAsync
                (m => m.VeiculoID == id);
            _context.Veiculo.Remove(Veiculo);
            await _context.SaveChangesAsync();

            TempData["Message"] = $"Veiculo {Veiculo.Placa.ToUpper()} foi removido";

            return RedirectToAction(nameof(Index));
        }




    }



}
