namespace VKCollectionApi.Infrastructure.Data
{
	public static class DataConstants
	{
		//Product
		public const int ProductNameMinLength = 4;
		public const int ProductNameMaxLength = 30;
		public const int ProductPriceMaxLength = 1000;
		public const int ProductPriceMinLength = 10;
		public const int ProductDescriptionMinLength = 10;

		//Account
		public const int FirstNameMaxLength = 20;
		public const int LastNameMaxLength = 20;

		//Review
		public const int CommentMaxLength = 500;
		public const int RatingMaxLength = 5;

		//Order
		public const int PaymentStatusMaxLength = 50;
		public const int OrderStatusMaxLength = 50;
		public const int TransactionIdMaxLength = 50;
	}
}
