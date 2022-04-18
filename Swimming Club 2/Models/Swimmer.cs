using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Swimming_Club_2.Models
{
    public class Swimmer : ISwimmer
    {       
        public int Id { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [DisplayName("Registration Number")]
        public string Registration { get; set; } = string.Empty;

        public string EmailAddress { get; set; } = string.Empty;
        public List<Discipline> Disciplines { get; set; } = new List<Discipline>();
    }
}
