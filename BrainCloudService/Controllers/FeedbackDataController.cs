using BrainCloudService.DataObjects;
using BrainCloudService.Models;
using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;

namespace BrainCloudService.Controllers
{
    public class FeedbackDataController : TableController<FeedbackData>
    {

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            var context = new BrainCloudContext();
            DomainManager = new EntityDomainManager<FeedbackData>(context, Request);
        }

        // GET tables/FeedbackData
        public IQueryable<FeedbackData> GetAllFeedbackData()
        {
            return Query();
        }

        // GET tables/FeedbackData/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<FeedbackData> GetFeedbackData(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/FeedbackData/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<FeedbackData> PatchFeedbackData(string id, Delta<FeedbackData> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/FeedbackData
        public async Task<IHttpActionResult> PostFeedbackData(FeedbackData item)
        {
            var current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/FeedbackData/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteFeedbackData(string id)
        {
            return DeleteAsync(id);
        }


    }
}