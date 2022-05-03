using Swimming_Club_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swimming_Club_2.Services
{
    public interface ISwimmerDataService
    {
        IEnumerable<Swimmer> GetAll();
        IEnumerable<Swimmer> SearchByLastName(string lastName);
        Swimmer GetOne(int Id);
        Swimmer Insert(Swimmer swimmer);
        Swimmer Delete(int Id);
        List<Discipline> GetDisciplinesById(int swimmerId);
        Swimmer Update(Swimmer swimmer);






    }
}
