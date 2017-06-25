using Cleared.Model;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleared.Services
{
    public class BrainCloudService
    {
        static BrainCloudService current;
        public static BrainCloudService Current
        {
            get { return (current = current ?? new BrainCloudService()); }
        }

        const string appUrl = "https://braincloud.azurewebsites.net/";
        const string filename = "BrainCloud.db";

        MobileServiceClient client = null;
        IMobileServiceSyncTable<FeedbackData> feedbackTable;

        public async Task Initialize()
        {
            if (client?.SyncContext?.IsInitialized ?? false)
                return;

            client = new MobileServiceClient(appUrl);
            var store = new MobileServiceSQLiteStore(filename);

            store.DefineTable<FeedbackData>();

            await client.SyncContext.InitializeAsync(store);

            feedbackTable = client.GetSyncTable<FeedbackData>();
        }

        public async Task SyncFeedback()
        {
            await Initialize();

            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                    return;

                await client.SyncContext.PushAsync();

                GameData.Current.FeedbackSent = true;

                await GameData.Current.SaveData();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public async Task<FeedbackData> AddFeedback(FeedbackData feedback)
        {
            await Initialize();

            await feedbackTable.InsertAsync(feedback);

            await SyncFeedback();

            return feedback;
        }
    }
}
