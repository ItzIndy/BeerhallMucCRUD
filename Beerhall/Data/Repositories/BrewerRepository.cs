using Beerhall.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beerhall.Data.Repositories {
    public class BrewerRepository : IBrewerRepository {

        private readonly ApplicationDbContext _context;

        //Belangrijk om deze constructor toe te voegen (klassieke fout op examen)
        public BrewerRepository(ApplicationDbContext context) {
            _context = context;
        }

        public void Add(Brewer brewer) {
            _context.Add(brewer);
            //Hetzelfde = _context.Brewers.Add(brewers); -> Brewers is een DbSet binnen ApplicationDbContext.
        }

        public IEnumerable<Brewer> GetAll() {
            return _context.Brewers.Include(brewer=> brewer.Location).OrderBy(b => b.Name).ToList();
        }

        public Brewer GetById(int id) {
            return _context.Brewers.Include(b=>b.Location).FirstOrDefault(b => b.BrewerId == id);
        }

        public void Remove(int id) {
             _context.Remove(GetById(id));
        }

        public void SaveChanges() {
            _context.SaveChanges();
        }
    }
}
