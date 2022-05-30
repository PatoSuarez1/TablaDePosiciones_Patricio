using System.Collections.Generic;
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
        //Http Get Index
        public IActionResult Index()
        {
            IEnumerable<Team> teamList = _context.Team;
            
            return View(teamList);
        }
        //Http Get Create
        public IActionResult Create()
        {
            return View();
        }

        //Http Post Create
        [HttpPost]
        [ValidateAntiForgeryToken]//es una proteccion para que no se pueda enviar records masivamente con un bot
        public IActionResult Create(Team team)
        {   //preguntamos si el estado del modelo es valido osea si fue cargado un team

            if (ModelState.IsValid)
            {
                _context.Team.Add(team);

                _context.SaveChanges();

                var records = new TeamRegistration
                {                    
                    Played = 0,
                    Won = 0,
                    Drawn = 0,
                    Lost = 0,
                    GoalsFavor = 0,
                    GoalsAgainst = 0,
                    GoalDifference = 0,
                    Points = 0,   
                };


                records.TeamId = team.Id;

                _context.TeamRegistration.Add(records);
             
                _context.SaveChanges();

                //mensaje para avisar al cliente que se ha creado.
                TempData["mensaje"] = "El Equipo se ha creado correctamente";
                //redireccion al index
                return RedirectToAction("Index");

            }

            return View();
        }

        //Http Get Edit
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

        //Http Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]//es una proteccion para que no se pueda enviar records masivamente con un bot
        public IActionResult Edit(Team team)
        {   //preguntamos si el estado del modelo es valido osea si fue cargado un team
            if (ModelState.IsValid)
            {
                _context.Team.Update(team);
                _context.SaveChanges();
                //mensaje para avisar al cliente que se ha creado.
                TempData["mensaje"] = "El team se ha actualizado correctamente";
                //redireccion al index
                return RedirectToAction("Index");
            }
            return View();
        }

        //Http Get Delete
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

        //Http Post Delete
        [HttpPost]
        [ValidateAntiForgeryToken]//es una proteccion para que no se pueda enviar records masivamente con un bot
        public IActionResult DeleteEquipo(int? id)
        {
            var team = _context.Team.Find(id);

            if (team == null)
            {
                return NotFound();
            }

            _context.Team.Remove(team);
            _context.SaveChanges();
            //mensaje para avisar al cliente que se ha creado.
            TempData["mensaje"] = "El team se ha eliminado correctamente";
            //redireccion al index
            return RedirectToAction("Index");
        }
    }
}
