using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        //Http Post Match
        [HttpPost]
        public IActionResult CMatchVM(CreateMatchViewModel match)
        {
            var homeTeam = match.HomeTeamId;
            var guestTeam = match.GuestTeamId;
            //var homeTeam = _context.TeamRegistration.First(t => t.TeamId == match.HomeTeamId);
            //var guestTeam = _context.TeamRegistration.First(t => t.TeamId == match.GuestTeamId);
            var homePoints = match.HomePoints;
            var guestPoints = match.GuestPoints;
            bool hasError = false;


            if (homeTeam == guestTeam)
            {
                ViewBag.ErrorMsg = ("Debe seleccionar un equipo. No pueden ser los mismos.");
                match.Teams = _context.Team.ToList();
                hasError = true;
            }

            if (homePoints < 0 || guestPoints < 0)
            {
                ViewBag.ErrorMsgPoint = ("Los puntuacion no puede ser menor a 0");
                match.Teams = _context.Team.ToList();
                hasError = true;
            }

            if (hasError == true)
            {
                return View(match);
            }

            var totalTeams = _context.Team.Select(t => t.Id).Count();
            var teams = _context.Team.ToArray();
            //var totalMatch = _context.Match.Select(m => m.HomeTeamId).Count();
            //var matches = _context.Match.ToArray();

            // ---- Tabla de posiciones ----
            for (int i = 0; i < totalTeams; ++i)
            {
                var nroTeam = teams[i].Id;
                //Partidos JUgados
                var playedL = _context.Match.Where(pl => pl.HomeTeamId == nroTeam).Count();
                var playedV = _context.Match.Where(pv => pv.GuestTeamId == nroTeam).Count();
                var playedT = playedL + playedV; //guardar.
                 
                // Partidos Ganados
                var wonL = _context.Match.Where(wl => wl.HomeTeamId == nroTeam && wl.HomePoints > wl.GuestPoints).Count();
                var wonV = _context.Match.Where(wv => wv.GuestTeamId == nroTeam && wv.GuestPoints > wv.HomePoints).Count();
                var WonT = wonL + wonV;//guardar.
                var points = WonT * 3; //guardar.

                //Partido Empatado
                var drawnL = _context.Match.Where(dl => dl.HomeTeamId == nroTeam && dl.HomePoints == dl.GuestPoints).Count();
                var drawnV = _context.Match.Where(dv => dv.GuestTeamId == nroTeam && dv.GuestPoints == dv.HomePoints).Count();
                var drawnT = drawnL + drawnV;//guardar.
                points += drawnT * 1; //guardar.

                // Partidos Perdidos
                var lostL = _context.Match.Where(ll => ll.HomeTeamId == nroTeam && ll.HomePoints < ll.GuestPoints).Count();
                var lostV = _context.Match.Where(lv => lv.GuestTeamId == nroTeam && lv.GuestPoints < lv.HomePoints).Count();
                var lostT = lostL + lostV; //guardar.

                //---- Faltan Goles a favor, encontra y diferencia. -----
                var goalsFL = _context.Match.Where(gfl => gfl.HomeTeamId == nroTeam && gfl.HomePoints >= 0 ).Count();
                var goalsFV = _context.Match.Where(gfv => gfv.GuestTeamId == nroTeam && gfv.GuestPoints >= 0 ).Count();
                var goalsFavorT = goalsFL + goalsFV;

                var goalsAL = _context.Match.Where(gal => gal.HomeTeamId == nroTeam && gal.GuestPoints >= 0).Count();
                var goalsAV = _context.Match.Where(gav => gav.GuestTeamId == nroTeam && gav.HomePoints >= 0).Count();
                var goalsAgainstT = goalsAL + goalsAV;
            };




            //if (homePoints > guestPoints)
            //{
            //    //partidos ganados
            //    homeTeam.Won += 1;
            //    //puntos en la tabla
            //    homeTeam.Points += 3;
            //    //partidos perdidos visit
            //    guestTeam.Lost += 1;
            //    //partidos jugados
            //    homeTeam.Played += 1;
            //    guestTeam.Played += 1;
            //    //goles a favor
            //    homeTeam.GoalsFavor += homePoints;
            //    guestTeam.GoalsFavor += guestPoints;
            //    //goles encontra
            //    homeTeam.GoalsAgainst += guestPoints;
            //    guestTeam.GoalsAgainst += homePoints;

            //    //diferencia de goles
            //    homeTeam.GoalDifference = homeTeam.GoalsFavor - homeTeam.GoalsAgainst;
            //    guestTeam.GoalDifference = guestTeam.GoalsFavor - guestTeam.GoalsAgainst;

            //}
            //else if (homePoints < guestPoints)
            //{
            //    //partidos ganados
            //    guestTeam.Won += 1;
            //    //puntos en la tabla
            //    guestTeam.Points += 3;
            //    //partido perdido Local
            //    homeTeam.Lost += 1;
            //    //partidos jugados
            //    homeTeam.Played += 1;
            //    guestTeam.Played += 1;
            //    //goles a favor
            //    homeTeam.GoalsFavor += homePoints;
            //    guestTeam.GoalsFavor += guestPoints;
            //    //goles encontra
            //    homeTeam.GoalsAgainst += guestPoints;
            //    guestTeam.GoalsAgainst += homePoints;

            //    //diferencia de goles
            //    homeTeam.GoalDifference = homeTeam.GoalsFavor - homeTeam.GoalsAgainst;
            //    guestTeam.GoalDifference = guestTeam.GoalsFavor - guestTeam.GoalsAgainst;
            //}
            //else
            //{
            //    //partidos empatados
            //    homeTeam.Drawn += 1;
            //    guestTeam.Drawn += 1;
            //    //puntos en la tabla
            //    homeTeam.Points += 1;
            //    guestTeam.Points += 1;
            //    //partidos jugados
            //    homeTeam.Played += 1;
            //    guestTeam.Played += 1;
            //    //goles a favor
            //    homeTeam.GoalsFavor += homePoints;
            //    guestTeam.GoalsFavor += guestPoints;
            //    //goles encontra
            //    homeTeam.GoalsAgainst += guestPoints;
            //    guestTeam.GoalsAgainst += homePoints;

            //    //diferencia de goles
            //    homeTeam.GoalDifference = homeTeam.GoalsFavor - homeTeam.GoalsAgainst;
            //    guestTeam.GoalDifference = guestTeam.GoalsFavor - guestTeam.GoalsAgainst;
            //}

            //var historial = new Match()
            //{
            //    HomeTeamId = homeTeam.TeamId,
            //    GuestTeamId = guestTeam.TeamId,
            //    HomePoints = homePoints,
            //    GuestPoints = guestPoints
            //};

            //_context.Match.Add(historial);
            //_context.TeamRegistration.Update(homeTeam);
            //_context.TeamRegistration.Update(guestTeam);
            _context.SaveChanges();
            //aviso partido realizado
            TempData["mensaje"] = "El partido fue añadido exitosamente";
            //primero el action y despues el controller
            return RedirectToAction("Index", "TeamsRegistration");
        }

        //http get
        public IActionResult Historial()
        {
            List<HistorialViewModel> matchs = _context.Match.Include(x => x.HomeTeam)
                .Include(x => x.GuestTeam).Select(item => new HistorialViewModel()
                {
                    HomeTeamName = item.HomeTeam.Name,
                    GuestTeamName = item.GuestTeam.Name,
                    HomePoints = item.HomePoints,
                    GuestPoints = item.GuestPoints
                }).ToList();

            return View(matchs);
        }

    }
}
