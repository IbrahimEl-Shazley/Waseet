using Microsoft.EntityFrameworkCore;
using Wasit.Context;
using Wasit.Core.Entities.SettingTables;
using Wasit.Integration.DTOs;
using Wasit.Integration.Services.Abstraction;

namespace Wasit.Integration.Services.Implementation
{
    public class SMSService : ISMSService
    {
        private readonly ApplicationDbContext _db;
        public SMSService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Send(SMSDTO dto)
        {
            Setting GetInfoSms = await _db.Settings.FirstOrDefaultAsync();
            if (GetInfoSms != null)
            {
                if (GetInfoSms.SenderName_SMS != "test")
                {
                    var resultSms = await SendMessageBy4jawaly(dto.Message.ToString(), dto.Number, GetInfoSms.SenderName_SMS, GetInfoSms.UserName_SMS, GetInfoSms.Password_SMS);
                    return resultSms;
                }
            }
            return false;
        }

        public static async Task<bool> SendMessageBy4jawaly(string msg, string numbers, string senderName, string userName, string password)
        {
            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.4jawaly.net/");
            var res = (await client.GetAsync("api/sendsms.php?username=" + userName + "&password=" + password + "&numbers=" + numbers + "&sender=" + senderName + "&message=" + msg + "&&return=string"));

            return res.IsSuccessStatusCode;
        }
    }
}
