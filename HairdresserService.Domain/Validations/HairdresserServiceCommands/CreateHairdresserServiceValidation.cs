using FluentValidation;
using HairdresserService.Domain.Commands.HairdresserService;

namespace HairdresserService.Domain.Validations.HairdresserServiceCommands
{
	public class CreateHairdresserServiceValidation:HairdresserServiceValidation<CreateHairdresserServiceCommand>
	{
		public CreateHairdresserServiceValidation()
		{
			ValidateName();
			ValidatePrice();
			ValidateHairdresserId();
			ValidateMediaList();
		}

		private void ValidateMediaList()
		{
			RuleFor(x => x.Base64MediaList.Count())
				.LessThanOrEqualTo(5)
				.WithMessage("Maximum 5 Images");
		}
	}
}
