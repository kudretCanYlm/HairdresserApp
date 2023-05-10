using FluentValidation;
using Media.Domain.Commands.Media;

namespace Media.Domain.Validations.MediaCommands
{
	public abstract class MediaValidation<T> : AbstractValidator<T> where T : MediaCommand
	{
		protected void ValidateId()
		{
			RuleFor(x => x.Id)
				.NotEqual(Guid.Empty);
		}
		protected void ValidateFileExtension()
		{
			RuleFor(x => x.FileExtension)
				.NotNull()
				.WithMessage("{PropertyName} cannot be null")
				.NotEqual(String.Empty)
				.WithMessage("{PropertyName} cannot be empty");
		}

		protected void ValidateMediaData()
		{
			RuleFor(x => x.MediaData)
				.NotNull()
				.WithMessage("{PropertyName} cannot be null")
				.NotEmpty()
				.WithMessage("{PropertyName} cannot be empty")
				.DependentRules(() =>
				{
					RuleFor(x => x.MediaData.Length).LessThanOrEqualTo(5000000)
					.WithMessage("{PropertyName} size lesser then 5mb or equal");
				});
		}

		protected void ValidateCustomType()
		{
			RuleFor(x => x.CustomType)
				.NotNull()
				.WithMessage("{PropertyName} cannot be null")
				.NotEqual(String.Empty)
				.WithMessage("{PropertyName} cannot be empty");
		}

		protected void ValidateImageOwnerId()
		{
			RuleFor(x => x.ImageOwnerId)
				.NotEqual(Guid.Empty);
		}

	}
}
