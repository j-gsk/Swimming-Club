using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Swimming_Club_2.Models
{
    public interface ICompetition
    {
        [Key]
        public int Id { get; set; }
        string Name { get; set; }
        DateTime Date { get; set; }
        List<ISwimmer> Swimmers { get; set; }
        string Location { get; set; }

        //List<IDiscipline> Disciplines { get; set; }


    }
}