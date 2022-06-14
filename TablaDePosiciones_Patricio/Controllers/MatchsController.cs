using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TablaDePosiciones_Patricio.Data;
using TablaDePosiciones_Patricio.Models;
using TablaDePosiciones_Patricio.ViewModels.ViewMatchs;

namespace TablaDePosiciones_Patricio.Controllers
{
    public class MatchsController : Controller
    {
        //Nos permite ingresar a la base de datos
        private readonly ApplicationDbContext _context;

        public MatchsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult CMatchVM()
        {
            //crear la instancia
            var vm = new CreateMatchViewModel();
            //la vista ya debe tener la lista de equipos cargada
            vm.Teams = _context.Team.ToList();
            //retornar view de createMatchViewModel.cs
            return View(vm);
        }

        [HttpPost]
        public IActionResult CMatchVM(CreateMatchViewModel matchViewModel)
        {
            bool hasError = false;

            matchViewModel.Teams = _context.Team.ToList();          

            if (matchViewModel.HomeTeamId == matchViewModel.GuestTeamId)
            {
                ViewBag.ErrorMsg = ("Debe seleccionar un equipo. No pueden ser los mismos.");
                hasError = true;
            }

            if (matchViewModel.HomePoints < 0 || matchViewModel.GuestPoints < 0)
            {
                ViewBag.ErrorMsgPoint = ("Los puntuacion no puede ser menor a 0");
                hasError = true;
            }

            if (hasError == true)
            {
                return View(matchViewModel);
            }

            Match match = new Match()
            {
                HomeTeamId = matchViewModel.HomeTeamId,
                GuestTeamId = matchViewModel.GuestTeamId,
                HomePoints = matchViewModel.HomePoints,
                GuestPoints = matchViewModel.GuestPoints
            };

            _context.Match.Add(match);        
            _context.SaveChanges();
            
            TempData["mensaje"] = "El partido fue añadido exitosamente";
            //primero el action y despues el controller
            return RedirectToAction("Index", "TeamsRegistration");
        }

        [HttpGet]
        public IActionResult Historial()
        {
            List<HistorialViewModel> historial = _context.Match.Include(x => x.HomeTeam)
                .Include(x => x.GuestTeam).Select(item => new HistorialViewModel()
                {
                    Id = item.Id,
                    HomeTeamName = item.HomeTeam.Name,
                    GuestTeamName = item.GuestTeam.Name,
                    HomePoints = item.HomePoints,
                    GuestPoints = item.GuestPoints
                }).ToList();

            return View(historial);
        }

        //[HttpGet]
        //public IActionResult EditMatch(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }

        //    //obetener el Match
        //    var match = _context.Match.Find(id);
        //    if (match == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(match);
        //}

        //[HttpPost]
        //public IActionResult EditMatch(CreateMatchViewModel match)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Match.Update(match);
        //        _context.SaveChanges();
        //        //mensaje para avisar al cliente que se ha creado.
        //        TempData["mensaje"] = "El equipo se ha actualizado correctamente";
        //        //redireccion al index
        //        return RedirectToAction("Index", "TeamsRegistration");
        //    }
        //    return View();
        //}

        //[HttpGet]
        //public IActionResult DeleteMatch(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }

        //    //obetener el team
        //    var match = _context.Match.Find(id);
        //    if (match == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(match);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]//es una proteccion para que no se pueda enviar records masivamente con un bot
        //public IActionResult Delete(int? id)
        //{
        //    var match = _context.Match.Find(id);

        //    if (match == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Match.Remove(match);
        //    _context.SaveChanges();
        //    //mensaje para avisar al cliente que se ha creado.
        //    TempData["mensaje"] = "El equipo se ha eliminado correctamente";
        //    //redireccion al index
        //    return RedirectToAction("Index");
        //}
    }
}
