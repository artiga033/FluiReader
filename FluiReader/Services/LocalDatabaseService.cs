using FluiReader.Models;
using Microsoft.Extensions.Logging;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluiReader.Services
{
    public class LocalDatabaseService
    {

        private SQLiteAsyncConnection? database;
        private readonly ILogger<LocalDatabaseService> _logger;

        /// <summary>
        /// Call <see cref="Init"/> to make sure this is not null
        /// </summary>
        public SQLiteAsyncConnection? Database
        {
            get => database; set => database = value;
        }

        public LocalDatabaseService(ILogger<LocalDatabaseService> logger)
        {
            this._logger = logger;
        }

        [MemberNotNull(nameof(Database))]
        public async Task Init()
        {
            if (Database is not null)
                return;

            var dbPath = Constants.DatabasePath;
            _logger.LogDebug("DBPATH:{}", dbPath);
            Database = new SQLiteAsyncConnection(dbPath, Constants.Flags);
            var result = await Database.CreateTableAsync<Subscription>();
        }
    }
}
