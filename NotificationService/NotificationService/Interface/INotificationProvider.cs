using NotificationService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Interface
{
    public interface INotificationProvider
    {
        Task<string> Notify(NotificationRequest request);
    }
}
