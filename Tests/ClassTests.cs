using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Open.Tests
{
    public class ClassTests<T> : BaseTests
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            type = typeof(T);
        }
    }
}
