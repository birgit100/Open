using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Aids;
using Open.Data.Location;
using Open.Domain.Country;
using Open.Infra.Country;

namespace Open.Tests.Infra.Country
{
    [TestClass]
    public class CountryObjectsRepositoryTests : ClassTests<CountryObjectsRepository>
    {
        private readonly CountryDbContext db;
        private readonly CountryObjectsRepository repository;
        private const int count = 10;

        public CountryObjectsRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<CountryDbContext>().UseInMemoryDatabase("TestDb").Options;
            db = new CountryDbContext(options);
            repository = new CountryObjectsRepository(db);
        }

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            Assert.AreEqual(0, db.Countries.Count());
            for (var i = 0; i < count; i++)
            {
                db.Countries.Add(GetRandom.Object<CountryDbRecord>());
                db.SaveChanges();
            }
        }

        [TestCleanup]
        public override void TestCleanup()
        {
            base.TestCleanup();
            foreach (var c in db.Countries)
            {
                db.Remove(c);
                db.SaveChanges();
            }
        }

        [TestMethod]
        public void CanCreate()
        {
            Assert.IsNotNull(new CountryObjectsRepository(null));
        }

        [TestMethod]
        public async Task GetObjectTest()
        {
            var o = GetRandom.Object<CountryObject>();
            var country = await repository.GetObject(o.DbRecord.ID);

            validateCountry(country.DbRecord, new CountryDbRecord());
            db.Countries.Add(o.DbRecord);
            db.SaveChanges();
            country = await repository.GetObject(o.DbRecord.ID);
            validateCountry(country.DbRecord, o.DbRecord);
        }


        [TestMethod]
        public async Task GetObjectsListTest()
        {
            var l = await repository.GetObjectsList();
            Assert.AreEqual(count, l.Count());
        }

        [TestMethod]
        public async Task AddObjectTest()
        {
            var o = GetRandom.Object<CountryObject>();
            var country = db.Countries.Find(o.DbRecord.ID);

            Assert.IsNull(country);
            await repository.AddObject(o);
            country = db.Countries.Find(o.DbRecord.ID);
            validateCountry(country, o.DbRecord);
        }

        [TestMethod]
        public async Task UpdateObjectTest()
        {
            var o = GetRandom.Object<CountryObject>();
            await repository.AddObject(o);
            o.DbRecord.Name = GetRandom.String();
            o.DbRecord.Code = GetRandom.String();
            o.DbRecord.ValidFrom = GetRandom.DateTime(null, DateTime.Now.AddYears(-10));
            o.DbRecord.ValidTo = GetRandom.DateTime(DateTime.Now.AddYears(10));

            repository.UpdateObject(o);
            Assert.AreEqual(count + 1, db.Countries.Count());
            var country = db.Countries.Find(o.DbRecord.ID);
            validateCountry(country, o.DbRecord);
        }

        [TestMethod]
        public void DeleteObjectTest()
        {
            var c = count;
            Assert.AreEqual(c, db.Countries.Count());
            foreach (var e in db.Countries)
            {
                repository.DeleteObject(new CountryObject(e));
                Assert.AreEqual(--c, db.Countries.Count());
            }
        }

        [TestMethod]
        public void IsInitializedTest()
        {
            Assert.IsTrue(repository.IsInitialized());
            TestCleanup();
            Assert.IsFalse(repository.IsInitialized());
        }

        private static void validateCountry(CountryDbRecord actual, CountryDbRecord expected)
        {
            Assert.AreEqual(actual.ID, expected.ID);
            Assert.AreEqual(actual.Name, expected.Name);
            Assert.AreEqual(actual.Code, expected.Code);
            Assert.AreEqual(actual.ValidFrom, expected.ValidFrom);
            Assert.AreEqual(actual.ValidTo, expected.ValidTo);
        }

    }
}

