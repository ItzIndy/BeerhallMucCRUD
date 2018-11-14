using Beerhall.Models.Domain;
using Beerhall.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Beerhall.Controllers {
    public class BrewerController : Controller
    {
        private readonly IBrewerRepository _brewerRepository;
        private readonly ILocationRepository _locationRepository;

        public BrewerController(IBrewerRepository brewerRepository, ILocationRepository locationRepository) {
            _brewerRepository = brewerRepository;
            _locationRepository = locationRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<Brewer> brewers = _brewerRepository.GetAll();
            return View(brewers);
        }

        public IActionResult Edit(int id) {
            var brewer = _brewerRepository.GetById(id);
            ViewData["Locations"] = new SelectList(_locationRepository.GetAll(), "PostalCode","Name");
            return View(new BrewerEditViewModel(brewer));
        }

        [HttpPost] 
        /*als je string name, string street, string postalCode, int? turnover as paramaters meegeven,
        maar als je deze BrewerEditViewModel gebruikt, maakt die automatisch dit object aan.
        Alles dat hier gebeurd noemen we model-binding. Dit werkt in 2 richtingen, 
        je kan data meegeven met een view, maar je kan ook data binnen trekken! */
        public IActionResult Edit(int id, BrewerEditViewModel model) {
            var brewer = _brewerRepository.GetById(id);
            brewer.Name = model.Name;
            brewer.Street = model.Street;
            brewer.Location = _locationRepository.GetByPostalCode(model.PostalCode);
            brewer.Turnover = model.Turnover;
            _brewerRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}