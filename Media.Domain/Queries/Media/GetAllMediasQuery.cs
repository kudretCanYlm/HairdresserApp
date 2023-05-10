using FluentValidation;
using Media.Domain.Models;
using MediatR;
using NetDevPack.Messaging;

namespace Media.Domain.Queries.Media
{
	public class GetAllMediasQuery : IRequest<IEnumerable<MediaModel>>
	{
		public GetAllMediasQuery(Guid imageOwnerId)
		{
			ImageOwnerId = imageOwnerId;
		}

		public Guid ImageOwnerId { get; set; }
	}
}
