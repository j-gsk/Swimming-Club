using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Swimming_Club_2.Models
{
    public interface ISwimmer
    {
        [Key]
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        DateTime DOB { get; set; }
        string Registration { get; set; }
        public List<Discipline> Disciplines { get; set; }
    }
}