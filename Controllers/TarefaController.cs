using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using trilha_net_mvc_desafio.Context;
using trilha_net_mvc_desafio.Models;

namespace trilha_net_mvc_desafio.Controllers
{
    public class TarefaController : Controller
    {
        private readonly OrganizadorContext _context;

        public TarefaController (OrganizadorContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var tarefas = _context.Tarefas.ToList();
            return View(tarefas);
        }

        public IActionResult NovaTarefa ()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NovaTarefa (Tarefa tarefa)
        {
            if(ModelState.IsValid)
            {
                _context.Tarefas.Add(tarefa);
               
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(tarefa);
        }

        public IActionResult DetalhesTarefa(int id)
        {
           var tarefa = _context.Tarefas.Find(id);
            return View(tarefa);
        }

        public IActionResult EditaTarefa(int id)
        {
            var tarefa = _context.Tarefas.Find(id);
            if (tarefa ==null)
            {
                return NotFound("Tarefa não Encontrada");
            }
            return View(tarefa);
        }

        [HttpPost]
        public IActionResult EditaTarefa(Tarefa tarefa)
        {
            var tarefaMod = _context.Tarefas.Find(tarefa.Id);
            if (tarefaMod ==null)
            {
                return NotFound("Tarefa não Encontrada");
            }

            tarefaMod.Titulo = tarefa.Titulo;
            tarefaMod.Descricao = tarefa.Descricao;
            tarefaMod.Status = tarefa.Status;
            tarefaMod.Data = tarefa.Data;

            _context.Tarefas.Update(tarefaMod);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeletaTarefa(int id)
        {
            var tarefa = _context.Tarefas.Find(id);
            if (tarefa ==null)
            {
                return NotFound("Tarefa não Encontrada");
            }
            return View(tarefa);
        }

        [HttpPost]
        public IActionResult DeletaTarefa(Tarefa tarefa)
        {
           var tarefaMod = _context.Tarefas.Find(tarefa.Id);

           _context.Tarefas.Remove(tarefaMod);
           _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}