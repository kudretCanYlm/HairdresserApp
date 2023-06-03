namespace Common.Media
{
	public class MediaUpdateModel
	{
		public Guid Id { get; set; }
		public string Base64Image { get; set; }
		public OperationEnum Operation { get; set; }

		public static bool SetSizeValidatinon(IEnumerable<MediaUpdateModel> mediaUpdateModels, int maxSize = 5)
		{
			var total = 0;

			foreach (var operation in mediaUpdateModels.Select(x => x.Operation))
			{
				switch (operation)
				{
					case OperationEnum.Delete:
						total -= 1;
						break;
					case OperationEnum.Update:
						total += 1;
						break;
					case OperationEnum.Create:
						total += 1;
						break;
					default:
						break;
				}
			}

			return total <= maxSize;

		}

	}

	public enum OperationEnum
	{
		Delete,
		Update,
		Create
	}
}
