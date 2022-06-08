using System.ComponentModel.DataAnnotations;

namespace TablaDePosiciones_Patricio.ViewModels
{
    public class HistorialViewModel
    {
        [Display(Name = "Local")]
        public string HomeTeamName { get; set; }
        [Display(Name = "Visitante")]
        public string GuestTeamName { get; set; }
        [Display(Name = "Pts")]
        public float HomePoints { get; set; }
        [Display(Name = "Pts")]
        public float GuestPoints { get; set; }
    }
}
