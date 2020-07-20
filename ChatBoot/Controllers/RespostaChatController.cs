using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChatBoot.Data;
using ChatBoot.Models;

namespace ChatBoot.Controllers {
    public class RespostaChatController : Controller {
        private readonly ChatBootContext _db;

        public RespostaChatController(ChatBootContext db) {
            _db = db;
        }

        public async Task<IActionResult> Index() {
            return View(await _db.RespostasChat.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var respostaChat = await _db.RespostasChat.FirstOrDefaultAsync(m => m.Id == id);
            if (respostaChat == null) {
                return NotFound();
            }

            return View(respostaChat);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Mensagem,Resposta")] RespostaChat respostaChat) {
            if (ModelState.IsValid)
            {
                _db.Add(respostaChat);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(respostaChat);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var respostaChat = await _db.RespostasChat.FindAsync(id);
            if (respostaChat == null)
            {
                return NotFound();
            }
            return View(respostaChat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Mensagem,Resposta")] RespostaChat respostaChat)
        {
            if (id != respostaChat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(respostaChat);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RespostaChatExists(respostaChat.Id))
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
            return View(respostaChat);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var respostaChat = await _db.RespostasChat
                .FirstOrDefaultAsync(m => m.Id == id);
            if (respostaChat == null)
            {
                return NotFound();
            }

            return View(respostaChat);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var respostaChat = await _db.RespostasChat.FindAsync(id);
            _db.RespostasChat.Remove(respostaChat);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("api/Chat")]
        public async Task<JsonResult> Chat(RequestApi request) {
            var respostaChat = await _db.RespostasChat
                .Where(r => r.Mensagem.ToUpper().Contains(request.Mensagem.ToUpper()))
                .FirstOrDefaultAsync();

            if (respostaChat != null) {
                var resposta = new ResponseApi { Resposta = respostaChat.Resposta };

                return Json(resposta);
            } else {
                var resposta = new ResponseApi { Resposta = "Não entendi sua pergunta. Poderia reformular?" };

                return Json(resposta);
            }
        }

        private bool RespostaChatExists(int id)
        {
            return _db.RespostasChat.Any(e => e.Id == id);
        }
    }
}
