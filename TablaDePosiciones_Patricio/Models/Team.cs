using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TablaDePosiciones_Patricio.Models
{
    public class Team
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del Equipo es obligatorio.")]
        [Display(Name = "Equipo")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El Capitán es obligatorio.")]
        [Display(Name = "Capitán")]
        public string Captain { get; set; }

        [Required(ErrorMessage = "La fecha de fundación es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fundación")]
        public DateTime Foundation { get; set; }

        [Required(ErrorMessage = "La ubicacion es obligatoria.")]
        [Display(Name = "Ubicación")]
        public string Location { get; set; }

        //Referencia one2one
        public virtual TeamRegistration TeamRegistration { get; set; }

        //Relacion con la clase Match
        [InverseProperty("HomeTeam")]
        public virtual ICollection<Match> HomeMatches { get; set; }

        [InverseProperty("GuestTeam")]
        public virtual ICollection<Match> GuestMatches { get; set; }
    }
}
