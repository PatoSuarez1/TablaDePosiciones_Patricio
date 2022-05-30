using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TablaDePosiciones_Patricio.Data;
using TablaDePosiciones_Patricio.ViewModels;

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

        //Http Get Match
        public IActionResult Match()
        {
            //crear la instancia
            var vm = new CreateMatchViewModel();
            //la vista ya debe tener la lista de equipos cargada
            vm.Teams = _context.Team.ToList();
            //retornar view de createMatchViewModel.cs
            return View(vm);
        }

        //Http Post Match
        [HttpPost]
        public IActionResult CMatchVM(CreateMatchViewModel match)
        {
            var homeTeam = _context.TeamRegistration.First(t => t.TeamId == match.HomeTeamId);
            var guestTeam = _context.TeamRegistration.First(t => t.TeamId == match.GuestTeamId);

            var homePoints = match.HomePoints;
            var guestPoints = match.GuestPoints;


            if (homePoints > guestPoints)
            {
                //partidos ganados
                homeTeam.Won += 1;
                //puntos en la tabla
                homeTeam.Points += 3;
                //partidos perdidos visit
                guestTeam.Lost += 1;
                //partidos jugados
                homeTeam.Played += 1;
                guestTeam.Played += 1;
                //goles a favor
                homeTeam.GoalsFavor += homePoints;
                guestTeam.GoalsFavor += guestPoints;
                //goles encontra
                homeTeam.GoalsAgainst += guestPoints;
                guestTeam.GoalsAgainst += homePoints;

                //diferencia de goles
                homeTeam.GoalDifference = homeTeam.GoalsFavor - homeTeam.GoalsAgainst;
                guestTeam.GoalDifference = guestTeam.GoalsFavor - guestTeam.GoalsAgainst;

            }
            else if (homePoints < guestPoints)
            {
                //partidos ganados
                guestTeam.Won += 1;
                //puntos en la tabla
                guestTeam.Points += 3;
                //partido perdido Local
                homeTeam.Lost += 1;
                //partidos jugados
                homeTeam.Played += 1;
                guestTeam.Played += 1;
                //goles a favor
                homeTeam.GoalsFavor += homePoints;
                guestTeam.GoalsFavor += guestPoints;
                //goles encontra
                homeTeam.GoalsAgainst += guestPoints;
                guestTeam.GoalsAgainst += homePoints;

                //diferencia de goles
                homeTeam.GoalDifference = homeTeam.GoalsFavor - homeTeam.GoalsAgainst;
                guestTeam.GoalDifference = guestTeam.GoalsFavor - guestTeam.GoalsAgainst;
            }
            else
            {
                //partidos empatados
                homeTeam.Drawn += 1;
                guestTeam.Drawn += 1;
                //puntos en la tabla
                homeTeam.Points += 1;
                guestTeam.Points += 1;
                //partidos jugados
                homeTeam.Played += 1;
                guestTeam.Played += 1;
                //goles a favor
                homeTeam.GoalsFavor += homePoints;
                guestTeam.GoalsFavor += guestPoints;
                //goles encontra
                homeTeam.GoalsAgainst += guestPoints;
                guestTeam.GoalsAgainst += homePoints;

                //diferencia de goles
                homeTeam.GoalDifference = homeTeam.GoalsFavor - homeTeam.GoalsAgainst;
                guestTeam.GoalDifference = guestTeam.GoalsFavor - guestTeam.GoalsAgainst;
            }

            _context.TeamRegistration.Update(homeTeam);
            _context.TeamRegistration.Update(guestTeam);
            _context.SaveChanges();
            //primero el action y despues el controller
            return RedirectToAction("Index", "TeamsRegistration");

        }

        //Http Get Historial
        //public IActionResult Historial(CreateMatchViewModel createMatchViewModel)
        //{
        //    if (createMatchViewModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(createMatchViewModel);
        //}

    }
}
