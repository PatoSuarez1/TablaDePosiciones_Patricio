using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TablaDePosiciones_Patricio.Models;

namespace TablaDePosiciones_Patricio.ViewModels.ViewMatchs
{
    public class DeleteMatchViewModel
    {
        public IEnumerable<Team> Teams { get; set; }
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe seleccionar un equipo")]
        [Display(Name = "Local")]
        public int HomeTeamId { get; set; }
        [Required(ErrorMessage = "Debe seleccionar un equipo")]
        [Display(Name = "Visitante")]
        public int GuestTeamId { get; set; }
        [Display(Name = "Pts")]
        public float HomePoints { get; set; }
        [Display(Name = "Pts")]
        public float GuestPoints { get; set; }
    }
}
