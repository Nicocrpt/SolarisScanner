using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SolarisScanner.Tests
{
    [TestClass]
    public class Test1
    {
        [TestMethod]
        public void ErrorMessage_PropertyChanged_IsWorking()
        {
            var vm = new LoginViewModel();

            bool eventRaised = false;
            vm.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(vm.ErrorMessage))
                    eventRaised = true;
            };

            vm.ErrorMessage = "Une erreur";
            Assert.AreEqual("Une erreur", vm.ErrorMessage);
            Assert.IsTrue(eventRaised);
        }
    }
}