namespace CA.Entities
{
	using System.ComponentModel.DataAnnotations;
	public class ChatUser
	{
		[Key]
		public long UserId { get; set; }
		[StringLength(Constants.MaxLen, MinimumLength = Constants.MinLen)]
		public string Name { get; set; }
	}
}
