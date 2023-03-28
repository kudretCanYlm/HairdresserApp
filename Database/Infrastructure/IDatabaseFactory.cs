using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Infrastructure
{
	public interface IDatabaseFactory<DbContext> : IDisposable where DbContext : Microsoft.EntityFrameworkCore.DbContext, IBaseDbContext
	{
		DbContext Get();
	}
}
