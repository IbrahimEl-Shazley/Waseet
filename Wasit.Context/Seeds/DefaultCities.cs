using Wasit.Core.Entities.UserTables;

namespace Wasit.Context.Seeds
{
    public static class DefaultCities
    {
        public static List<City> BasicCitiesList()
        {

            return new List<City>()
            {
                new City()
                {
                    Id = 1,
                    NameAr="الرياض",
                    NameEn="Riyadh",
                    IsActive=true,
                },
                new City()
                {
                    Id = 2,
                    NameAr="مكة المكرمة",
                    NameEn="Makkah",
                    IsActive=true,
                },
            };
        }
    }
}
