using FluiReader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluiReader.Services.Interfaces
{
    public interface ISubscriptionRepoService
    {
        public Task<IList<Subscription>> GetSubscriptionAsync(int page = 1, int pageSize = 10);
        public Task<Subscription> GetSubscriptionAsync(int id);
        public Task<Subscription> AddSubscriptionAsync(Subscription subscription);
        public Task<Subscription> EditSubscriptionAsync(Subscription subscription);
        public Task DeleteSubscription(int id);
    }
}
