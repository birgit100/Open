using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Open.Tests.Facade
{
    [TestClass]
    public class IsFacadeTested : AssemblyTests
    {
        private const string assembly = "Open.Facade";

        protected override string Namespace(string name)
        {
            return $"{assembly}.{name}";
        }

        [TestMethod]
        public void IsCountryTested()
        {
            isAllClassesTested(assembly, Namespace("Country"));
        }
    }
}
