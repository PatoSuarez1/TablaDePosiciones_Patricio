using System.ComponentModel.DataAnnotations;

namespace TablaDePosiciones_Patricio.ViewModels
{
    public class TeamRegistrationViewModel
    {
        [Display(Name = "Pos")]
        public int Position { get; set; }
        [Display(Name = "Equipo")]
        public string Name { get; set; }
        [Display(Name = "J")]
        public int Played { get; set; }
        [Display(Name = "G")]
        public int Won { get; set; }
        [Display(Name = "E")]
        public int Drawn { get; set; }
        [Display(Name = "P")]
        public int Lost { get; set; }
        [Display(Name = "G/F")]
        public int GoalsFavor { get; set; }
        [Display(Name = "G/E")]
        public int GoalsAgainst { get; set; }
        [Display(Name = "DIF")]
        public int GoalDifference { get; set; }
        [Display(Name = "Pts")]
        public int Points { get; set; }
    }
}
