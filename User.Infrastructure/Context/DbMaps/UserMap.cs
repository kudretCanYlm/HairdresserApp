using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Models;

namespace User.Infrastructure.Context.DbMaps
{
	public class UserMap : IEntityTypeConfiguration<UserModel>
	{
		public void Configure(EntityTypeBuilder<UserModel> builder)
		{
			builder.Property(x=> x.Id)
				.HasColumnName("Id"); 
		}
	}
}
