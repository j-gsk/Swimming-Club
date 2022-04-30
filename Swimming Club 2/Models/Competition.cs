using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Swimming_Club_2.Models
{
    public class Competition
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Name of Competion")]
        public string Name { get; set; }
        public string Location { get; set; }

        [DisplayName("Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DisplayName("Signed up swimmers")]

        public List<Swimmer> Swimmers { get; set; } = new List<Swimmer>();

        [DisplayName("Referees")]
        public List<Referee> Referee { get; set; } = new List<Referee>();

        //disciplines that are part of swimmer class are different to disciplines that are part of competition class. reason is swimmer's disciplines contain his personal records. Competition's disciplines have world, season, and event records.
                        
    }
}
