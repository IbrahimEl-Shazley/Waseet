using Wasit.Core.Entities.UserTables;

namespace Wasit.Context.Seeds
{
    public static class DefaultRegions
    {
        public static List<Region> BasicRegionsList()
        {

            return new List<Region>()
            {
                new Region()
                {
                    Id = 1,
                    NameAr="الفاخرية",
                    NameEn="Alfakhiria",
                    CityId=1,   
                    IsActive=true,
                },
                new Region()
                {
                    Id = 2,
                    NameAr="المؤتمرات",
                    NameEn="Almutamarat",
                    CityId=1,
                    IsActive=true,
                },
                new Region()
                {
                    Id = 3,
                    NameAr="أجياد",
                    NameEn="Ajyad",
                    CityId=2,   
                    IsActive=true,
                },
                new Region()
                {
                    Id = 4,
                    NameAr="العدل",
                    NameEn="Aladl",
                    CityId=2,
                    IsActive=true,
                },
            };
        }
    }
}
