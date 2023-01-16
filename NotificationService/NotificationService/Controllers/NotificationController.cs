using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Interface;
using NotificationService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        //private readonly INotificationService _notificationService;

        private readonly Func<TokenType, INotificationProvider> _notificationProvider;

        //public NotificationController(INotificationService notificationService)
        //{
        //    _notificationService = notificationService;
        //}


        public NotificationController(Func<TokenType, INotificationProvider> notificationProvider)
        {
            _notificationProvider = notificationProvider;
        }

        [HttpPost]
        public async Task<string> Post(NotificationRequest notificationRequest)
        {
            return await _notificationProvider(notificationRequest.TokenType).Notify(notificationRequest);
        }
    }
}
