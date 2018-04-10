using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Open.Tests.Aids
{
    [TestClass]
    public class IsAidsTested : AssemblyTests
    {
        [TestMethod]
        public void IsTested()
        {
            isAllClassesTested(Namespace("Aids"));
        }
    }
}
