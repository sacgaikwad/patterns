using NotificationService.Interface;
using NotificationService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Implementation
{
    public class EmailNotificationProvider : INotificationProvider
    {

        public async Task<string> Notify(NotificationRequest request)
        {
            return await Task.FromResult<string>(request.TokenType.ToString());
        }
    }
}
