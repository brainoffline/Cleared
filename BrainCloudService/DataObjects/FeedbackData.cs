using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrainCloudService.DataObjects
{
    public class FeedbackData : EntityData
    {
        public int Happy { get; set; }
        public string Comment { get; set; }
        public string Email { get; set; }
    }
}