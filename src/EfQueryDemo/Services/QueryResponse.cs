﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using EfQueryDemo.Infrastructure.Database;

namespace EfQueryDemo.Services
{
    public class QueryResponse<T> : QResponse
    {
        public T Result { get; private set; }

        private readonly DatabaseContext _context;

        public QueryResponse(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<QResponse> ExecuteAsync(Func<DatabaseContext, Task<T>> action)
        {
            try
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                Result = await action(_context);
                stopWatch.Stop();
                QueryElapsed = stopWatch.ElapsedMilliseconds;
                Queries = _context.QueriesLog;

                return this;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
    }
}