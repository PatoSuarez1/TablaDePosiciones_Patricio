using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TablaDePosiciones_Patricio.Data;
using TablaDePosiciones_Patricio.Models;
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
        //Http Get Index
        public IActionResult Index()
        {
            //De esta forma incluyo dentro de la clase teamRegistration todo lo que este en la clase Team.
            List<TeamRegistrationViewModel> teamRecords = _context.TeamRegistration.Include(x => x.Team)
               //ordeno la tabla por puntos y por goles de diferencia
                .OrderByDescending(x => x.Points).ThenByDescending(x => x.GoalDifference)
                //"convierto" TeamRegistration en TeamRegistrationViewModel
                .Select(item => new TeamRegistrationViewModel()
                {
                    Name = item.Team.Name,
                    Played = item.Played,
                    Won = item.Won,
                    Drawn = item.Drawn,
                    Lost = item.Lost,
                    GoalsFavor = item.GoalsFavor,
                    GoalsAgainst = item.GoalsAgainst,
                    GoalDifference = item.GoalDifference,   
                    Points = item.Points

                }).ToList();

            for (int i = 0; i < teamRecords.Count(); i++)
            {
                teamRecords[i].Position = i + 1;                
            }

            return View(teamRecords);
        }   
    }
}
