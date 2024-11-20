using AAITHelper.ModelHelper;
using RestSharp;
using Wasit.Core.Enums;

namespace Wasit.Core.Helpers.Notifications
{
    public class FCMHelper
    {
        public static async Task<bool> SendPushNotification(string serverKey, string senderId, List<DeviceIdModel> userDevices, dynamic info, string msg, NotifyTypes type, long? routeId = null, CategoryType? categoryType = null, string title = "")
        {
            var client = new RestClient("https://fcm.googleapis.com/fcm/");
            client.AddDefaultHeader("Accept", "application/json");
            client.AddDefaultHeader("Authorization", $"key={serverKey}");
            client.AddDefaultHeader("Sender", $"id={senderId}");

            foreach (var device in userDevices)
            {
                if (device == null)
                {
                    continue;
                }

                var request = new RestRequest("send");
                request.AddJsonBody(new
                {
                    to = device.DeviceId,
                    priority = "high",
                    notification = new
                    {
                        title = title,
                        body = msg,
                        sound = "default"
                    },
                    data = new
                    {
                        type = type,
                        itemId = routeId,
                        categoryType = categoryType,
                        priority = "high",
                        click_action = "flutterNotificationClick"
                    }
                });
                try
                {
                    var restResponse = await client.ExecutePostAsync(request);
                    var result = restResponse.Content;
                }
                catch
                {
                    continue;
                }
            }
            return true;
        }
    }
}
