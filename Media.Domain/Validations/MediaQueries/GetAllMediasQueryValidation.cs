using FluentValidation;
using Media.Domain.Queries.Media;

namespace Media.Domain.Validations.MediaQueries
{
	public class GetAllMediasQueryValidation : AbstractValidator<GetAllMediasQuery>
	{
		public GetAllMediasQueryValidation()
		{
			RuleFor(x => x.ImageOwnerId)
				.NotEqual(Guid.Empty);
		}
	}
}
