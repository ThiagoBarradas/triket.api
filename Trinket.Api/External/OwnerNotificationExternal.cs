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
            var loc = currentVehicle.Location[0].ToString().Replace(",",".") + "," + currentVehicle.Location[1].ToString().Replace(",", ".");

            var obj = new {
                app_id = "570d6f01-58e2-472a-9025-a8a237a4c72d",
                include_player_ids = ownerNotification.OneSignalIds.ToArray(),
                url = string.Format("https://www.google.com.br/maps/search/{0}/@{0},17z", loc),
                android_led_color = "F47900FF",
                android_accent_color = "F47900FF",
                large_icon = "https://i.imgsafe.org/84/84157836e5.png",
                language = "en",
                headings = new { en = "Opa, achamos que alguém viu o seu carro!" },
                contents = new { en = string.Format("O usúario {0} viu o seu carro de placa {1} por ai! Da uma olhadinha onde clicando aqui.", currentVehicle.Owner.Name, currentVehicle.LicensePlate) },
                big_picture = string.Format("https://maps.googleapis.com/maps/api/staticmap?size=600x300&zoom=15&center={0}&markers=color:red%7Clabel:Trinket%7C{0}&format=png&style=feature:transit.line%7Cvisibility:simplified%7Ccolor:0xbababa&style=feature:road.highway%7Celement:labels.text.fill%7Cvisibility:on%7Ccolor:0xffffff&key=AIzaSyDNP8SVFrTICqLly8FZTw6oMphZjs9QgW4", loc)
            };

            RestClient client = new RestClient("https://onesignal.com/api/v1/notifications");
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Basic NGNjZTY3N2EtNDE4ZC00YWFlLTlhMjUtODBhMzc0MTA3NjBi");
            request.AddJsonBody(obj);

            var response = client.Execute(request);

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }
        }
    }
}
