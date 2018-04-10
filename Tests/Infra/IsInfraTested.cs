using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Open.Tests.Infra
{
    [TestClass]
    public class IsInfraTested : AssemblyTests
    {
        private const string assembly = "Open.Infra";

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
