using System.ComponentModel.DataAnnotations;

namespace Swimming_Club_2.Models
{
    public interface IReferee
    {
        [Key]
        int Id { get; set; }
        string FirstName { get; set; }        
        string LastName { get; set; }
        public string Registration { get; set; }

    }
}