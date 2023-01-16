using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Model
{
    public class NotificationRequest
    {
        public string TokenValue { get; set; }
        public TokenType TokenType { get; set; }
    }
}
