using Swimming_Club_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swimming_Club_2.Services
{
    public class SwimmersDAO_EF : ISwimmerDataService
    {
        private readonly AppDbContext _context;
        public SwimmersDAO_EF(AppDbContext context)
        {
            _context = context;
        }
        public Swimmer Delete(int id)
        {
            var toDelete = _context.Swimmers.Find(id);
            if(toDelete != null)
            {
                _context.Swimmers.Remove(toDelete);
            }
            return toDelete;
        }

        public Swimmer GetOne(int id)
        {            
            return _context.Swimmers.Find(id);
        }

        public IEnumerable<Swimmer> GetAll()
        {
            return _context.Swimmers;
        }
        

        public Swimmer Insert(Swimmer swimmer)
        {
            _context.Add(swimmer);
            return swimmer;
        }

        public IEnumerable<Swimmer> SearchByLastName(string lastName)
        {
            var found = _context.Swimmers.Where(s => s.LastName == lastName).ToList();
            return found;
        }

        public Swimmer Update(Swimmer swimmer)
        {            
            if(_context.Swimmers.Any(s => s.Id == swimmer.Id))
            {
                var x = _context.Swimmers.Attach(swimmer);
                x.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }            
            return swimmer;
        }

        public List<Discipline> GetPRsBySwimmerId(int swimmerId)
        {
            var prs = _context.Disciplines.ToList();
            return prs;
        }
    }
}
