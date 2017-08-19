using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Trinket.Api.Models;

namespace Trinket.Api.External
{
    public class OwnerNotificationExternal
    {
        public void SendPushNotification(Vehicle vehicle, OwnerNotification ownerNotification, Vehicle currentVehicle, Owner ownerWhoSearched)
        {
            var loc = currentVehicle.Location[0] + "," + currentVehicle.Location[1];

            var obj = new {
                app_id = "570d6f01-58e2-472a-9025-a8a237a4c72d",
                include_player_ids = ownerNotification.OneSignalIds.ToArray(),
                url = string.Format("https://www.google.com.br/maps/search/{0}/@{0},17z", loc),
                android_led_color = "F47900FF",
                android_accent_color = "F47900FF",
                large_icon = "https://i.imgsafe.org/84/84157836e5.png",
                language = "pt",
                headings = new { pt = "Opa, achamos que alguém viu o seu carro!" },
                contents = new { pt = string.Format("O usúario {0} viu o seu carro de placa {1} por ai! Da uma olhadinha onde clicando aqui.", currentVehicle.Owner.Name, currentVehicle.LicensePlate) }
            };

        

//            {
//                "app_id": "5eb5a37e-b458-11e3-ac11-000c2940e62c",
//  "include_player_ids": ["6392d91a-b206-4b7b-a620-cd68e32c3a76","76ece62b-bcfe-468c-8a78-839aeaa8c5fa","8e0f21fa-9a5a-4ae7-a9a6-ca1f24294b86"],
//  "data": {"foo": "bar"},
//  "contents": {"en": "English Message"}
//}


            RestClient client = new RestClient("https://onesignal.com/api/v1/notifications");
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Basic NGNjZTY3N2EtNDE4ZC00YWFlLTlhMjUtODBhMzc0MTA3NjBi");
            request.AddJsonBody(obj);

            var response = client.Execute<VehicleDetailsExternal>(request);

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

            //return response.Data;
        }
    }
}
