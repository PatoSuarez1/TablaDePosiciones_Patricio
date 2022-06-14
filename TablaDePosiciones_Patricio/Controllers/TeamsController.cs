using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TablaDePosiciones_Patricio.Data;
using TablaDePosiciones_Patricio.Models;

namespace TablaDePosiciones_Patricio.Controllers
{
    public class TeamsController : Controller
    {
        //Nos permite ingresar a la base de datos
        private readonly ApplicationDbContext _context;

        public TeamsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Team> teamList = _context.Team;
            
            return View(teamList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//es una proteccion para que no se pueda enviar records masivamente con un bot
        public IActionResult Create(Team team)
        {   //preguntamos si el estado del modelo es valido osea si fue cargado un team

            if (ModelState.IsValid)
            {
                bool nameAlreadyExists = _context.Team.Any(t => t.Name == team.Name);
                
                if (nameAlreadyExists)
                {
                    ModelState.AddModelError("Name", "El nombre de ese equipo ya existe.");
                    return View();
                }

                _context.Team.Add(team);

                _context.SaveChanges();

                //mensaje para avisar al cliente que se ha creado.
                TempData["mensaje"] = "El Equipo se ha creado correctamente";
                //redireccion al index
                return RedirectToAction("Index");

            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //obetener el team
            var team = _context.Team.Find(id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }
                
        [HttpPost]
        [ValidateAntiForgeryToken]//es una proteccion para que no se pueda enviar records masivamente con un bot
        public IActionResult Edit(Team team)
        {   //preguntamos si el estado del modelo es valido osea si fue cargado un team
            if (ModelState.IsValid)
            {
                _context.Team.Update(team);
                _context.SaveChanges();
                //mensaje para avisar al cliente que se ha creado.
                TempData["mensaje"] = "El equipo se ha actualizado correctamente";
                //redireccion al index
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //obetener el team
            var team = _context.Team.Find(id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//es una proteccion para que no se pueda enviar records masivamente con un bot
        public IActionResult DeleteTeam(int? id)
        {
            var team = _context.Team.Find(id);

            if (team == null)
            {
                return NotFound();
            }

            _context.Team.Remove(team);
            _context.SaveChanges();
            //mensaje para avisar al cliente que se ha creado.
            TempData["mensaje"] = "El equipo se ha eliminado correctamente";
            //redireccion al index
            return RedirectToAction("Index");
        }
    }
}
