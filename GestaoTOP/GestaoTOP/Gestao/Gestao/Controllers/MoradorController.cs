using Gestao.Data;
using Gestao.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestao.Controllers
{
    public class MoradorController : Controller
    {
        private readonly IESContext _context;

        //METODO CONSTRUTOR PARA INJETAR O DB NO _CONTEXT E TER ACCESSO AOS DADOS
        public MoradorController(IESContext context)
        {
            this._context = context;
        }

        public async Task<IActionResult> Index()
        {
            //VAI PERCORRER A TABELA INTEIRA E VAI MANDAR ORDENADO PRA VIEW (FAMOSO SELECT * FROM)
            return View(await _context.Morador.OrderBy
                (c => c.Nome).ToListAsync());
        }

        //GET: CREATE
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

        //POST CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Morador Morador)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(Morador);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("Erro", "Não foi possível inserir os dados");
            }
            return View(Morador);

        }

        //GET EDIT
        [HttpGet]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Morador = await _context.Morador.SingleOrDefaultAsync
                (m => m.MoradorID == id);

            if (Morador == null)
            {
                return NotFound();
            }
            return View(Morador);
        }

        private bool MoradorExists(long? id)
        {
            return _context.Morador.Any(e => e.MoradorID == id);
        }


        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, Morador Morador)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Morador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (!MoradorExists(Morador.MoradorID))
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Morador);
        }

        //DETALHES
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Morador = await _context.Morador.SingleOrDefaultAsync
                (m => m.MoradorID == id);

            if (Morador == null)
            {
                return NotFound();
            }
            return View(Morador);
        }

        //GET DELETE
        [HttpGet]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Morador = await _context.Morador
                .SingleOrDefaultAsync(d => d.MoradorID == id);

            if (Morador == null)
            {
                return NotFound();
            }
            return View(Morador);
        }

        //POST DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var relacionados = _context.Veiculo.Where(r => r.fk_MoradorID == id);

            if (relacionados.Any())
            {
                TempData["Erro"] = $"Não é possível excluir a Morador, pois existem os Veiculos {relacionados} associados a ela.";
                return RedirectToAction(nameof(Index));

            }

            var Morador = await _context.Morador.SingleOrDefaultAsync
                    (m => m.MoradorID == id);
            _context.Morador.Remove(Morador);
            await _context.SaveChangesAsync();

            TempData["Message"] = $"Morador {Morador.Nome.ToUpper()} foi removido";

            return RedirectToAction(nameof(Index));
        }


    }
}
