using Open.Data.Location;
using System;
using System.Globalization;


namespace Open.Domain.Country
{
    public static class CountryObjectFactory
    {
        public static CountryObject Create(string id, string name, string code,
            DateTime? validFrom = null, DateTime? validTo = null)
        {
            var o = new CountryDbRecord
            {
                ID = id,
                Name = name,
                Code = code,
                ValidFrom = validFrom?? DateTime.MinValue,
                ValidTo = validTo?? DateTime.MaxValue
            };
            return new CountryObject(o);
        }

        public static CountryObject Create(RegionInfo r)
        {
            var id = r?.ThreeLetterISORegionName;
            var name = r?.DisplayName;
            var code = r?.TwoLetterISORegionName;
            return Create(id, name, code);
        }
    }
}
