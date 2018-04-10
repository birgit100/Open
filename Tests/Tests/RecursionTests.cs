﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Aids;

namespace Tests.Tests
{
    [TestClass]
    public class RecursionTests
    {
        private static string ToBackwardsUsingRecursion(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            var i = s.Length - 1;
            var x = s.Substring(0, i);
            x = ToBackwardsUsingRecursion(x);
            return s[i] + x;
        }

        [TestMethod]
        public void RecursionIsSlowTest()
        {
            var d1 = DateTime.Now.Ticks;
            for (var i = 0; i < 1000000; i++) SystemString.ToBackwards("abc");
            var d2 = DateTime.Now.Ticks;
            for (var i = 0; i < 1000000; i++) ToBackwardsUsingRecursion("abc");
            var d3 = DateTime.Now.Ticks;
            var r1 = d2 - d1;
            var r2 = d3 - d2;
            Assert.IsTrue(r1 < r2);
        }
    }
}
