using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Open.Tests.Tests
{
    [TestClass] public class ThreadSafeTests
    {
        private List<string> values;
        private List<string> threads;
        private readonly object key = new object();
        private const string thread1 = "thread1";
        private const string thread2 = "thread2";

        [TestInitialize]
        public void TestInitialize()
        {
            values = new List<string>();
            threads = new List<string>();
        }

        [TestMethod]
        public void BlockOtherTest()
        {
            startThreads(addToListBlockOther);
            Assert.IsTrue(values.Count == 10);
            Assert.IsTrue(threads.Count == 1);
        }

        [TestMethod]
        public void ThreadSafeTest()
        {
            startThreads(addToListSafe);
            Assert.IsTrue(values.Count == 10);
            Assert.IsTrue(threads.Count == 2);
        }

        [TestMethod]
        public void NotThreadSafeTest()
        {
            startThreads(addToListNotSafe);
            Assert.IsTrue(values.Count > 10);
            Assert.IsTrue(threads.Count == 2);
        }

        private void startThreads(Action<string> method)
        {
            var th1 = new Thread(() => method(thread1));
            var th2 = new Thread(() => method(thread2));
            th1.Start();
            Thread.Sleep(5);
            th2.Start();
            Thread.Sleep(200);
        }

        private void addToListNotSafe(string thread)
        {
            for (var i = 0; i < 10; i++)
            {
                addValue(i, thread);
            }
        }

        private void addToListSafe(string thread)
        {
            for (var i = 0; i < 10; i++)
            {
                lock (key)
                {
                    addValue(i, thread);
                }
            }
        }

        private void addToListBlockOther(string thread)
        {
            lock (key)
            {
                for (var i = 0; i < 10; i++)
                {
                    addValue(i, thread);
                }
            }
        }

        private void addValue(int value, string thread)
        {
            var s = value.ToString();
            if (!values.Contains(s))
            {
                Thread.Sleep(5);
                values.Add(s);
                if (threads.Contains(thread)) return;
                threads.Add(thread);
            }
        }
    }
}
