using NotificationService.Interface;
using NotificationService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Implementation
{
    public class NotiticationServiceImpl : INotificationService
    {

        private readonly Func<TokenType, INotificationProvider> _notificationProvider;

        public NotiticationServiceImpl(Func<TokenType, INotificationProvider> notificationProvider)
        {
            _notificationProvider = notificationProvider;
        }
        public async Task<string> Send(NotificationRequest notificationRequest)
        {
            return await _notificationProvider(notificationRequest.TokenType).Notify(notificationRequest);
        }
    }
}
