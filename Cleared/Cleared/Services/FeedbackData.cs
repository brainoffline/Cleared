using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleared.Services
{
    public class FeedbackData
    {
        public string Id { get; set; }

        public int Happy { get; set; }
        public string Comment { get; set; }
        public string Email { get; set; }

        [Microsoft.WindowsAzure.MobileServices.Version]
        public string AzureVersion { get; set; }
    }
}
