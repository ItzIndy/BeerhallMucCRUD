using System.Collections.Generic;

namespace Beerhall.Models.Domain {
    public interface IBrewerRepository {
        IEnumerable<Brewer> GetAll();
        Brewer GetById(int id);
        void Add(Brewer brewer);
        void Remove(int id);
        void SaveChanges();

    }
}
