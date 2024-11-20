
using Microsoft.AspNetCore.Identity;
using Wasit.Core.Entities.UserTables;
using Wasit.Core.Enums;

namespace Wasit.Context.Seeds
{
    public static class DefaultUser
    {
        public static List<ApplicationDbUser> IdentityBasicUserList()
        {
            var hasher = new PasswordHasher<ApplicationDbUser>();

            return new List<ApplicationDbUser>()
            {
                new ApplicationDbUser
                {
                    // ادمن لوحه التحكم
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    UserName = "aait@aait.sa",
                    Email = "aait@aait.sa",
                    User_Name = "aait@aait.sa",

                    UserType =UserType.Admin.ToString(),
                    ActiveCode = true,
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ImgProfile = "images/Users/default.jpg",
                    NormalizedEmail = "aait@aait.sa",
                    NormalizedUserName = "Aait@Aait.sa",
                    RegionId= 1,
                },
                new ApplicationDbUser
                {
                    // يوزر لسرفس
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    UserName = "Api@aait.sa",
                    Email = "Api@aait.sa",
                    User_Name = "Api@aait.sa",

                    UserType = UserType.Client.ToString(),
                    ActiveCode = true,
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ImgProfile = "images/Users/default.jpg",
                    NormalizedEmail = "Api@aait.sa",
                    NormalizedUserName = "Api@Aait.sa",
                    RegionId= 1
                },
            };
        }
    }
}
