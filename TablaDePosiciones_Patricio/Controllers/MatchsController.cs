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
        public IActionResult EditMatch(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //obetener el Match
            var match = _context.Match.Find(id);
            if (match == null)
            {
                return NotFound();
            }
            //Pasar datos de match a la viewmodel
            EditMatchViewModel editMatchView = new EditMatchViewModel()
            {
                Id = match.Id,
                HomeTeamId = (int)match.HomeTeamId,
                HomePoints = match.HomePoints,
                GuestTeamId = (int)match.GuestTeamId,
                GuestPoints = match.GuestPoints
            };
            editMatchView.Teams = _context.Team.ToList();
            //cargarle los equipos a la view model
            return View(editMatchView);
        }

        [HttpPost]
        public IActionResult EditMatch(EditMatchViewModel viewModel)
        {
            bool hasError = false;
            viewModel.Teams = _context.Team.ToList();

            if (viewModel.HomeTeamId == viewModel.GuestTeamId)
            {
                ViewBag.ErrorMsg = ("Debe seleccionar un equipo. No pueden ser los mismos.");
                hasError = true;
            }

            if (viewModel.HomePoints < 0 || viewModel.GuestPoints < 0)
            {
                ViewBag.ErrorMsgPoint = ("Los puntuacion no puede ser menor a 0");
                hasError = true;
            }

            if (hasError == true)
            {
                return View(viewModel);
            }

            if (ModelState.IsValid)
            {
                Match match = new Match()
                {
                    Id = viewModel.Id,
                    HomeTeamId = viewModel.HomeTeamId,
                    GuestTeamId = viewModel.GuestTeamId,
                    HomePoints = viewModel.HomePoints,
                    GuestPoints = viewModel.GuestPoints
                };

                _context.Match.Update(match);
                _context.SaveChanges();
                TempData["mensaje"] = "El partido fue modificado";
                return RedirectToAction("Index", "TeamsRegistration");
            }
            return View();
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

        [HttpGet]
        public IActionResult DeleteMatch(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var match = _context.Match.Find(id);

            if (match == null)
            {
                return NotFound();
            }

            DeleteMatchViewModel editMatchView = new DeleteMatchViewModel()
            {
                Id = match.Id,
                HomeTeamId = (int)match.HomeTeamId,
                HomePoints = match.HomePoints,
                GuestTeamId = (int)match.GuestTeamId,
                GuestPoints = match.GuestPoints
            };
            editMatchView.Teams = _context.Team.ToList();

            return View(editMatchView);
        }

        [HttpPost]
        public IActionResult DeleteMatch(DeleteMatchViewModel deleteMatch)
        {
            Match match = new Match()
            {
                Id = deleteMatch.Id,
                HomeTeamId = deleteMatch.HomeTeamId,
                GuestTeamId = deleteMatch.GuestTeamId,
                HomePoints = deleteMatch.HomePoints,
                GuestPoints = deleteMatch.GuestPoints,
                
             
            };
            _context.Match.Remove(match);
            _context.SaveChanges();
            TempData["mensaje"] = "El partido se ha eliminado correctamente";
            return RedirectToAction("Index", "TeamsRegistration");
        }
    }
}
