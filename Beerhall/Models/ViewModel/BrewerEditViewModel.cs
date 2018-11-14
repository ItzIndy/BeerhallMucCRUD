using Beerhall.Models.Domain;

namespace Beerhall.Models.ViewModel {
    #region properties
    public class BrewerEditViewModel {
        public string Name { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public int? Turnover { get; }
        #endregion
        public BrewerEditViewModel() {

        }
        public BrewerEditViewModel(Brewer brewer) {
            Name = brewer.Name;
            Street = brewer.Street;
            PostalCode = brewer.Location?.PostalCode;
            Turnover = brewer.Turnover;
        }
    }

}
