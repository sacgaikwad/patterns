using NotificationService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Interface
{
    public interface INotificationService
    {
        Task<string> Send(NotificationRequest notificationRequest);
    }
}
