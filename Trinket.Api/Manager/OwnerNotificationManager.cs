using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trinket.Api.Models;
using Trinket.Api.Utilities;

namespace Trinket.Api.Manager
{
    public class OwnerNotificationManager : BaseManager
    {
        public BaseResponse<object> CreateOrUpdateOwnerNotification(OwnerNotificationRequest ownerNotificationRequest)
        {
            BaseResponse<object> response = new BaseResponse<object>();

            var ownerNotification = this.OwnerNotificationRepository.GetOwnerNotification(ownerNotificationRequest.OwnerId);
            if (ownerNotification == null) ownerNotification = new OwnerNotification();

            ownerNotification.OwnerId = ownerNotificationRequest.OwnerId;
            ownerNotification.OneSignalIds.Add(ownerNotificationRequest.OneSignalId);
            ownerNotification.OneSignalIds = ownerNotification.OneSignalIds.Distinct().ToList();

            this.OwnerNotificationRepository.CreateOrUpdateOwnerNotification(ownerNotification);

            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            response.SuccessBody = ownerNotification;

            return response;
        }
    }
}
