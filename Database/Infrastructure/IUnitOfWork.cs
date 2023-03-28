using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Infrastructure
{
	public interface IUnitOfWork<DbContext> where DbContext : Microsoft.EntityFrameworkCore.DbContext, IBaseDbContext
	{
		Task Commit();
		Task BeginTransaction();
		Task RollBack();
	}
}
