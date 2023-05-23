using FluiReader.Models;
using FluiReader.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluiReader.Services
{
    public class LocalSubscriptionRepoService : ISubscriptionRepoService
    {
        private readonly LocalDatabaseService _db;

        public LocalSubscriptionRepoService(LocalDatabaseService db)
        {
            this._db = db;
        }
        public async Task<Subscription> AddSubscriptionAsync(Subscription subscription)
        {
            await _db.Init();
            await _db.Database.InsertAsync(subscription);
            // this is safe as we are a simple client-side db.
            // and the UI won't allow race condition to happen
            return await _db.Database.Table<Subscription>().OrderByDescending(x => x.Id).FirstAsync();
        }

        public async Task DeleteSubscription(int id)
        {
            await _db.Init();
            await _db.Database.DeleteAsync<Subscription>(id);
        }

        public async Task<IList<Subscription>> GetSubscriptionAsync(int page, int pageSize)
        {
            await _db.Init();
            return await _db.Database.Table<Subscription>()
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();
        }

        public async Task<Subscription> GetSubscriptionAsync(int id)
        {
            await _db.Init();
            return await _db.Database.GetAsync<Subscription>(id);
        }

        public async Task<Subscription> EditSubscriptionAsync(Subscription subscription)
        {
            await _db.Init();
            await _db.Database.UpdateAsync(subscription);
            return await _db.Database.GetAsync<Subscription>(subscription.Id);
        }
    }
}
