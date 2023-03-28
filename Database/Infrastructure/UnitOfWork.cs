using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Infrastructure
{
	public class UnitOfWork<DbContext> : IUnitOfWork<DbContext> where DbContext : Microsoft.EntityFrameworkCore.DbContext, IBaseDbContext
	{
		private readonly IDatabaseFactory<DbContext> databaseFactory;
		private DbContext dbcontext;

		public UnitOfWork(IDatabaseFactory<DbContext> databaseFactory)
		{
			this.databaseFactory = databaseFactory;
		}

		protected DbContext DbContextV
		{
			get { return dbcontext ?? (dbcontext = databaseFactory.Get()); }
		}


		public async Task BeginTransaction()
		{
			await DbContextV.BeginTransaction();
		}

		public async Task Commit()
		{
			await DbContextV.Commit();
		}

		public async Task RollBack()
		{
			await DbContextV.RollBack();
		}
	}

}
