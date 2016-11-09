using Etrack.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace Etrack.Infrastructure.Filters
{
    public class DbContextTransactionFilter : IAsyncActionFilter
    {
        private readonly EtrackDbContext _dbContext;

        public DbContextTransactionFilter(EtrackDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                await _dbContext.Database.BeginTransactionAsync();

                await next();

                _dbContext.Database.CommitTransaction();
            }
            catch (Exception)
            {
                _dbContext.Database.RollbackTransaction();
                throw;
            }
        }
    }
}
