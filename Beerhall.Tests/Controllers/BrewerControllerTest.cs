using Beerhall.Controllers;
using Beerhall.Models.Domain;
using Beerhall.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Beerhall.Tests.Controllers {
    public class BrewerControllerTest {
        private readonly BrewerController _controller;
        /*
         * Net als Bij ontwerpen maken we Mock objecten zodat de te testen klasse niet afhangt van andere klasses/databanken
         * We importen dus Moq bij NuGet packages.
         * We maken dan een Mock Object en geven ze mee aan onze controller.
         */
        private readonly Mock<IBrewerRepository> _brewerRepository;
        private readonly Mock<ILocationRepository> _locationRepository;
        public BrewerControllerTest() {
            _brewerRepository = new Mock<IBrewerRepository>();
            _locationRepository = new Mock<ILocationRepository>();
            //Je moet .Object gebruiken, want de constructor verwacht een object van een brewerRepository en een LocationRepository. 
            _controller = new BrewerController(_brewerRepository.Object, _locationRepository.Object);
            _controller.TempData = new Mock<ITempDataDictionary>().Object;
        }

        //test op httpGet
        //Index slaagt op de View actionmethod.
        [Fact]
        public void Index_PassesOrderedListOfBrewersToDefautlView() {
            //Arrange
            var brewers = new List<Brewer>() {
                new Brewer("Brewer1"),
                new Brewer("Brewer2")
            };
            //trainen Mock object
            _brewerRepository.Setup(m => m.GetAll()).Returns(brewers);
            //Act
            /*Voor de action method op te roepen import je 2 NuGet packages:
             * 1) microsoft.aspnetcore.mvc Abstractions
             * 2) microsoft.aspnetcore.mvc ViewFeatures
            */
            var result = _controller.Index() as ViewResult; //type dat je terug krijgt is ViewResult.

            //Assert
            Assert.Equal(2, (result.Model as IEnumerable<Brewer>).Count());
            Assert.Null(result.ViewName); //default view gebruiken, dus je gaf geen naam mee. (geen doorverwijzing etc.)
        }

        //test op httpPost
        [Fact]
        public void EditHttpPost_ValidEdit_ChangesAndPersistBrewer() {
            //Arrange
            var liefmans = new Brewer("Liefmans");
            var brewerEditViewModel = new BrewerEditViewModel(liefmans);

            //mock trainen
            _brewerRepository.Setup(m => m.GetBy(1)).Returns(liefmans);
            //Do a valid edit
            brewerEditViewModel.Name = "InBev";

            //Act
            var result = _controller.Edit(brewerEditViewModel, 1);

            //Assert
            Assert.Equal("InBev", liefmans.Name);
            _brewerRepository.Verify(r => r.SaveChanges(), Times.Once);

        }

        [Fact]
        public void EditHttpPost_ValidEdit_Redirects() {
            //Arrange
            var liefmans = new Brewer("Liefmans");
            var brewerEditViewModel = new BrewerEditViewModel(liefmans);

            //mock trainen
            _brewerRepository.Setup(m => m.GetBy(1)).Returns(liefmans);
            //Do a valid edit
            brewerEditViewModel.Name = "InBev";

            //Act
            var result = _controller.Edit(brewerEditViewModel, 1) as RedirectToActionResult;

            //Assert
            Assert.Equal("Index", result.ActionName);
        }
    }
}
