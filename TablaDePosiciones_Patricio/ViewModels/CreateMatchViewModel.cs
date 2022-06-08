using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TablaDePosiciones_Patricio.Models;

namespace TablaDePosiciones_Patricio.ViewModels
{
    public class CreateMatchViewModel
    {
        //lista de equipos y datos que estan en match.cs
        public IEnumerable<Team> Teams { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un equipo")]
        [Display(Name = "Local")]
        public int HomeTeamId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un equipo")]
        [Display(Name = "Visitante")]
        public int GuestTeamId { get; set; }

        [Display(Name = "Pts")]
        public int HomePoints { get; set; }

        [Display(Name = "Pts")]
        public int GuestPoints { get; set; }
    }
}
