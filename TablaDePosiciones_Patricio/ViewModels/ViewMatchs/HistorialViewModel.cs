using System.ComponentModel.DataAnnotations;

namespace TablaDePosiciones_Patricio.ViewModels.ViewMatchs
{
    public class HistorialViewModel
    {
        public int Id { get; set; }
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
