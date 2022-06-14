using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TablaDePosiciones_Patricio.Data;
using TablaDePosiciones_Patricio.ViewModels;

namespace TablaDePosiciones_Patricio.Controllers
{
    public class TeamsRegistrationController : Controller
    {
        //Nos permite ingresar a la base de datos
        private readonly ApplicationDbContext _context;

        public TeamsRegistrationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {            
            var teams = _context.Team.ToArray();

            List<TeamRegistrationViewModel> teamRecords = new List<TeamRegistrationViewModel>();

            // ---- Tabla de posiciones ----
            for (int i = 0; i < teams.Count(); ++i)
            {
                var nroTeam = teams[i].Id;
                //Nombre del Equipo
                var name = teams[i].Name;
                //Partidos JUgados
                var playedH = _context.Match.Where(ph => ph.HomeTeamId == nroTeam).Count();
                var playedG = _context.Match.Where(pg => pg.GuestTeamId == nroTeam).Count();
                var playedT = playedH + playedG;

                // Partidos Ganados
                var wonH = _context.Match.Where(wh => wh.HomeTeamId == nroTeam && wh.HomePoints > wh.GuestPoints).Count();
                var wonG = _context.Match.Where(wg => wg.GuestTeamId == nroTeam && wg.GuestPoints > wg.HomePoints).Count();
                var WonT = wonH + wonG;
                var points = WonT * 3; 

                //Partido Empatado
                var drawnH = _context.Match.Where(dh => dh.HomeTeamId == nroTeam && dh.HomePoints == dh.GuestPoints).Count();
                var drawnG = _context.Match.Where(dg => dg.GuestTeamId == nroTeam && dg.GuestPoints == dg.HomePoints).Count();
                var drawnT = drawnH + drawnG;
                points += drawnT * 1; 

                // Partidos Perdidos
                var lostH = _context.Match.Where(lh => lh.HomeTeamId == nroTeam && lh.HomePoints < lh.GuestPoints).Count();
                var lostG = _context.Match.Where(lg => lg.GuestTeamId == nroTeam && lg.GuestPoints < lg.HomePoints).Count();
                var lostT = lostH + lostG;

                //---- Goles ----
                var goalsFH = _context.Match.Where(gfh => gfh.HomeTeamId == nroTeam).Select(m => m.HomePoints).Sum();
                var goalsFG = _context.Match.Where(gfg => gfg.GuestTeamId == nroTeam).Select(m => m.GuestPoints).Sum();
                var goalsFavorT = goalsFH + goalsFG;
                
                var goalsAH = _context.Match.Where(gfh => gfh.HomeTeamId == nroTeam).Select(m => m.GuestPoints).Sum();
                var goalsAG = _context.Match.Where(gfg => gfg.GuestTeamId == nroTeam).Select(m => m.HomePoints).Sum();
                var goalsAgainstT = goalsAH + goalsAG;

                var goalDifference = goalsFavorT - goalsAgainstT;

                TeamRegistrationViewModel viewModel = new TeamRegistrationViewModel()
                {   
                    Position = 0,
                    Name = name,
                    Played = playedT,                  
                    Won = WonT,
                    Drawn = drawnT,
                    Lost = lostT,
                    GoalsFavor = (int)goalsFavorT,
                    GoalsAgainst = (int)goalsAgainstT,
                    GoalDifference = (int)goalDifference,
                    Points = points
                };
                teamRecords.Add(viewModel);
            };

            var teamRecordsOrder = teamRecords.OrderByDescending(x => x.Points).ThenByDescending(x => x.GoalDifference).ToList();
       
            for (int i = 0; i < teamRecordsOrder.Count(); i++)
            {
                teamRecordsOrder[i].Position = i + 1;
            };
            return View(teamRecordsOrder);
        }
        
    }
}
