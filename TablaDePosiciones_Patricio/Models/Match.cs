using System.ComponentModel.DataAnnotations.Schema;

namespace TablaDePosiciones_Patricio.Models
{
    public class Match
    {
        public int Id { get; set; }
        [ForeignKey("HomeTeam")]
        public int? HomeTeamId { get; set; }
        [ForeignKey("GuestTeam")]
        public int? GuestTeamId { get; set; }
        public float HomePoints { get; set; }
        public float GuestPoints { get; set; }

        //Relacion con clase Team

        public Team HomeTeam { get; set; }
        public Team GuestTeam { get; set; }

    }
}