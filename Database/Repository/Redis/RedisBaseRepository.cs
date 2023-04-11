using NetDevPack.Domain;
using Redis.OM;
using Redis.OM.Searching;
using System.Linq.Expressions;

namespace Database.Repository.Redis
{
	public class RedisBaseRepository<T> : IRedisBaseRepository<T> where T : class,IAggregateRoot
	{
        private readonly RedisCollection<T> _redisDb;
        private readonly RedisConnectionProvider _provider;

        public RedisBaseRepository(RedisConnectionProvider provider)
        {
            _provider = provider;
            _redisDb = (RedisCollection<T>)provider.RedisCollection<T>();
        }

		public async Task DeleteAsync(T model)
		{
			
			await _redisDb.DeleteAsync(model);
		}

		public async Task DeleteWhere(Expression<Func<T, bool>> where)
		{
			var first=await GetSingle(where);

			if (first != null)
				throw new ArgumentNullException(nameof(first));

			await DeleteAsync(first);
		}

		public async Task<IEnumerable<T>> GetAllByWhereAsync(Expression<Func<T, bool>> where)
		{
			var list = await _redisDb.Where(where).ToListAsync();
			return list;
		}

		public async Task<T> GetSingle(Expression<Func<T, bool>> where)
		{
			return await _redisDb.Where(where).FirstOrDefaultAsync();
		}

		public async Task<string> InsertAsync(T model)
		{
			return await _redisDb.InsertAsync(model);
		}

		public async Task UpdateAsync(T model)
		{
			await _redisDb.UpdateAsync(model);
		}

		public void Dispose()
		{
			_provider.Connection.Dispose();
		}

		public async Task AddExpire(string aggregateAndId, TimeSpan expiringTimeSeconds)
		{
			var time = expiringTimeSeconds.TotalSeconds.ToString();
			await _provider.Connection.ExecuteAsync("EXPIRE",aggregateAndId,time);
		}
	}
}
