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
            IEnumerable<TeamRegistrationViewModel> teamRecords = _context.TeamRegistration.Include(x => x.Team).OrderByDescending(x => x.Points)
                .ThenByDescending(x => x.GoalDifference).Select(item=> new TeamRegistrationViewModel()).ToList();
           
            //meter el for para sumarle uno a posicion usar teamrecord.count y position = i + 1;

            return View(teamRecords);
        }   
    }
}
