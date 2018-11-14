using Beerhall.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beerhall.Data.Repositories {
    public class LocationRepository : ILocationRepository {
        private readonly ApplicationDbContext _context;

        public LocationRepository(ApplicationDbContext context) {
            _context = context;
        }

        public IEnumerable<Location> GetAll() {
            return _context.Locations.OrderBy(L => L.Name).ToList();
        }

        public Location GetByPostalCode(string postalCode) {
            return _context.Locations.FirstOrDefault(Location => Location.PostalCode == postalCode);
            
        }


    }
}
